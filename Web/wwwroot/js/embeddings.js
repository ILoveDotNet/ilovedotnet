import { env, pipeline } from 'https://cdn.jsdelivr.net/npm/@xenova/transformers@2.17.2';

let extractor;

export async function initModel(model) {
    if (!extractor) {
        extractor = await pipeline('feature-extraction', model);
    }
}

export async function embed(text) {
    if (!extractor) {
        throw new Error('The embedding model has not been initialized.');
    }

    const output = await extractor(text, { pooling: 'mean', normalize: true });
    return Array.from(output.data);
}
