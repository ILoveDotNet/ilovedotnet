const stopWords = new Set([
    "a", "an", "and", "are", "as", "at", "be", "by", "can", "could", "define", "do", "does",
    "explain", "for", "from", "how", "i", "in", "is", "it", "its", "me", "of", "on",
    "or", "please", "should", "that", "the", "their", "them", "these", "they", "this",
    "those", "to", "was", "were", "what", "when", "where", "which", "why", "will",
    "with", "would", "you", "work", "works", "show", "article", "explaining", "not",
    "invent", "source", "using", "use", "about",
]);

export function meaningfulTerms(text) {
    return new Set((text.toLowerCase().replace(/\bweb\s+assembly\b/g, "webassembly").match(/[a-z_][a-z0-9_]*/g) ?? [])
        .filter(term => !stopWords.has(term))
        .map(term => {
            if (term.length > 5 && term.endsWith("ing")) {
                term = term.slice(0, -3).replace(/([b-df-hj-np-tv-z])\1$/, "$1");
            } else if (term.length > 4 && term.endsWith("ies")) {
                term = `${term.slice(0, -3)}y`;
            } else if (term.length > 4 && term.endsWith("s") && !term.endsWith("ss")) {
                term = term.slice(0, -1);
            }
            if (term.length > 6 && term.endsWith("tion")) {
                term = term.slice(0, -3);
            }
            return term.length > 4 && term.endsWith("e") ? term.slice(0, -1) : term;
        }));
}
