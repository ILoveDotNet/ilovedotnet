import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import test from 'node:test';

const source = await readFile(new URL('../wwwroot/js/search.js', import.meta.url), 'utf8');
const search = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`);

function createInput() {
    const handlers = new Map();
    return {
        value: 'dotnet',
        expanded: true,
        handlers,
        getAttribute() { return String(this.expanded); },
        addEventListener(name, handler) { handlers.set(name, handler); },
        removeEventListener(name, handler) {
            if (handlers.get(name) === handler) handlers.delete(name);
        }
    };
}

function key(key, overrides = {}) {
    return {
        key,
        prevented: false,
        stopped: false,
        preventDefault() { this.prevented = true; },
        stopPropagation() { this.stopped = true; },
        ...overrides
    };
}

test('navigation prevents page scrolling without blocking typing or Tab', () => {
    const input = createInput();
    search.initializeSearch(input);
    const handler = input.handlers.get('keydown');
    for (const name of ['ArrowDown', 'ArrowUp', 'Enter', 'Escape']) {
        const event = key(name);
        handler(event);
        assert.equal(event.prevented, true, name);
        assert.equal(event.stopped, false);
    }
    for (const name of ['c', 'Tab', 'Home', 'End']) {
        const event = key(name);
        handler(event);
        assert.equal(event.prevented, false, name);
    }
    input.expanded = false;
    input.value = '';
    const event = key('ArrowDown');
    handler(event);
    assert.equal(event.prevented, false);
    search.disposeSearch(input);
});

test('IME composition never selects a suggestion or suppresses composition', () => {
    const input = createInput();
    search.initializeSearch(input);
    for (const overrides of [{ isComposing: true }, { keyCode: 229 }]) {
        const event = key('Enter', overrides);
        input.handlers.get('keydown')(event);
        assert.equal(event.stopped, true);
        assert.equal(event.prevented, false);
    }
    search.disposeSearch(input);
});

test('listeners are replaced and cleaned up without affecting another input', () => {
    const input = createInput();
    const other = createInput();
    search.initializeSearch(input);
    const original = input.handlers.get('keydown');
    search.initializeSearch(input);
    search.initializeSearch(other);
    assert.notEqual(input.handlers.get('keydown'), original);
    assert.equal(input.handlers.size, 1);
    search.disposeSearch(input);
    search.disposeSearch(input);
    assert.equal(input.handlers.size, 0);
    assert.equal(other.handlers.size, 1);
    search.disposeSearch(other);
});

test('focus moving to recovery actions keeps suggestions open; leaving closes them', async () => {
    const original = globalThis.document;
    const input = {};
    const retry = {};
    const outside = {};
    const root = { contains: element => element === input || element === retry };
    try {
        globalThis.document = { activeElement: input };
        const movingFocus = search.containsFocus(root);
        document.activeElement = retry;
        assert.equal(await movingFocus, true);
        document.activeElement = outside;
        assert.equal(await search.containsFocus(root), false);
    } finally {
        if (original === undefined) delete globalThis.document;
        else globalThis.document = original;
    }
});
