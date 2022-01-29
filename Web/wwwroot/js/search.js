export function getViewPortWidth(dotNetHelper, dispose) {
    if (!dispose) {
        window.onresize = () => {
            dotNetHelper.invokeMethodAsync('WindowResized', Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0) < 640);
        }
    }
    else {
        dotNetHelper.dispose();
    }

    return Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);
}

export function focusElement(id)
{
    var element = document.getElementById(id);
    element.focus();
}