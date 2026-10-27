# Browser-Local Learning Chat - Implementation Plan

> Status: implemented browser-local preview; bounded development validation completed.
> Updated: 2026-10-27.
> This is the current implementation contract, not general factuality or release sign-off.
> Superseded designs and retired experiments remain in Git history.

## 1. Scope and approved behavior

The **Learn with AI** page at `/chat/` uses this pipeline:

**Question -> browser-local article retrieval -> bounded evidence -> WebLLM
explanation -> streamed draft -> validated citations and original sources.**

WebLLM reads selected article passages, not every full article or arbitrary URLs.
The website remains a static deployment without an inference API, vector database,
API key or native model server. The page and navigation are available without a
feature flag. Header vector search remains independent and unchanged.

| Requirement | Implemented contract |
| --- | --- |
| SIMPLE-001 | Explain concepts naturally from supplied article passages. Paraphrasing is allowed; exact quotations and quote-bound JSON are not required. |
| SIMPLE-002 | Preserve heading/body context, useful lists and bounded code when packing evidence. Respect embedding and generation limits. |
| SIMPLE-003 | Require valid evidence IDs at the end of each answer paragraph. Resolve links from application-owned source metadata, never generated URLs. Citations establish provenance, not factual correctness. |
| SIMPLE-004 | Safely render provisional text, then validate completion and citations. Refused, incomplete, invalid or failed output retains original sources and an explicit status. |
| SIMPLE-005 | Preserve automatic retrieval, remembered generation-download consent, active model display, stop/retry/new-conversation controls, source cards, theme support and existing search/navigation. No cloud inference. |
| SIMPLE-006 | Keep the shared page, scripts, bundles, corpus assets and build ownership in `CommonComponents`. Generation-unsupported devices retain source-only search. |
| SIMPLE-007 | Demonstrate the TDD Red question through the real production path and record bounded regression results and semantic failures honestly. |

Additional user-approved workspace behavior:

- The left sidebar contains **New conversation**, saved conversations, selection,
  individual deletion, confirmed delete-all and model controls.
- The center shows the active conversation with a fixed composer and independently
  scrollable messages. On mobile, **Chats & model** toggles the sidebar.
- Scrolling is **manual only**, including during streaming and after long answers;
  native scroll anchoring is disabled for the message region.
- Article links remain visible; long source excerpts are expandable.
- Enter sends; Shift+Enter inserts a newline; IME composition does not submit.
- Conversations persist in this browser profile until deleted, subject to browser
  storage availability. New conversation preserves previous chats.
- There are no accounts, server-stored conversations, sharing or cross-device sync.

Out of scope: autonomous agents, tool calling, code execution, arbitrary browsing,
fine-tuning, cloud/native inference fallback, automatic semantic verification,
model picker, full offline/PWA support and guaranteed generation on every device.

## 2. Ownership and implementation map

Paths below are relative to the repository root.

| Area | Files and responsibility |
| --- | --- |
| Shared page | `CommonComponents/Pages/LearningChat.razor` and `.razor.cs`: workspace, orchestration, safe rendering, lifecycle and history |
| History contract | `CommonComponents/Models/LearningChatHistory.cs` and `LearningChatJsonContext.cs`: versioned persisted records and trim-compatible JSON |
| Retrieval bridge | `CommonComponents/wwwroot/js/learning-chat.js`: keyboard handling, localStorage access, worker requests and disposal |
| Retrieval worker | `CommonComponents/wwwroot/js/chat/retrieval-worker.js` and `topic-terms.js`: corpus validation, embeddings, ranking and context expansion |
| Generation | `CommonComponents/wwwroot/js/chat/generation.js`, `generation-worker.js`, `generation-contract.js`: capability/consent, worker lifecycle, prompts, streaming and validation |
| Runtime bundle | `ChatBundle/`: pinned WebLLM dependency, lockfile, bundle entry point and production-contract Node tests |
| Corpus generator | `VectorSearchIndexGenerator/ChatCorpusGenerator.cs`, `ChatArticleExtractor.cs`, `ChatCorpusJsonContext.cs`, `MiniLmEmbedder.cs` and `Program.cs --chat` |
| Build integration | `CommonComponents/CommonComponents.csproj`: shared runtime bundles, corpus generation and static-asset inclusion |
| Styling | `CommonComponents/Styles/ilovedotnet.css` through the existing shared CSS build to `wwwroot/css/app.min.css`; no Web-only or isolated chat stylesheet |
| Notices | `THIRD-PARTY-NOTICES.txt`: runtime, embedding and generation model notices |

Generated assets live under `CommonComponents/wwwroot`:

