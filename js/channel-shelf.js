export function scrollChannels(shelf, direction) {
    shelf.scrollBy({
        left: direction * shelf.clientWidth * 0.8,
        behavior: window.matchMedia("(prefers-reduced-motion: reduce)").matches ? "instant" : "smooth"
    });
}
