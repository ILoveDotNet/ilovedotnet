export function onDisplayModeChanged(mode) {
    if (mode == 'dark') {
        document.documentElement.classList.add('dark');
    }
    else {
        document.documentElement.classList.remove('dark');
    }
}