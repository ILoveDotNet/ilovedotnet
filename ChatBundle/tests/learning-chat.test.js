import test from "node:test";
import assert from "node:assert/strict";
import { initialize, dispose, scrollToLatest } from "../../CommonComponents/wwwroot/js/learning-chat.js";

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
        scrollToLatest(conversation);
        assert.equal(conversation.scrollTop, 500);

        // A removed ElementReference is null at the .NET/JS boundary.
        dispose(null);
        assert.doesNotThrow(() => dispose());
        enter();
        assert.equal(submissions, 1, "The original input listener must be removed.");
        conversation.scrollTop = 0;
        conversation.dispatchEvent(new Event("scroll"));
        scrollToLatest(conversation);
        assert.equal(conversation.scrollTop, 0, "The old scroll listener and state must be removed.");
    }
});
