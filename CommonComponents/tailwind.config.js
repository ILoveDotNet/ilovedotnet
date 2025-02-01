module.exports = {
    important: true,
    content: ['../**/*.html', '../**/*.razor', '../**/*.cs', '!../**/bin/**', '!../**/obj/**', '!../**/node_modules/**'],
    darkMode: 'class',
    theme: {
        extend: {}
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/typography')
    ],
};