```text
chat/corpus/manifest.json
chat/corpus/versions/{corpusVersion}/chunks.json
chat/corpus/versions/{corpusVersion}/chunks.vec
lib/webllm/webllm.js
lib/transformers/transformers.min.js
```

Web and MAUI consume shared components/assets; no Web-owned chat page, scripts or
inference service are required. **MAUI WebView runtime parity remains unverified.**
Shared navigation exposes chat on desktop and in the mobile overflow menu.
The page has canonical/social/structured metadata, `noindex,follow`, and an entry
in the static sitemap. Conversations have no public routes or sitemap entries.

## 3. Corpus and retrieval

### Build-time extraction and publication

- Read catalogue entries from `TableOfContents.AllContents`, then include only
  `/blogs/...` articles whose `ModifiedOn` date is at or before the publication
  cutoff. Scheduled content is excluded before writing public assets.
- Resolve each eligible route to Razor source. Missing sources and duplicate
  routes fail generation explicitly.
- Extract static What, Why, How and Summary content using AngleSharp; preserve
  heading context, decoded text and fenced code. Demo placeholders are not evidence;
  unsupported dynamic content is rejected rather than silently indexed.
- Group content by section/heading and split into child passages. Count the
  complete embedding prefix plus excerpt with the model tokenizer; each input
  must fit the 256-token limit without silent truncation.
- Use pinned `Xenova/all-MiniLM-L6-v2`, revision
  `751bff37182d3f1213fa05d7196b954e230abad9`, with normalized 384-dimensional vectors.
- Write schema-versioned metadata, float32 little-endian vectors and SHA-256
  hashes. Stage a content-addressed version before replacing the manifest;
  remove stale generated versions.
- The shared project tracks input inventory and publication cutoff for incremental
  regeneration, including source deletion and date changes. `ChatCorpusCutoff`
  defaults to the UTC build date and can be overridden.
- Preserve the existing metadata-search generator invocation and outputs.

### Runtime retrieval

Opening the interactive chat page automatically loads the corpus and embedding
model, with download disclosure, cancel and retry. There is no retrieval-enable
button. Initial uncached model downloads require network access.

The worker validates schema/model compatibility, dimensions, counts, hashes,
unique passage IDs, finite vectors and application-owned blog URLs before use.
Ranking combines cosine similarity with corpus-weighted lexical overlap,
definition/implementation boosts and introduction-boilerplate penalties.
Keyword-assisted matches require at least 0.3 cosine similarity and 50% weighted
heading/body topic coverage; semantic-only matches require stronger similarity.

Named topic changes retrieve independently; pronoun follow-ups use bounded prior
question context. Comparison questions prioritize individual matching definition
headings within relevant articles. Results are limited to six sources and three
per article, deduplicated by parent/heading. Up to three adjacent child chunks
from the same parent are combined in source order.

No evidence is a valid outcome, not permission to invent sources. Original article
passages remain usable without downloading a generation model.

## 4. Generation, citations and lifecycle

### Model and consent

| Setting | Current value |
| --- | --- |
| Runtime | `@mlc-ai/web-llm` 0.2.86, bundled locally with pinned dependencies |
| Default model | `Qwen3-4B-q4f16_1-MLC`; no picker |
| Model revision | `a5c9fab855e3ccbdfed2e7e69683d75f30332161` |
| WASM library revision | `00fe7a40e57e5e8eb9fef87526a4bf28ad5846c0` |
| Weight size | 2,262,920,192 bytes, about 2.26 GB, plus tokenizer/runtime downloads |
| Context / output | 8,192-token context; at most 512 output tokens |
| Prompt content | At most 6,000 UTF-8 bytes; at most six evidence slots and 1,600 bytes per excerpt |
| Decoding | Temperature 0, seed 42, thinking disabled |

The first generation-model download requires explicit consent. Successful consent
is remembered by immutable model URL; consent for a different revision/model does
not authorize this one. Opening chat alone does not download the generation model.
The page displays the active model after loading. Clearing its cache is separate
from deleting conversations.

Capability checks require a secure context, WebGPU, `shader-f16` and at least a
128 MiB storage-buffer binding limit. Passing these checks does **not** guarantee
enough GPU memory. Actual memory and latency depend on the device and context;
the model needs several GB of GPU memory plus browser/runtime overhead.

### Prompt and output contract

- Pack relevant excerpt bodies with titles/headings, preserving source slots even
  when an oversized excerpt is skipped. Keep complete fitting sentences, useful
  lists and complete bounded code fences.
- Include bounded previous user questions for context, not generated claims as
  authoritative evidence.
