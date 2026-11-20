import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import test from 'node:test';

const source = await readFile(new URL('../../SharedComponents/wwwroot/js/ads.js', import.meta.url), 'utf8');
const ads = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`);

function setup({ hidden = false, fail = false } = {}) {
    const classes = new Set(['ad-slot-pending']);
    const ad = {
        dataset: {},
        hasAttribute: () => false,
        classList: { add: name => classes.add(name), remove: name => classes.delete(name) }
    };
    const error = { hidden: true };
    let visible = !hidden;
    const root = {
        isConnected: true,
        querySelector: selector => selector === 'ins[data-ad-slot]' ? ad : error,
        getClientRects: () => visible ? [{}] : [],
        getBoundingClientRect: () => ({ width: visible ? 288 : 0 })
    };
    let intersection;
    let resize;
    globalThis.IntersectionObserver = class {
        constructor(callback) { intersection = this; this.callback = callback; }
        observe() {}
        disconnect() { this.disconnected = true; }
    };
    globalThis.ResizeObserver = class {
        constructor(callback) { resize = this; this.callback = callback; }
        observe() {}
        disconnect() { this.disconnected = true; }
    };
    const queued = [];
    globalThis.document = { getElementById: () => root };
    globalThis.window = {
        adsbygoogle: {
            push(value) {
                if (fail) throw new Error('SDK rejected the unit.');
                queued.push(value);
            }
        }
    };
    return {
        ad, classes, error, queued,
        enter: () => intersection.callback([{ isIntersecting: true }]),
        resize: () => resize.callback(),
        show: () => { visible = true; },
        disconnected: () => intersection.disconnected && resize.disconnected
    };
}

test('units join the SDK only near the viewport and cannot refresh on repeated observer events', () => {
    const unit = setup();
    ads.observeAd('visible');
    assert.equal(unit.classes.has('adsbygoogle'), false);
    assert.equal(unit.queued.length, 0);
    unit.enter();
    unit.resize();
    unit.enter();
    ads.observeAd('visible');
    assert.equal(unit.queued.length, 1);
    assert.equal(unit.ad.dataset.adQueued, 'true');
    assert.equal(unit.classes.has('adsbygoogle'), true);
    assert.equal(unit.disconnected(), true);
});

test('hidden mobile sidebar units remain unqueued until there is usable desktop space', () => {
    const unit = setup({ hidden: true });
    ads.observeAd('sidebar');
    unit.enter();
    unit.resize();
    assert.equal(unit.queued.length, 0);
    unit.show();
    unit.resize();
    assert.equal(unit.queued.length, 1);
});

test('navigation disposal releases both observers without requesting an ad', () => {
    const unit = setup();
    ads.observeAd('disposed');
    ads.stopObservingAd('disposed');
    unit.enter();
    assert.equal(unit.queued.length, 0);
    assert.equal(unit.disconnected(), true);
});

test('SDK failures are reported without timers or automatic retries', () => {
    const unit = setup({ fail: true });
    const originalError = console.error;
    const errors = [];
    console.error = (...args) => errors.push(args);
    try {
        ads.observeAd('failed');
        unit.enter();
        unit.resize();
        ads.observeAd('failed');
        assert.equal(errors.length, 1);
        assert.equal(unit.error.hidden, false);
        assert.equal(unit.ad.dataset.adInitialization, 'failed');
        assert.equal(unit.classes.has('adsbygoogle'), false);
        assert.equal(unit.disconnected(), true);
    } finally {
        console.error = originalError;
    }
});
