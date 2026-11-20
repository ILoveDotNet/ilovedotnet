export function onDisplayModeChanged(mode) {
    if (mode === 'dark' || (mode === 'system' && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark');
    }
    else {
        document.documentElement.classList.remove('dark');
    }
    if (mode === 'system') {
        localStorage.removeItem('DisplayMode');
    }
    else {
        localStorage.setItem('DisplayMode', mode);
    }
}

export function getDisplayMode() {
    return ('DisplayMode' in localStorage) ? localStorage.getItem('DisplayMode') : null;
}