- Treat user text and article content as untrusted data. Ask for one plain-text
  paragraph of about 60-100 words, using only supplied evidence and citing it.
  Unsupported evidence should produce exactly `INSUFFICIENT_EVIDENCE`.
- Reject a requested qualified method call absent from packed excerpt bodies
  before inference. This is a narrow API-presence check, not semantic verification.
- Safely encode streamed drafts and label them provisional. Strip only the known
  empty thinking wrapper; do not expose nonempty reasoning as an answer.
- Final validation requires normal completion, nonblank in-range evidence IDs and
  paragraph-end citations; limits are three paragraphs and 150 words. Reject
  generated URLs/HTML, malformed citations, refusal mixed with advice and truncation.
- Do not perform a repair/second inference pass. Refusals, validation failures and
  runtime errors retain original sources with an explicit status.
- Resolve final citation IDs through that message's trusted source metadata.
  Targets use existing `#what`, `#why`, `#how` or `#summary` anchors, not invented
  heading slugs or exact chunk anchors.

**Valid citations are not entailment checks.** A model can cite a supplied passage
and still explain it incorrectly. The UI asks readers to check original articles.

Retrieval and generation run in separate workers. Operation/request IDs reject
stale callbacks after cancellation, reset or navigation. Stop terminates active
work, not merely rendering. Disposal retains original initialized DOM elements
for listener removal, stops workers and releases interop references; it does not
re-resolve removed Blazor element references.

## 5. History and privacy

History uses localStorage key `ilovedotnet-learning-chat-history:v1` and a versioned,
source-generated JSON contract. It stores questions, completed replies, source
metadata and the selected conversation; provisional drafts are not persisted as
completed generated answers.

Restored links must be application-owned blog paths with valid sections.
Malformed/incompatible history is reported and not silently overwritten.
Quota or storage-access errors are visible while the current-page chat remains
usable. There is no automatic history pruning or retention cap.

Questions, prompt assembly, article retrieval scoring and inference run locally;
there is no chat upload/inference endpoint or server history sync. Download hosts
still receive ordinary asset requests. The sidebar warns that anyone sharing the
browser profile can read saved chats. Clearing browser data may remove history,
consent and cached models. A full third-party telemetry/privacy audit remains open;
do not equate local inference with an independently verified site-wide privacy claim.

## 6. Completed implementation and acceptance

The original approved task IDs are retained here for traceability, not as pending work.

| Phase / tasks | Delivered result |
| --- | --- |
| P1 / T001 | Added TDD Red and bounded regression cases to retrieval data and the production generation benchmark |
| P2 / T002-T003 | Preserved lists/code and stable evidence slots; prioritized comparison definitions. TDD source evidence already existed, so no additional extractor/chunker change was needed |
| P3 / T004-T005 | Enabled composed answers with bounded prompts and pinned Qwen3; updated notices/consent disclosures |
| P4 / T006 | Safe provisional/final rendering, trusted citations, explicit fallback and lifecycle handling |
| P5 / T007-T009 | Updated contract/browser tests, exercised real retrieval/generation and documented results and limitations |

The acceptance question is **"what is red phase in TDD?"**:
retrieve the failing-test explanation, compose an answer explaining a failing
test before implementation, observe a draft and open a supporting article's
existing anchor. A refusal on this supported acceptance case is not success.
The wording is not hard-coded or matched as an exact expected string.

### Recorded development results

These are historical measured results, not tests rerun by this documentation update.

- Real MiniLM retrieval: **43/43 supported and 8/8 unsupported cases passed**.
- The real production Qwen3 TDD acceptance test passed: retrieval, provisional
  streaming, relevant composed answer and citation navigation to
  `introducing-tdd-in-csharp-dotnet/#how`. The fabricated API case fell back
  before inference.
- Workspace validation included desktop light/dark/mobile inspection, manual
  scrolling, persisted conversations and citations, deletion and storage failures.
- After retiring the alternate experimental harness: **9 Node tests and 23 focused
  chat browser tests passed**, with a successful test-project build. Earlier
  header-search regressions passed unchanged; an intermittent header-search timeout
  occurred in one broader development run.
- The exact Release/GitHub Pages publish command below passed, including
  prerendering and trimmed history serialization, before the test-only cleanup.
  Builds/publish reported existing warnings, not a warning-free baseline.

The retained production benchmark writes
`ILoveDotNet.DistributedTest/bin/Debug/net10.0/TestResults/learning-chat-production-generation.json`
with evidence, answers, trusted citation URLs, streaming observations and raw output.
Its hard assertions cover TDD/citation/fallback behavior; the other diagnostic
answers are **not all correctness-approved**:

