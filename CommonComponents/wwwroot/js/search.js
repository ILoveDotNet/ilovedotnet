const keyboardHandlers = new WeakMap();

export function initializeSearch(input) {
    disposeSearch(input);
    const handler = event => {
        if (event.isComposing || event.keyCode === 229) {
            event.stopPropagation();
            return;
        }
        const expanded = input.getAttribute("aria-expanded") === "true";
        const hasQuery = input.value.trim().length > 0;
        if (event.key === "Escape" || event.key === "Enter"
            || ((event.key === "ArrowDown" || event.key === "ArrowUp") && (expanded || hasQuery))) {
            event.preventDefault();
        }
    };
    input.addEventListener("keydown", handler);
    keyboardHandlers.set(input, handler);
}

export function disposeSearch(input) {
    const handler = keyboardHandlers.get(input);
    if (handler) {
        input.removeEventListener("keydown", handler);
        keyboardHandlers.delete(input);
    }
}

export function containsFocus(root) {
    return new Promise(resolve => setTimeout(() => resolve(root.contains(document.activeElement)), 0));
}

export function scrollOptionIntoView(id) {
    requestAnimationFrame(() => document.getElementById(id)?.scrollIntoView({ block: "nearest" }));
}