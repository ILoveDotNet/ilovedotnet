const observations = new Map();

export function observeAd(id) {
    if (observations.has(id)) return;
    const root = document.getElementById(id);
    if (!root) throw new Error(`Advertisement container ${id} was not found.`);
    const ad = root.querySelector('ins[data-ad-slot]');
    if (!ad) throw new Error(`Advertisement container ${id} has no ad unit.`);
    if (ad.dataset.adQueued || ad.hasAttribute('data-adsbygoogle-status')) return;

    let active = true;
    let nearViewport = false;
    const cleanup = () => {
        active = false;
        intersection.disconnect();
        resize.disconnect();
        observations.delete(id);
    };
    const enqueue = () => {
        if (!active || !nearViewport) return;
        if (!root.isConnected) {
            cleanup();
            return;
        }
        if (!root.getClientRects().length || root.getBoundingClientRect().width <= 0) return;

        // Only eligible units join the SDK's queue of uninitialised ads.
        ad.classList.add('adsbygoogle');
        ad.dataset.adQueued = 'true';
        try {
            (window.adsbygoogle ??= []).push({});
        } catch (error) {
            ad.classList.remove('adsbygoogle');
            ad.dataset.adInitialization = 'failed';
            root.querySelector('[data-ad-error]').hidden = false;
            console.error('Unable to initialise an advertisement.', error);
        }
        cleanup();
    };
    const intersection = new IntersectionObserver(entries => {
        nearViewport = entries.some(entry => entry.isIntersecting);
        enqueue();
    }, { rootMargin: '200px 0px' });
    const resize = new ResizeObserver(enqueue);
    observations.set(id, cleanup);
    intersection.observe(root);
    resize.observe(root);
}

export function stopObservingAd(id) {
    observations.get(id)?.();
}
