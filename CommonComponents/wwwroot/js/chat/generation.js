import { selectedModel } from "./generation-contract.js";

let worker;
let nextId = 0;
const pending = new Map();
const consentKey = `ilovedotnet-answer-consent:${selectedModel.model}`;

export async function capability() {
    if (!globalThis.isSecureContext || !navigator.gpu) {
        return { supported: false, reason: "This browser does not expose WebGPU. Original article sources remain available.", consent: false };
    }
    try {
        const adapter = await navigator.gpu.requestAdapter();
        if (!adapter || !adapter.features.has("shader-f16") || adapter.limits.maxStorageBufferBindingSize < 134217728) {
            return { supported: false, reason: "This device lacks the GPU features needed for local answers. Original article sources remain available.", consent: false };
        }
        return { supported: true, reason: "", consent: localStorage.getItem(consentKey) === "accepted", modelId: selectedModel.model_id };
    } catch (error) {
        return { supported: false, reason: `Local answer capability check failed: ${error.message}. Original sources remain available.`, consent: false };
    }
}

export async function load(callback, operation) {
    await request("load", {}, callback, operation);
    try {
        localStorage.setItem(consentKey, "accepted");
    } catch (error) {
        stop();
        throw new Error(`The model loaded, but download consent could not be remembered: ${error.message}`);
    }
}

export function generate(question, sources, history, callback, operation) {
    return request("generate", { question, sources, history }, callback, operation);
}

export async function clear() {
    stop();
    try {
        await request("clear", {});
        localStorage.removeItem(consentKey);
    } finally {
        stop();
    }
}

function request(type, payload, callback, operation) {
    if (!worker) {
        worker = new Worker(new URL("./generation-worker.js", import.meta.url), { type: "module" });
        worker.addEventListener("message", onMessage);
        worker.addEventListener("error", onError);
        worker.addEventListener("messageerror", onError);
    }
    const requestId = ++nextId;
    return new Promise((resolve, reject) => {
        pending.set(requestId, { resolve, reject, callback, operation, updates: Promise.resolve() });
        worker.postMessage({ requestId, type, ...payload });
    });
}

function onMessage({ data }) {
    const request = pending.get(data.requestId);
    if (!request) {
        return;
    }
    if (data.update) {
        request.updates = request.updates.then(() => request.callback?.invokeMethodAsync(
            "OnGenerationUpdate", request.operation, data.update.phase, data.update.text, data.update.progress ?? 0));
        return;
    }
    pending.delete(data.requestId);
    request.updates.then(() => data.error ? request.reject(data.error) : request.resolve(data.result),
        error => request.reject(error));
}

function onError(event) {
    stop(event.message || "The local answer worker failed.");
}

export function stop(reason = "Local answer generation stopped.") {
    if (worker) {
        worker.terminate();
        worker = undefined;
    }
    for (const request of pending.values()) {
        request.reject(new Error(reason));
    }
    pending.clear();
}
