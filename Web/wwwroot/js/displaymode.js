export function onDisplayModeChanged(mode) {
    if (mode == 'dark') {
        document.documentElement.classList.add(mode);
    }
    else {
        document.documentElement.classList.remove(mode);
    }
}