import { env, pipeline } from "../../lib/transformers/transformers.min.js";

const embeddingModel = "Xenova/all-MiniLM-L6-v2";
const embeddingRevision = "751bff37182d3f1213fa05d7196b954e230abad9";
const dimensions = 384;
const maximumQuestionLength = 3000;
const minimumSimilarity = 0.3;
const stopWords = new Set([
    "a", "an", "and", "are", "as", "at", "be", "by", "can", "could", "define", "do", "does",
    "explain", "for", "from", "how", "i", "in", "is", "it", "its", "me", "of", "on",
    "or", "please", "should", "that", "the", "their", "them", "these", "they", "this",
    "those", "to", "was", "were", "what", "when", "where", "which", "why", "will",
    "with", "would", "you",
]);
let searchIndex;
let extractorPromise;
let applicationBase;

env.allowLocalModels = false;
env.allowRemoteModels = true;

self.addEventListener("message", async event => {
    const { requestId, type, question, maximumResults, applicationBaseUri, context = "" } = event.data;
    try {
        if (type === "initialize") {
            await initialize(applicationBaseUri);
            self.postMessage({ requestId, result: true });
            return;
        }

        if (type === "search") {
            if (typeof question !== "string" || question.trim().length === 0 || question.length > maximumQuestionLength) {
                throw new Error("Enter a question between 1 and 3000 characters.");
            }

            if (!Number.isInteger(maximumResults) || maximumResults < 1 || maximumResults > 10) {
                throw new Error("The requested result count is invalid.");
            }

            if (typeof context !== "string" || context.length > maximumQuestionLength) {
                throw new Error("The article search conversation context is invalid.");
            }

            const results = await search(question, maximumResults, context);
            self.postMessage({ requestId, result: results });
            return;
        }

        throw new Error("The local article search received an unsupported operation.");
    } catch (error) {
        self.postMessage({
            requestId,
            error: error instanceof Error ? error.message : "The local article search failed.",
        });
    }
});

async function initialize(applicationBaseUri) {
    if (!applicationBase) {
        const baseUri = new URL(applicationBaseUri);
        if (baseUri.origin !== self.location.origin || !baseUri.pathname.endsWith("/")) {
            throw new Error("The learning chat base URI is invalid.");
        }
        applicationBase = baseUri;
    }

    if (!searchIndex) {
        searchIndex = await loadCorpus();
    }

    await initializeEmbedder();
}

async function loadCorpus() {
    const manifestUrl = new URL("chat/corpus/manifest.json", applicationBase);
    const manifestResponse = await fetch(manifestUrl);
    if (!manifestResponse.ok) {
        throw new Error(`Article search index could not be loaded (HTTP ${manifestResponse.status}).`);
    }

    const manifest = await manifestResponse.json();
    if (manifest.schemaVersion !== 1
        || manifest.model !== embeddingModel
        || manifest.modelRevision !== embeddingRevision
        || manifest.dimensions !== dimensions
        || manifest.vectorFormat !== "float32-le"
        || !Number.isInteger(manifest.count)
        || manifest.count <= 0
        || typeof manifest.corpusVersion !== "string") {
        throw new Error("The article search index is incompatible with the configured embedding model.");
    }

    const chunksUrl = new URL(manifest.chunksUrl, manifestUrl);
    const vectorsUrl = new URL(manifest.vectorsUrl, manifestUrl);
    if (chunksUrl.origin !== applicationBase.origin || vectorsUrl.origin !== applicationBase.origin) {
        throw new Error("The article search index points outside this website.");
    }

    const [chunksResponse, vectorsResponse] = await Promise.all([fetch(chunksUrl), fetch(vectorsUrl)]);
    if (!chunksResponse.ok || !vectorsResponse.ok) {
        throw new Error("The article search passages or vectors could not be loaded.");
    }

    const [chunksBytes, vectorsBuffer] = await Promise.all([
        chunksResponse.arrayBuffer(),
        vectorsResponse.arrayBuffer(),
    ]);
    if (await sha256(chunksBytes) !== manifest.chunksSha256
        || await sha256(vectorsBuffer) !== manifest.vectorsSha256) {
        throw new Error("The article search files failed their integrity check.");
    }

    const passages = JSON.parse(new TextDecoder().decode(chunksBytes));
    if (!Array.isArray(passages) || passages.length !== manifest.count
        || vectorsBuffer.byteLength !== manifest.count * dimensions * Float32Array.BYTES_PER_ELEMENT) {
        throw new Error("The article search passage and vector counts do not match.");
    }

    const ids = new Set();
    for (const passage of passages) {
        if (!isValidPassage(passage) || ids.has(passage.id)) {
            throw new Error("The article search index contains an invalid or duplicate passage.");
        }
        ids.add(passage.id);
    }

    const vectors = new Float32Array(vectorsBuffer);
    if (vectors.some(value => !Number.isFinite(value))) {
        throw new Error("The article search vectors contain invalid values.");
    }

    return { passages, vectors };
}

