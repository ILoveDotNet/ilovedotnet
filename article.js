export function getHeadings(body) {
    const usedIds = new Set(Array.from(body.ownerDocument.querySelectorAll('[id]'), element => element.id));
    const headings = [];

    for (const heading of body.querySelectorAll('h2, h3, h4, h5, h6')) {
        if (heading.closest('.not-prose, pre, code')) continue;
        const title = heading.textContent.replace(/\s+/g, ' ').trim();
        if (!title) continue;

        if (!heading.id) {
            const stem = title.normalize('NFKD').replace(/\p{M}/gu, '')
                .toLowerCase().replace(/[^\p{L}\p{N}]+/gu, '-').replace(/^-|-$/g, '') || 'section';
            let id = stem;
            let suffix = 2;
            while (usedIds.has(id)) id = `${stem}-${suffix++}`;
            heading.id = id;
            usedIds.add(id);
        }

        if (!heading.hasAttribute('tabindex')) heading.tabIndex = -1;
        headings.push({ id: heading.id, title, level: Number(heading.tagName.slice(1)) });
    }

    return headings;
}

export function restoreFragment(body) {
    if (!window.location.hash) return;
    let id;
    try {
        id = decodeURIComponent(window.location.hash.slice(1));
    } catch (error) {
        console.warn('Unable to open article section: the URL fragment is not valid.', error);
        return;
    }

    const target = body.ownerDocument.getElementById(id);
    if (target && body.contains(target)) target.scrollIntoView({ block: 'start' });
}

export function getMidArticleAdId(body) {
    // Never insert space above a reader who arrived at a specific section.
    if (window.location.hash) return null;
    const boundary = body.querySelector('[data-article-ad-break]');
    if (!boundary || boundary.getBoundingClientRect().top < window.innerHeight) return null;

    const prose = body.cloneNode(true);
    for (const element of prose.querySelectorAll('.not-prose, pre, code, [data-article-ad-break]')) {
        element.remove();
    }

    const words = prose.textContent.match(/\p{L}[\p{L}\p{N}'\u2019-]*/gu) ?? [];
    return words.length >= 1000 ? boundary.id : null;
}
