export function getViewPortWidth() {
    return Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);
}

export function focusElement(id) {
    var element = document.getElementById(id);
    element.focus();
}