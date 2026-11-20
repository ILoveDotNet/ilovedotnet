import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import test from 'node:test';

const source = await readFile(new URL('../wwwroot/js/displaymode.js', import.meta.url), 'utf8');
const theme = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`);

function setup(storedMode, systemDark) {
    const classes = new Set();
    globalThis.document = {
        documentElement: {
            classList: {
                add: name => classes.add(name),
                remove: name => classes.delete(name)
            }
        }
    };
    globalThis.window = { matchMedia: () => ({ matches: systemDark }) };
    globalThis.localStorage = {
        getItem(name) { return this[name] ?? null; },
        setItem(name, value) { this[name] = value; },
        removeItem(name) { delete this[name]; }
    };
    if (storedMode) localStorage.setItem('DisplayMode', storedMode);
    return classes;
}

test('explicit light and dark selections override the system and persist', () => {
    for (const mode of ['light', 'dark']) {
        const classes = setup(null, mode === 'light');
        theme.onDisplayModeChanged(mode);
        assert.equal(classes.has('dark'), mode === 'dark');
        assert.equal(theme.getDisplayMode(), mode);
    }
});

test('System clears the explicit preference and applies the current system appearance', () => {
    for (const systemDark of [false, true]) {
        const classes = setup(systemDark ? 'light' : 'dark', systemDark);
        theme.onDisplayModeChanged('system');
        assert.equal(classes.has('dark'), systemDark);
        assert.equal(theme.getDisplayMode(), null);
        assert.equal('DisplayMode' in localStorage, false);
    }
});