| Question | Observed outcome / limitation |
| --- | --- |
| TDD Red | Correctly explains a failing test before implementation; supported article citation; some repetition |
| GC sweeping | Explains reclaiming dead-object memory without moving surviving objects, consistent with supplied evidence |
| Transient/scoped/singleton comparison | Source-only fallback despite all three definitions being supplied; confused wording and uncited recommendation were rejected |
| JsonNode property mutation | Composed and cited; calling `AsObject()` a "cast" is loose terminology |
| Value objects | Repeats an overbroad source claim that every value object must belong to an entity; not technical correctness approval |
| `GC.EnableTeleportation()` | Explicit source-only fallback before inference because the API is absent |

The alternate evidence-selection and quote-bound JSON experiments were retired.
Their central lesson remains: valid IDs or exact quote binding do not establish
claim correctness. Use tests of the production path, not an alternate prompt/worker.

## 7. Build and validation commands

Run commands from the repository root. Tests require the repository's .NET 10 /
xUnit v3 / Microsoft.Testing.Platform setup and existing Playwright prerequisites.
Use focused selectors rather than the whole solution by default.

```bash
# Production prompt, citation, disposal and localStorage contracts.
npm test --prefix ChatBundle

# Extraction and token-bounded chunking.
dotnet test --project VectorSearchIndexGenerator.Tests/VectorSearchIndexGenerator.Tests.csproj

# Build shared assets and Web before browser tests; AppHost runs Web with --no-build.
dotnet build Web/Web.csproj

# Deterministic browser regressions, including independent header search.
HEADED=1 \
ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL=https://127.0.0.1:0 \
ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL=https://127.0.0.1:0 \
dotnet test --project ILoveDotNet.DistributedTest/ILoveDotNet.DistributedTest.csproj \
  --filter-class '*LearningChatTests' \
  --filter-class '*LearningChatGenerationTests' \
  --filter-class '*LearningChatCorpusValidationTests' \
  --filter-class '*VectorSearchTests'

# Exact pipeline publish, including prerendering.
dotnet publish ./Web/Web.csproj -c:Release -p:GHPages=true -o dist/Web --nologo
```

Real-model evaluation is explicit and requires network downloads; generation needs
hardware WebGPU and several GB of available GPU memory. It is not a cheap unit test:

```bash
HEADED=1 \
ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL=https://127.0.0.1:0 \
ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL=https://127.0.0.1:0 \
dotnet test --project ILoveDotNet.DistributedTest/ILoveDotNet.DistributedTest.csproj \
  --filter-class '*LearningChatEvaluationTests' \
  --filter-class '*LearningChatGenerationBenchmarkTests' --explicit on
```

Optionally set `LEARNING_CHAT_BENCHMARK_PROFILE` to a dedicated Chromium profile
directory to reuse the generation model cache. The retrieval evaluation asserts
at least 90% supported recall and unsupported abstention, plus critical first-result
checks; it does not evaluate generated claim correctness.

Normal builds own corpus generation. For a reproducible explicit cutoff:

```bash
dotnet build CommonComponents/CommonComponents.csproj -p:ChatCorpusCutoff=2026-10-27
```

Do not hand-edit generated corpus, bundles or CSS. Regenerate through their build
paths. Model/runtime changes must update pins, notices, size/memory disclosures,
consent identity and production evaluations together.

## 8. Remaining work and release boundaries

- **Answer quality:** address DI comparison failures and review source inaccuracies
  separately. Broader independent claim-level correctness/coverage review remains
  required before general factuality sign-off; current tests do not provide it.
- **Device/runtime parity:** validate MAUI WebView and a broader browser/device
  matrix, including device loss, memory pressure, interrupted downloads and storage
  eviction. Capability checks alone are not a supported-device certification.
- **Performance/model size:** measure cold/warm startup, memory and answer latency
  on target devices. A smaller model needs a separate production-path quality
  evaluation before replacing the default; code cleanup does not reduce its weights.
- **Privacy/deployment:** complete telemetry review and validate deployed base-path,
  host/CORS/CSP and cache behavior beyond the recorded development/publish checks.
- **Optional improvements:** precise subsection/chunk anchors and reloadable offline
  behavior are deferred, not prerequisites already delivered by this implementation.
- **Maintenance:** regenerate on article/publication changes and rerun relevant
  production evaluations after content, ranking, prompt, runtime or model changes.
  Preserve header search, source fallback and matched corpus/runtime/model contracts.

No feature flag, automatic entailment model, repair pass or new model-comparison
framework is required to maintain the current approved experience.
