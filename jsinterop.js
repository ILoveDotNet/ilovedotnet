var blazorInterop = blazorInterop || {};

export function displayAlert(textToDisplay) 
{ 
    alert(textToDisplay); 
};

export function getViewportDimensions() 
{
    return { width: window.innerWidth, height: window.innerHeight };
}

export function callDotNetStaticFromJs()
{
    let message = DotNet.invokeMethod('BlazorDemoComponents', 'GetMessageFromDotNet');
    alert(`Message from .Net Static Method - ${message}`);
}

export function callDotNetInstanceFromJs(dotNetHelper)
{
    let message = dotNetHelper.invokeMethod('GetMessageFromDotNet');
    alert(`Message from .Net Instance Method - ${message}`);
}