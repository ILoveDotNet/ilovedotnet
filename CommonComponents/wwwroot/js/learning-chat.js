const keyHandlers = new WeakMap();

export function initialize(input) {
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
}

export function scrollToLatest(conversation) {
    conversation.scrollTop = conversation.scrollHeight;
}

export function dispose(input) {
    input.removeEventListener("keydown", keyHandlers.get(input));
    keyHandlers.delete(input);
}
