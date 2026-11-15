export async function onRuntimeConfigLoaded(config) {
    try {
        const configUrl = new URL('_blazor/_configuration', document.baseURI).href;
        const response = await fetch(configUrl);
        if (!response.ok || !response.headers.get('content-type')?.includes('application/json')) {
            return;
        }

        const serverConfig = await response.json();
        const environmentVariables = serverConfig?.webAssembly?.environment;
        if (!environmentVariables) {
            return;
        }

        config.environmentVariables ??= {};
        Object.assign(config.environmentVariables, environmentVariables);
    } catch (error) {
        console.warn('Failed to load Aspire client configuration:', error);
    }
}
