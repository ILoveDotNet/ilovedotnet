import test from "node:test";
import assert from "node:assert/strict";
import { initialize, dispose, loadHistory, saveHistory, clearHistory } from "../../CommonComponents/wwwroot/js/learning-chat.js";

test("navigation cleanup uses retained elements and is safe after DOM removal or repeated disposal", () => {
    for (let visit = 0; visit < 2; visit++) {
        const input = new EventTarget();
        const conversation = new EventTarget();
        input.value = "What is TDD?";
        let submissions = 0;
        input.form = { requestSubmit: () => submissions++ };
        conversation.scrollHeight = 500;
        conversation.scrollTop = 0;
        conversation.clientHeight = 100;
        initialize(input, conversation);
        const enter = () => {
            const event = new Event("keydown", { cancelable: true });
            Object.defineProperty(event, "key", { value: "Enter" });
            input.dispatchEvent(event);
        };
        enter();
        assert.equal(submissions, 1);
        assert.equal(conversation.scrollTop, 0, "Initialization must not move the reader's scroll position.");

        // A removed ElementReference is null at the .NET/JS boundary.
        dispose(null);
        assert.doesNotThrow(() => dispose());
        enter();
        assert.equal(submissions, 1, "The original input listener must be removed.");
        conversation.scrollTop = 0;
        conversation.dispatchEvent(new Event("scroll"));
        assert.equal(conversation.scrollTop, 0, "The old scroll listener and state must be removed.");
    }
});

test("history is stored locally, removed explicitly, and storage failures are not swallowed", () => {
    const values = new Map();
    globalThis.localStorage = {
        getItem: key => values.get(key) ?? null,
        setItem: (key, value) => values.set(key, value),
        removeItem: key => values.delete(key),
    };
    try {
        assert.equal(loadHistory(), null);
        const history = '{"Version":1,"Conversations":[]}';
        saveHistory(history);
        assert.equal(loadHistory(), history);
        clearHistory();
        assert.equal(loadHistory(), null);
        globalThis.localStorage.setItem = () => { throw new Error("Quota exceeded"); };
        assert.throws(() => saveHistory(history), /Quota exceeded/);
        globalThis.localStorage.getItem = () => { throw new Error("Storage blocked"); };
        assert.throws(() => loadHistory(), /Storage blocked/);
    } finally {
        delete globalThis.localStorage;
    }
});