function isValidPassage(passage) {
    if (typeof passage?.id !== "string" || passage.id.trim().length === 0
        || typeof passage.slug !== "string" || passage.slug.trim().length === 0
        || typeof passage.title !== "string" || passage.title.trim().length === 0
        || !["What", "Why", "How", "Summary"].includes(passage.section)
        || typeof passage.heading !== "string"
        || typeof passage.excerpt !== "string" || passage.excerpt.trim().length === 0
        || !Number.isInteger(passage.tokenCount) || passage.tokenCount < 1 || passage.tokenCount > 256
        || typeof passage.contentUrl !== "string") {
        return false;
    }

    const url = new URL(passage.contentUrl, applicationBase);
    return url.origin === applicationBase.origin
        && url.pathname.startsWith(new URL("blogs/", applicationBase).pathname)
        && !url.search
        && !url.hash;
}

async function initializeEmbedder() {
    if (!extractorPromise) {
        extractorPromise = pipeline("feature-extraction", embeddingModel, { revision: embeddingRevision })
            .catch(error => {
                extractorPromise = undefined;
                throw error;
            });
    }

    return extractorPromise;
}

async function search(question, maximumResults, context) {
    await initialize(applicationBase.toString());
    const extractor = await initializeEmbedder();
    const questionTerms = meaningfulTerms(question);
    const needsContext = questionTerms.size === 0 || /\b(it|its|that|this|they|their|them|these|those)\b/i.test(question);
    const input = needsContext && context.trim() ? `${context.trim()}\n${question.trim()}` : question.trim();
    const queryTerms = questionTerms.size > 0 ? questionTerms : meaningfulTerms(context.trim().split("\n").at(-1) ?? "");
    const tokenCount = extractor.tokenizer.encode(input).length;
    if (tokenCount > 256) {
        throw new Error(`This embedding model supports at most 256 question tokens; your question uses ${tokenCount}. Please shorten it.`);
    }

    const output = await extractor(input, { pooling: "mean", normalize: true });
    if (output.data.length !== dimensions) {
        throw new Error(`The embedding model returned ${output.data.length} dimensions instead of ${dimensions}.`);
    }

    const definitionQuestion = /^\s*(what\s+(is|are)|define|explain)\b/i.test(question);
    const ranked = searchIndex.passages
        .map((passage, row) => {
            const offset = row * dimensions;
            let semantic = 0;
            for (let dimension = 0; dimension < dimensions; dimension++) {
                semantic += output.data[dimension] * searchIndex.vectors[offset + dimension];
            }

            const headingMatch = termCoverage(queryTerms, passage.heading);
            const excerptMatch = termCoverage(queryTerms, passage.excerpt);
            const titleMatch = termCoverage(queryTerms, passage.title);
            const topicMatch = termCoverage(queryTerms, `${passage.heading} ${passage.excerpt} ${passage.title}`);
            const definitionBoost = definitionQuestion && excerptMatch === 1 && passage.section === "What" ? 0.15 : 0;
            return {
                passage,
                semantic,
                topicMatch,
                evidenceMatch: Math.max(headingMatch, excerptMatch),
                score: semantic * 0.5 + headingMatch * 0.45 + excerptMatch * 0.35 + titleMatch * 0.1 + definitionBoost,
            };
        })
        .filter(result => result.semantic >= minimumSimilarity);

    // When the corpus names the requested concept, unrelated semantic neighbours are not evidence.
    const hasTopicMatches = ranked.some(result => result.evidenceMatch > 0);
    const relevant = ranked
        .filter(result => !hasTopicMatches || result.evidenceMatch > 0)
        .sort((left, right) => hasTopicMatches
            ? right.topicMatch - left.topicMatch || right.score - left.score
            : right.score - left.score);

    const articleCounts = new Map();
    const selected = [];
    for (const result of relevant) {
        const count = articleCounts.get(result.passage.slug) ?? 0;
        if (count >= 3) {
            continue;
        }

        articleCounts.set(result.passage.slug, count + 1);
        selected.push({
            title: result.passage.title,
            section: result.passage.section,
            heading: result.passage.heading,
            contentUrl: result.passage.contentUrl,
            excerpt: result.passage.excerpt,
        });
        if (selected.length >= maximumResults) {
            break;
        }
    }

    return selected;
}

function meaningfulTerms(text) {
    return new Set((text.toLowerCase().match(/[a-z_][a-z0-9_]*/g) ?? [])
        .filter(term => !stopWords.has(term))
        .map(term => {
            if (term.length > 5 && term.endsWith("ing")) {
                term = term.slice(0, -3).replace(/([b-df-hj-np-tv-z])\1$/, "$1");
            } else if (term.length > 4 && term.endsWith("ies")) {
                term = `${term.slice(0, -3)}y`;
            } else if (term.length > 4 && term.endsWith("s") && !term.endsWith("ss")) {
                term = term.slice(0, -1);
            }
            return term.length > 4 && term.endsWith("e") ? term.slice(0, -1) : term;
        }));
}

function termCoverage(queryTerms, text) {
    if (queryTerms.size === 0) {
        return 0;
    }
    const terms = meaningfulTerms(text);
    return Array.from(queryTerms).filter(term => terms.has(term)).length / queryTerms.size;
}

async function sha256(buffer) {
    const digest = await crypto.subtle.digest("SHA-256", buffer);
    return Array.from(new Uint8Array(digest), value => value.toString(16).padStart(2, "0")).join("");
}
