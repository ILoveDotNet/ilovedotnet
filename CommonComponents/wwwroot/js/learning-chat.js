const keyHandlers = new Map();
const conversationByInput = new WeakMap();
const scrollHandlers = new WeakMap();
const nearBottom = new WeakMap();
const pending = new Map();
let worker;
let nextRequestId = 0;
let applicationBaseUri;

export function initialize(input, conversation) {
    const handler = event => {
        if (event.key !== "Enter" || event.shiftKey || event.isComposing || event.keyCode === 229) {
            return;
        }

        event.preventDefault();
        if (!event.repeat && input.value.trim()) {
            input.form.requestSubmit();
        }
    };

    input.addEventListener("keydown", handler);
    keyHandlers.set(input, handler);
    conversationByInput.set(input, conversation);
    nearBottom.set(conversation, true);

    const scrollHandler = () => {
        nearBottom.set(conversation, conversation.scrollHeight - conversation.scrollTop - conversation.clientHeight <= 96);
    };
    conversation.addEventListener("scroll", scrollHandler, { passive: true });
    scrollHandlers.set(conversation, scrollHandler);
}

export function scrollToLatest(conversation) {
    if (nearBottom.get(conversation)) {
        conversation.scrollTop = conversation.scrollHeight;
    }
}

export async function enableRetrieval(baseUriValue) {
    if (worker) {
        return;
    }

    let baseUri;
    try {
        baseUri = new URL(baseUriValue);
    } catch {
        throw new Error("The learning chat base URI is invalid.");
    }
    if (baseUri.origin !== location.origin || !baseUri.pathname.endsWith("/")) {
        throw new Error("The learning chat base URI is invalid.");
    }

    applicationBaseUri = baseUri.toString();
    worker = new Worker(new URL("./chat/retrieval-worker.js", import.meta.url), { type: "module" });
    worker.addEventListener("message", onWorkerMessage);
    worker.addEventListener("error", onWorkerFailure);
    worker.addEventListener("messageerror", onWorkerFailure);
    try {
        await sendRequest("initialize", { applicationBaseUri });
    } catch (error) {
        stopWorker();
        throw error;
    }
}

export function search(question, maximumResults, context = "") {
    return (async () => {
        if (!worker) {
            if (!applicationBaseUri) {
                throw new Error("Enable local article search before asking a question.");
            }
            await enableRetrieval(applicationBaseUri);
        }
        return sendRequest("search", { question, maximumResults, context });
    })();
}

export function stop() {
    stopWorker();
}

export function dispose() {
    // Blazor element references may already resolve to null during navigation.
    for (const [input, handler] of keyHandlers) {
        input.removeEventListener("keydown", handler);
        const conversation = conversationByInput.get(input);
        if (conversation) {
            conversation.removeEventListener("scroll", scrollHandlers.get(conversation));
            scrollHandlers.delete(conversation);
            nearBottom.delete(conversation);
            conversationByInput.delete(input);
        }
    }
    keyHandlers.clear();

    stopWorker();
}

function sendRequest(type, payload) {
    const requestId = ++nextRequestId;
    return new Promise((resolve, reject) => {
        pending.set(requestId, { resolve, reject });
        worker.postMessage({ requestId, type, ...payload });
    });
}

function onWorkerMessage(event) {
    const { requestId, result, error } = event.data;
    const request = pending.get(requestId);
    if (!request) {
        return;
    }

    pending.delete(requestId);
    if (error) {
        request.reject(new Error(error));
    } else {
        request.resolve(result);
    }
}

function onWorkerFailure(event) {
    const failure = new Error(event.message || "The local article search worker stopped unexpectedly.");
    for (const request of pending.values()) {
        request.reject(failure);
    }
    pending.clear();
    stopWorker();
}

function stopWorker() {
    if (worker) {
        worker.removeEventListener("message", onWorkerMessage);
        worker.removeEventListener("error", onWorkerFailure);
        worker.removeEventListener("messageerror", onWorkerFailure);
        worker.terminate();
        worker = undefined;
    }

    for (const request of pending.values()) {
        request.reject(new Error("The local article search was stopped."));
    }
    pending.clear();
}
