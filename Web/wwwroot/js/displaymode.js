export function onDisplayModeChanged(mode) {
    if (mode === 'dark' || (mode === 'system' && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark');
        localStorage.setItem('DisplayMode', 'dark');
    }
    else {
        document.documentElement.classList.remove('dark');
        localStorage.removeItem('DisplayMode');
    }
}