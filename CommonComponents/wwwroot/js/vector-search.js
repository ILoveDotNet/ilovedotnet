import { env, pipeline } from '../lib/transformers/transformers.min.js';

const dimensions = 384;
const minimumScore = 0.25;
const model = 'Xenova/all-MiniLM-L6-v2';
const modelRevision = '751bff37182d3f1213fa05d7196b954e230abad9';
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

    const index = await metadataResponse.json();
    const buffer = await vectorResponse.arrayBuffer();
    const vectors = new Float32Array(buffer);
    const entries = index.Entries;

    if (index.Model !== model
        || index.ModelRevision !== modelRevision
        || index.Dimensions !== dimensions
        || !Array.isArray(entries)
        || index.Count !== entries.length
        || !entries.every(isValidEntry)
        || vectors.length !== index.Count * dimensions) {
        throw new Error('The vector search index is incompatible with the configured embedding model.');
    }

    return { entries, vectors };
}

function isValidEntry(entry) {
    return typeof entry?.Slug === 'string'
        && entry.Slug.trim().length > 0
        && typeof entry.PublishOn === 'string'
        && isValidDate(entry.PublishOn);
}

function isValidDate(value) {
    const match = /^(\d{4})-(\d{2})-(\d{2})$/.exec(value);
    if (!match) {
        return false;
    }

    const year = Number(match[1]);
    const month = Number(match[2]);
    const day = Number(match[3]);
    const date = new Date(Date.UTC(year, month - 1, day));
    return date.getUTCFullYear() === year
        && date.getUTCMonth() === month - 1
        && date.getUTCDate() === day;
}

function initializeModel() {
    if (!modelPromise) {
        modelPromise = pipeline('feature-extraction', model, { revision: modelRevision })
            .catch(error => {
                modelPromise = undefined;
                throw error;
            });
    }

    return modelPromise;
}

function getIndex() {
    if (!indexPromise) {
        indexPromise = initializeIndex()
            .catch(error => {
                indexPromise = undefined;
                throw error;
            });
    }

    return indexPromise;
}

function normalizeText(text) {
    return text.replace(/\p{S}/gu, ' ').replace(/\s+/g, ' ').trim();
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
    if (queryVector.length !== dimensions) {
        throw new Error(`The embedding model returned ${queryVector.length} dimensions instead of ${dimensions}.`);
    }

    return rank(queryVector, entries, vectors, maximumResults);
}

function rank(queryVector, entries, vectors, maximumResults) {
    const today = localDate();

    return entries
        .map((entry, entryIndex) => ({ entry, entryIndex }))
        .filter(candidate => candidate.entry.PublishOn <= today)
        .map(candidate => ({
            slug: candidate.entry.Slug,
            score: dotProduct(queryVector, vectors, candidate.entryIndex),
        }))
        .filter(candidate => candidate.score >= minimumScore)
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
