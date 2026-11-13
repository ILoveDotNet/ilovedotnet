import { env, pipeline } from "../../lib/transformers/transformers.min.js";
import { meaningfulTerms } from "./topic-terms.js";

const embeddingModel = "Xenova/all-MiniLM-L6-v2";
const embeddingRevision = "751bff37182d3f1213fa05d7196b954e230abad9";
const dimensions = 384;
const maximumQuestionLength = 3000;
const minimumSimilarity = 0.3;
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

    const documents = passages.map(passage => ({
        title: meaningfulTerms(passage.title),
        heading: meaningfulTerms(passage.heading),
        excerpt: meaningfulTerms(passage.excerpt),
    }));
    const frequencies = new Map();
    for (const document of documents) {
        for (const term of new Set([...document.title, ...document.heading, ...document.excerpt])) {
            frequencies.set(term, (frequencies.get(term) ?? 0) + 1);
        }
    }
    return { passages, vectors, documents, frequencies };
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
    const currentQuestion = question.trim();
    const tokenCount = extractor.tokenizer.encode(currentQuestion).length;
    if (tokenCount > 256) {
        throw new Error(`This embedding model supports at most 256 question tokens; your question uses ${tokenCount}. Please shorten it.`);
    }
    const priorQuestions = needsContext ? context.split("\n").map(line => line.trim()).filter(Boolean) : [];
    let input = currentQuestion;
    while (priorQuestions.length > 0) {
        const candidate = `${priorQuestions.join("\n")}\n${currentQuestion}`;
        if (extractor.tokenizer.encode(candidate).length <= 256) {
            input = candidate;
            break;
        }
        priorQuestions.shift();
    }
    const queryTerms = questionTerms.size > 0 ? questionTerms : meaningfulTerms(priorQuestions.at(-1) ?? "");

    const output = await extractor(input, { pooling: "mean", normalize: true });
    if (output.data.length !== dimensions) {
        throw new Error(`The embedding model returned ${output.data.length} dimensions instead of ${dimensions}.`);
    }

    const definitionQuestion = /^\s*(what\s+(is|are)|define|explain)\b/i.test(question);
    const implementationQuestion = /^\s*how\b/i.test(question);
    const weights = new Map(Array.from(queryTerms, term => [
        term, Math.log(1 + searchIndex.passages.length / (searchIndex.frequencies.get(term) ?? 1)),
    ]));
    const ranked = searchIndex.passages
        .map((passage, row) => {
            const offset = row * dimensions;
            let semantic = 0;
            for (let dimension = 0; dimension < dimensions; dimension++) {
                semantic += output.data[dimension] * searchIndex.vectors[offset + dimension];
            }

            const document = searchIndex.documents[row];
            const headingMatch = termCoverage(weights, document.heading);
            const excerptMatch = termCoverage(weights, document.excerpt);
            const titleMatch = termCoverage(weights, document.title);
            const topicMatch = termCoverage(weights, new Set([...document.heading, ...document.excerpt]));
            const definitionBoost = definitionQuestion && headingMatch >= 0.8
                ? 0.2 * queryTerms.size / Math.max(queryTerms.size, document.heading.size)
                : 0;
            const introductoryDefinitionBoost = definitionQuestion && passage.section === "What" && excerptMatch >= 0.8 ? 0.2 : 0;
            const implementationBoost = implementationQuestion && passage.section === "How" ? 0.15 : 0;
            const introductionPenalty = /^\s*(in this (article|blog|post)|this article covers)/i.test(passage.excerpt) ? 0.25 : 0;
            return {
                passage,
                semantic,
                topicMatch,
                score: semantic * 0.6 + headingMatch * 0.4 + excerptMatch * 0.4
                    + titleMatch * 0.1 + definitionBoost + introductoryDefinitionBoost + implementationBoost - introductionPenalty,
            };
        })
        .filter(result => result.semantic >= minimumSimilarity);

    // Require meaningful topic coverage; semantic-only matches need stronger evidence than keyword-assisted ones.
    const hasTopicMatches = ranked.some(result => result.topicMatch >= 0.5);
    const relevant = ranked
        .filter(result => queryTerms.size === 0
            ? result.semantic >= 0.5
            : hasTopicMatches ? result.topicMatch >= 0.5 : result.semantic >= 0.6)
        .sort((left, right) => right.score - left.score);

    // Comparisons need the individual definitions, not only passages mentioning all subjects.
    if (/\b(compare|difference|versus|vs)\b/i.test(question)) {
        const articles = new Set(relevant.map(result => result.passage.slug));
        const definitions = ranked.filter(result => {
            const heading = meaningfulTerms(result.passage.heading);
            return articles.has(result.passage.slug) && heading.size > 0
                && [...heading].every(term => queryTerms.has(term));
        }).sort((left, right) => right.score - left.score);
        relevant.unshift(...definitions);
    }

    const articleCounts = new Map();
    const selectedHeadings = new Set();
    const selected = [];
    for (const result of relevant) {
        const parentId = result.passage.id.slice(0, result.passage.id.lastIndexOf(":"));
        const headingKey = JSON.stringify([parentId, result.passage.slug, result.passage.section, result.passage.heading]);
        const count = articleCounts.get(result.passage.slug) ?? 0;
        if (count >= 3 || selectedHeadings.has(headingKey)) {
            continue;
        }

        selectedHeadings.add(headingKey);
        articleCounts.set(result.passage.slug, count + 1);
        // Keep adjacent prose/code from the same heading together, bounded to three child chunks.
        const siblings = searchIndex.passages.filter(passage => passage.slug === result.passage.slug
            && passage.section === result.passage.section && passage.heading === result.passage.heading
            && passage.id.slice(0, passage.id.lastIndexOf(":")) === parentId);
        const siblingIndex = siblings.indexOf(result.passage);
        const start = Math.max(0, siblingIndex - 1);
        selected.push({
            title: result.passage.title,
            section: result.passage.section,
            heading: result.passage.heading,
            contentUrl: result.passage.contentUrl,
            excerpt: siblings.slice(start, start + 3).map(passage => passage.excerpt).join("\n\n"),
        });
        if (selected.length >= maximumResults) {
            break;
        }
    }

    return selected;
}

function termCoverage(weights, terms) {
    if (weights.size === 0) {
        return 0;
    }
    let matched = 0;
    let total = 0;
    for (const [term, weight] of weights) {
        total += weight;
        if (terms.has(term)) {
            matched += weight;
        }
    }
    return matched / total;
}

async function sha256(buffer) {
    const digest = await crypto.subtle.digest("SHA-256", buffer);
    return Array.from(new Uint8Array(digest), value => value.toString(16).padStart(2, "0")).join("");
}
