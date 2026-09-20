import { env, pipeline } from 'https://cdn.jsdelivr.net/npm/@xenova/transformers@2.17.2';

const dimensions = 384;
const assetPath = 'search/';

let indexPromise;
let modelPromise;

env.allowLocalModels = false;
env.allowRemoteModels = true;

function assetUrl(fileName) {
    return new URL(`${assetPath}${fileName}`, document.baseURI);
}

async function initializeIndex() {
    const [metadataResponse, vectorResponse] = await Promise.all([
        fetch(assetUrl('index.json')),
        fetch(assetUrl('index.vec')),
    ]);

    if (!metadataResponse.ok || !vectorResponse.ok) {
        throw new Error('The vector search index could not be loaded.');
    }

    const entries = await metadataResponse.json();
    const buffer = await vectorResponse.arrayBuffer();
    const vectors = new Float32Array(buffer);

    if (vectors.length !== entries.length * dimensions) {
        throw new Error('The vector search index has an invalid length.');
    }

    return { entries, vectors };
}

function initializeModel() {
    if (!modelPromise) {
        modelPromise = pipeline('feature-extraction', 'Xenova/all-MiniLM-L6-v2');
    }

    return modelPromise;
}

function getIndex() {
    if (!indexPromise) {
        indexPromise = initializeIndex();
    }

    return indexPromise;
}

function normalizeText(text) {
    return text.replace(/[^\p{L}\p{N}]+/gu, ' ').trim();
}

function localDate() {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}

export async function warmUp() {
    await Promise.all([initializeModel(), getIndex()]);
}

export async function search(text, maximumResults) {
    const normalizedText = normalizeText(text);
    if (!normalizedText) {
        return [];
    }

    const [extractor, { entries, vectors }] = await Promise.all([
        initializeModel(),
        getIndex(),
    ]);
    const output = await extractor(normalizedText, { pooling: 'mean', normalize: true });
    const queryVector = output.data;
    const today = localDate();

    return entries
        .map((entry, entryIndex) => ({ entry, entryIndex }))
        .filter(candidate => candidate.entry.PublishOn <= today)
        .map(candidate => ({
            slug: candidate.entry.Slug,
            score: dotProduct(queryVector, vectors, candidate.entryIndex),
        }))
        .sort((left, right) => right.score - left.score)
        .slice(0, maximumResults)
        .map(result => result.slug);
}

function dotProduct(queryVector, vectors, entryIndex) {
    const offset = entryIndex * dimensions;
    let score = 0;

    for (let dimension = 0; dimension < dimensions; dimension++) {
        score += queryVector[dimension] * vectors[offset + dimension];
    }

    return score;
}
