# ADR 0001: Incremental build-time content generation

- Date: 2026-11-15
- Status: Accepted
- Implementation: [PR #667](https://github.com/ILoveDotNet/ilovedotnet/pull/667)

## Context

Building the application also produces sitemaps, posters, RSS, search vectors,
the browser-local chat corpus, JavaScript bundles and Release CSS.

Previously, 32 sitemap targets and 31 poster targets ran on every non-Release
build without MSBuild input/output tracking. Topic projects duplicated these
targets and launched generators through `dotnet run`, using caller-specific
build scopes. This repeated nested build work across channels.

Search, chat and JavaScript generation already had some incremental checks.
However, search/chat inputs included generated C# under `SharedModels\bin` and
`SharedModels\obj`, creating unnecessary invalidations. Whenever vector
generation ran, it recomputed embeddings for unchanged text. Release CSS and
RSS generation also repeated work unnecessarily.

The goal is to avoid redundant work without disabling application features,
changing asset formats or paths, or changing publication eligibility.

## Decision

Use MSBuild incremental targets for asset generation, shared generator builds,
and a content-addressed embedding cache.

### Centralize project configuration

`Directory.Build.targets` supplies shared sitemap/poster targets. Topic projects
declare their channel and original output locations instead of duplicating
generation commands. For example, `AIDemoComponents.csproj` declares:

```xml
<PropertyGroup>
  <ContentGenerationChannel>AI</ContentGenerationChannel>
  <SitemapXmlFilePath>$([System.IO.Path]::Combine('wwwroot', 'sitemap-blog-ai.xml'))</SitemapXmlFilePath>
  <PosterOutputPath>$([System.IO.Path]::Combine('wwwroot', 'image', 'blogs', 'ai'))</PosterOutputPath>
</PropertyGroup>
```

MSBuild automatically imports the shared targets. `CommonComponents.csproj`
applies the same tracking approach to RSS, search, chat, bundles and CSS.

### Track inputs, inventories and successful-generation stamps

Each generation target declares relevant source/configuration files as `Inputs`
and a successful-generation stamp under its project's `obj` directory as
`Outputs`. MSBuild skips the target body when the stamp is current.

Preparation targets maintain input inventories with `WriteOnlyWhenDifferent`.
These detect added/removed files and changed generation settings without
invalidating unchanged builds. Generated `bin`/`obj` C# is excluded from model
inputs.

Expected-output checks invalidate stamps when required files are missing.
Poster inventories track PNG/WebP outputs; chat inventories track the manifest
and both current versioned corpus files. Existing output files are also inputs
where appropriate, so external modifications invalidate generation.

Stamps are updated only after successful target execution. They are used instead
of generated-file timestamps because generators may intentionally leave
unchanged files untouched. Otherwise, a newer input could cause generation to
repeat forever even after successful processing.

Asset inclusion is separate from generation: skipped targets must not exclude
existing generated files from the application.

### Share and isolate generator builds

Generators use the shared `BuildScope=ContentGenerators`, Debug configuration,
and repository generator framework. Required generation builds the tools through
MSBuild and executes their compiled DLLs directly, rather than using repeated
`dotnet run` commands.

Nested restore/build calls remove caller-specific runtime/publish properties
and `OutputPath`, `BaseOutputPath`, and `OutDir`. This keeps generator DLLs in
their expected shared output paths, including when the application uses custom
output directories.

### Reuse embeddings safely

The cache under `VectorSearchIndexGenerator\.embedding-cache` is keyed by
embedding text and a pipeline fingerprint covering model/vocabulary bytes,
model identity/revision, generator/ONNX/tokenizer assemblies and host/runtime
identity.

Cached vectors are validated for checksum, length, dimensions and finite values.
Corrupt entries fail explicitly with a regeneration instruction rather than
silently producing an invalid index. Writes are atomic.

The ONNX inference session is initialized lazily, so an all-cache-hit generation
does not initialize inference. During corpus regeneration, missing/corrupt files
inside an existing immutable version are repaired; healthy files retain their
timestamps.

### Keep dependency restoration incremental

Bundle and CSS dependency restore targets track package manifests/lockfiles and
restore stamps, with checks for required installed packages. Restoration is
invoked when the corresponding asset operation needs dependencies.

`CopyChartCSS` independently depends on `RestoreCssDependencies`: deleting
`node_modules` must not break chart CSS copying when `BuildCSS` is up to date.
CSS cleanup uses portable Node filesystem operations instead of Unix-only `rm`.

CI caches NuGet/npm downloads and embeddings, retaining the existing model cache.
Publish uses the earlier dependency-graph restore through `--no-restore`, but
still performs its build and publish-specific processing. Deployment assets are
not used as CI cache substitutes for generation.

## Compatibility requirements

- Preserve existing asset paths, formats and generation hooks.
- Generate sitemaps/posters only in non-Release configurations, as before.
- Generate CSS only in Release, as before.
- Preserve the RSS nested-publish guard.
- Preserve chat's existing daily UTC publication cutoff.
- Invalidate poster inputs once per UTC calendar day to preserve scheduled
  metadata behavior. Do not normalize existing publication rules to a new clock.
- Keep native WASM compilation, the C demo and prerendering enabled.

The sitemap namespace comparison was also corrected so unchanged namespaced
XML is recognized, while replaced or duplicate URLs cause regeneration.

## Alternatives considered

- **Disable native WASM or prerendering:** rejected because it changes application
  behavior rather than removing redundant work.
- **Cache embeddings but keep unconditional generation:** rejected as incomplete;
  generator startup, nested build checks and other asset pipelines would remain.
- **Use generated files alone as target outputs:** rejected because intentionally
  unchanged output timestamps can cause repeated generation. Success stamps
  distinguish completed processing from rewritten content.

## Consequences

Unchanged builds avoid generator execution, and changed content can reuse
embeddings for unchanged text. New topics reuse shared targets instead of
copying build events. Missing assets remain recoverable.

First builds and relevant source/configuration changes still perform necessary
work. Shared input changes conservatively invalidate multiple targets. Daily
publication changes intentionally invalidate date-sensitive targets. Full
solution builds and Release publishing still include their existing expensive
compilation/native/prerendering work.

Cleaning `obj` removes generation state and can trigger regeneration. Removing
the embedding cache forces recomputation; there is no automatic cache eviction.
Pipeline/runtime changes can invalidate cached embeddings even if text is
unchanged. Future generator changes must keep input/output inventories complete.

## Development guidance

For web-only development, build the Web project instead of the full solution,
which also builds MAUI platforms and test/tool projects:

```powershell
dotnet build Web\Web.csproj
```

Do not add `--no-build` to publishing unless the publish configuration has
separately been verified; native and prerendering processing must remain enabled.

## Validation recorded

- Debug Web build and Release publish passed on Windows.
- 21 generator regression tests and 13 ChatBundle JavaScript tests passed.
- An unchanged Web build took 14.54 seconds, skipped 68 generation targets and
  executed zero generators. This is an environment-specific warm-build
  observation, not a measured percentage improvement over a baseline.
- Missing-output recovery reused 3,504 corpus embeddings and 225 search
  embeddings, reproducing original checksums.
- Publication-cutoff and poster calendar-day invalidation checks passed.
- Release CSS skipped unchanged work. With `node_modules` removed, chart copying
  restored dependencies while `BuildCSS` remained skipped.
- Each caller output override (`OutputPath`, `BaseOutputPath`, `OutDir`) was
  tested separately; all four generators retained their shared output paths.
- Published checksums, vector lengths, bundles, CSS, RSS, notices, sitemap,
  prerendered content and native WASM output were verified.

## Implementation references

- [Shared inputs and framework properties](../Directory.Build.props)
- [Shared generator and channel targets](../Directory.Build.targets)
- [Common asset targets](../CommonComponents/CommonComponents.csproj)
- [Embedding cache](../VectorSearchIndexGenerator/EmbeddingCache.cs)
- [CI workflow](../.github/workflows/build-blazor-app.yml)
