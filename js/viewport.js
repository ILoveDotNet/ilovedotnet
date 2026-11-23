const observers = new Map();

export function observeViewport(observerId, dotNetReference) {
    stopObservingViewport(observerId);
    const query = window.matchMedia("(max-width: 639px)");
    const onChange = event => {
        dotNetReference.invokeMethodAsync("OnViewportChanged", event.matches)
            .catch(error => console.error("Unable to update the responsive layout.", error));
    };
    query.addEventListener("change", onChange);
    observers.set(observerId, { query, onChange });
    return query.matches;
}

export function stopObservingViewport(observerId) {
    const observer = observers.get(observerId);
    if (observer) {
        observer.query.removeEventListener("change", observer.onChange);
        observers.delete(observerId);
    }
}