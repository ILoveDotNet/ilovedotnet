module.exports = {
    content: ['**/*.html', '**/*.razor', '**/*.cs'],
    extractors: [
        {
            extractor: (content) => {
                // fix for escaped tailwind prefixes (sm:, lg:, etc)
                return content.match(/[A-Za-z0-9-_:\/]+/g) || []
            },
            extensions: ['css', 'html', 'razor'],
        },
    ],
    css: ['wwwroot/css/app.min.css'],
    safelist: ['blazor-error-boundary'],
    output: 'wwwroot/css/app.min.css'
};