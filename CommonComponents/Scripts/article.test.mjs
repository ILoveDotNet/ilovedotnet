import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import test from 'node:test';

const source = await readFile(new URL('../../BaseComponents/wwwroot/article.js', import.meta.url), 'utf8');
const article = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`);

function setup(sections, outsideIds = []) {
    const headings = sections.map(section => ({
        id: '',
        tagName: 'H4',
        closest() { return this.excluded ? {} : null; },
        hasAttribute(name) { return name === 'tabindex' && this.tabIndex !== undefined; },
        scrollIntoView(options) { this.scrolled = options; },
        ...section
    }));
    const outside = outsideIds.map(id => ({ id }));
    const document = {
        querySelectorAll: () => [...outside, ...headings].filter(element => element.id),
        getElementById: id => [...outside, ...headings].find(element => element.id === id)
    };
    const body = {
        ownerDocument: document,
        querySelectorAll: () => headings,
        contains: element => headings.includes(element)
    };
    globalThis.window = { location: { hash: '' } };
    return { body, headings };
}

test('contents include subsections while retaining authored anchors and keyboard focusability', () => {
    const { body, headings } = setup([
        { id: 'how', textContent: 'Implementation', tagName: 'H3' },
        { textContent: ' When  to\nCache ' },
        { textContent: '.NET IMemoryCache', tagName: 'H5', tabIndex: 0 },
        { textContent: 'Snippet heading', excluded: true },
        { textContent: ' ' }
    ]);

    assert.deepEqual(article.getHeadings(body), [
        { id: 'how', title: 'Implementation', level: 3 },
        { id: 'when-to-cache', title: 'When to Cache', level: 4 },
        { id: 'net-imemorycache', title: '.NET IMemoryCache', level: 5 }
    ]);
    assert.equal(headings[1].tabIndex, -1);
    assert.equal(headings[2].tabIndex, 0);
    assert.equal(headings[3].id, '');
});

test('generated anchors avoid page-wide collisions and remain stable on repeated reads', () => {
    const { body } = setup([
        { textContent: 'Cache' },
        { textContent: 'Cache' },
        { textContent: 'Café' },
        { textContent: '缓存' },
        { textContent: '???' }
    ], ['cache', 'cache-2', 'section']);

    const first = article.getHeadings(body);
    assert.deepEqual(first.map(heading => heading.id), ['cache-3', 'cache-4', 'cafe', '缓存', 'section-2']);
    assert.deepEqual(article.getHeadings(body), first);
});

test('deep links restore only an article target, including encoded Unicode fragments', () => {
    const { body, headings } = setup([{ id: '缓存', textContent: '缓存' }], ['main']);
    window.location.hash = '#main';
    article.restoreFragment(body);
    assert.equal(headings[0].scrolled, undefined);
    window.location.hash = '#missing';
    article.restoreFragment(body);
    assert.equal(headings[0].scrolled, undefined);
    window.location.hash = `#${encodeURIComponent('缓存')}`;
    article.restoreFragment(body);
    assert.deepEqual(headings[0].scrolled, { block: 'start' });
});

test('malformed fragments report the problem without breaking article contents', () => {
    const { body } = setup([]);
    const originalWarn = console.warn;
    const warnings = [];
    console.warn = (...args) => warnings.push(args);
    try {
        window.location.hash = '#%invalid';
        assert.doesNotThrow(() => article.restoreFragment(body));
        assert.equal(warnings.length, 1);
        assert.match(warnings[0][0], /URL fragment is not valid/);
    } finally {
        console.warn = originalWarn;
    }
});

function adCandidate(words, { hash = '', top = 1200, codeWords = 0, boundary = true } = {}) {
    globalThis.window = { location: { hash }, innerHeight: 800 };
    const text = 'prose '.repeat(words);
    return {
        querySelector: () => boundary ? { id: 'first-boundary', getBoundingClientRect: () => ({ top }) } : null,
        cloneNode: () => {
            const clone = {
                textContent: text + 'code '.repeat(codeWords),
                querySelectorAll: () => [{ remove: () => { clone.textContent = text; } }]
            };
            return clone;
        }
    };
}

test('one mid-article placement needs at least 1000 prose words, excluding code', () => {
    assert.equal(article.getMidArticleAdId(adCandidate(999, { codeWords: 2000 })), null);
    assert.equal(article.getMidArticleAdId(adCandidate(1000)), 'first-boundary');
});

test('mid-article ads never insert above the viewport or a deep-linked reader', () => {
    assert.equal(article.getMidArticleAdId(adCandidate(2000, { top: 799 })), null);
    assert.equal(article.getMidArticleAdId(adCandidate(2000, { hash: '#how' })), null);
    assert.equal(article.getMidArticleAdId(adCandidate(2000, { boundary: false })), null);
});
