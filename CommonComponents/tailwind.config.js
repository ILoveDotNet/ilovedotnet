module.exports = {
    important: true,
    content: ['../**/*.html', '../**/*.razor', '../**/*.cs'],
    darkMode: 'class',
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
