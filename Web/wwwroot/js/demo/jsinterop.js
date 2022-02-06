var blazorInterop = blazorInterop || {};

blazorInterop.displayAlert = (textToDisplay) => { alert(textToDisplay); };

blazorInterop.getViewportDimensions = () => {
    return { width: window.innerWidth, height: window.innerHeight };
}

blazorInterop.callDotNetStaticFromJs = () => {
    let message = DotNet.invokeMethod('Web', 'GetMessageFromDotNet');
    alert(`Message from .Net Static Method - ${message}`);
}

blazorInterop.callDotNetInstanceFromJs = (dotNetHelper) => {
    let message = dotNetHelper.invokeMethod('GetMessageFromDotNet');
    alert(`Message from .Net Static Method - ${message}`);
}