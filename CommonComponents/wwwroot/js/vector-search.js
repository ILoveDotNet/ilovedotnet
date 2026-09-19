import { env, pipeline } from 'https://cdn.jsdelivr.net/npm/@xenova/transformers@2.17.2';

const dimensions = 384;
const assetPath = 'search/';

let extractor;
let indexPromise;

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

async function initializeModel() {
    if (!extractor) {
        extractor = await pipeline('feature-extraction', 'Xenova/all-MiniLM-L6-v2');
    }
}

export async function search(text, maximumResults) {
    await initializeModel();
    indexPromise ??= initializeIndex();

    const [{ entries, vectors }, output] = await Promise.all([
        indexPromise,
        extractor(text, { pooling: 'mean', normalize: true }),
    ]);
    const queryVector = output.data;

    return entries
        .map((entry, entryIndex) => ({ slug: entry.Slug, score: dotProduct(queryVector, vectors, entryIndex) }))
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
