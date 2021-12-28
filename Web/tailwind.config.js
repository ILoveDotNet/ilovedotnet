module.exports = {
    important: true,
    content: ['**/*.html', '**/*.razor', '**/*.cs'],
    darkMode: 'media',
    theme: {
        extend: {}
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('autoprefixer')
    ],
};
