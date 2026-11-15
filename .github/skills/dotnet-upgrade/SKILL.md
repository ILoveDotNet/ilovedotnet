---
name: dotnet-upgrade
description: >-
  Upgrade this repository to a newer .NET version: update target frameworks first,
  upgrade NuGet packages with dotnet outdated -u, align Dockerfiles and workflow
  pipelines, then fix upgrade-related failures until all required builds and tests
  pass.
---

# .NET Upgrade

Keep the upgrade focused. Preserve existing work and both Blazor WebAssembly and
.NET MAUI compatibility. Do not perform unrelated refactoring or rewrite historical
blog code samples.

1. **Discover and confirm the target.** Find all projects, effective
   `TargetFramework`/`TargetFrameworks` values, shared MSBuild properties, SDK pins,
   package manifests, Dockerfiles, and pipelines. Use the explicitly requested
   version, or resolve the latest stable .NET release from Microsoft's support
   policy: https://dotnet.microsoft.com/platform/support/policy/dotnet-core.
   Confirm an ambiguous target; do not assume latest stable means latest LTS.
   Verify the target SDK and required WASM/MAUI workloads are available.
   Use the current feature branch and preserve existing changes.
2. **Read the relevant migration documentation.** Before editing, review
   Microsoft's breaking-change documentation for the current-to-target upgrade:
   https://learn.microsoft.com/dotnet/core/compatibility/.
   Include intervening releases when skipping versions, and read the official
   migration guides or release notes for affected packages and workloads.
   Identify changes that apply to this solution and use the documented migration
   steps during the upgrade. If an update exposes another breaking change, read
   its respective migration documentation before proceeding; do not guess fixes.
3. **Update all in-scope TFMs before packages.** Start with
   `Directory.Build.props`, then update project overrides, including production,
   generators, and test projects. Update `MAUI\MAUI.csproj` multi-targeting,
   TFM-dependent conditions, and active paths that embed the old TFM. Preserve
   platform suffixes, OS-version qualifiers, conditional targets, and intentional
   compatibility targets. Update SDK pins in `global.json` if present, preserving
   its test-runner settings. Assess unsupported project types separately rather
   than blindly replacing every framework string.
4. **Upgrade NuGet packages.** Follow `.github\skills\nuget-manager\SKILL.md`.
   Check that `dotnet-outdated-tool` is available; install it only if missing,
   using `dotnet tool install --global dotnet-outdated-tool`, or restore a
   repository-local tool manifest if present. Run `dotnet outdated -u` at the
   repository root; pass `ILoveDotNet.slnx` explicitly if solution selection is
   ambiguous. For stable-only upgrades, add `--pre-release Never`.
   Include projects outside the solution with explicit project commands.
   If the solution-level upgrade fails, investigate and retry affected projects
   in dependency order, dependencies first. Versions are centrally managed in
   `Directory.Packages.props`; verify published versions and restore after updates.
   Keep related ASP.NET Core packages aligned with the selected framework release.
   Review major-version changes and lock files; do not silently ignore failures.
   Check the Aspire AppHost SDK and integration packages too; use `aspire update`
   if required by the applicable Aspire upgrade guidance.
5. **Update Dockerfiles.** Align .NET SDK and runtime image tags with the target,
   including `Web\Dockerfile` and image references in Compose files. Verify tags
   exist and build affected images with the correct context. Preserve image
   families, OS variants, and architecture unless a change is necessary.
   The Web app is served by nginx: do not replace its final image with a .NET
   runtime image just because the build SDK changed.
6. **Update workflow pipelines.** Review all `.github\workflows` files for SDK
   versions, workloads, and framework-specific build/test/publish paths.
   In particular, align the SDK and generated Playwright script path in
   `.github\workflows\build-blazor-app.yml`. Update deployment runtime settings,
   SDK pins, and directly affected documentation without unrelated action upgrades.
7. **Make all required builds and tests pass.** Run
   `dotnet restore ILoveDotNet.slnx`, `dotnet build ILoveDotNet.slnx --no-restore`,
   and `dotnet build Web/Web.csproj`. Validate MAUI targets on hosts with the
   required platform tooling; a Web build alone does not validate native targets.
   Run `dotnet test` and verify it covers every required test project.
   Include `UITests`, `VectorSearchIndexGenerator.Tests`,
   `ILoveDotNet.DistributedTest`, and the separate
   `E2E\EndToEndTests\EndToEndTests.csproj` suite using its compatible runner.
   Follow the repository's test setup and CI commands for Playwright browser
   installation, Aspire services, and application endpoints. Run
   `dotnet format --verbosity quiet whitespace --folder` and review its diff.
   Fix upgrade-related errors, consulting Microsoft's breaking-change guidance,
   then rerun affected checks and the full required validation. Do not skip,
   disable, or weaken tests to claim success. Verify CI before declaring completion.
   If tooling, permissions, infrastructure, or compatibility blocks validation,
   report the failing command, cause, and required next action.
8. **Review and deliver.** Report the selected target, changed files, package
   updates, validation results, and remaining risks. Commit, push, or open a PR
   only when requested or authorized, following repository Git conventions.

Planning or assessment alone does not authorize implementation or Git changes.
