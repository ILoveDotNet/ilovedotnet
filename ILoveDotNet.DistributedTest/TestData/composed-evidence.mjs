export function selectEvidence(question, retrieved, corpus, meaningfulTerms) {
    if (!retrieved.length) return [];
    const query = meaningfulTerms(question);
    const terms = text => meaningfulTerms(text);
    const overlap = (left, right) => [...left].filter(term => right.has(term)).length;
    const articles = new Set(retrieved.map(source => source.contentUrl));
    const candidates = [...retrieved, ...corpus.filter(source => articles.has(source.contentUrl))];
    const distinct = new Map();
    for (const source of candidates) {
        const key = JSON.stringify([source.contentUrl, source.section, source.heading]);
        const previous = distinct.get(key);
        if (!previous || source.excerpt.length > previous.excerpt.length) distinct.set(key, source);
    }
    const ranked = [...distinct.values()].map(source => ({
        source,
        headingTerms: terms(source.heading),
        score: overlap(query, terms(source.heading)) * 3 + overlap(query, terms(source.excerpt))
            - (/^\s*(in this (article|post)|this article covers)/i.test(source.excerpt) ? 2 : 0),
    })).sort((left, right) => right.score - left.score);
    const selected = [];
    const covered = new Set();
    // Cover explicitly requested subtopics before filling remaining slots by relevance.
    while (selected.length < 6 && ranked.length) {
        ranked.sort((left, right) => {
            const gain = item => [...item.headingTerms].filter(term => query.has(term) && !covered.has(term)).length;
            return gain(right) - gain(left) || right.score - left.score;
        });
        const next = ranked.shift();
        if (next.score <= 0) continue;
        selected.push(next.source);
        for (const term of next.headingTerms) covered.add(term);
    }
    return selected;
}

export function buildEvidencePrompt(question, sources, system, context = "", maximumBytes = 6000) {
    const encoder = new TextEncoder();
    const start = `Question: ${question}\nPrevious question (context only): ${context}\n\nArticle evidence:\n`;
    const end = "\nAnswer only from these excerpts. Cite each sentence. If a requested API or fact is absent, say INSUFFICIENT_EVIDENCE. /no_think";
    let evidence = "";
    const included = [];
    const segmenter = new Intl.Segmenter("en", { granularity: "sentence" });
    for (const [index, source] of sources.entries()) {
        const header = `[${included.length + 1}] ${source.title} / ${source.heading}\n`;
        const remaining = maximumBytes - encoder.encode(system + start + evidence + header + end + "\n\n").length;
        const budget = Math.min(1400, Math.floor(remaining / Math.min(3, sources.length - index)));
        let excerpt = "";
        const prose = source.excerpt.replace(/```[\s\S]*?(?:```|$)/g, "").replace(/\s+/g, " ").trim();
        const segments = Array.from(segmenter.segment(prose), part => part.segment);
        for (let index = 0; index < segments.length; index++) {
            let segment = segments[index];
            // Intl may split ".NET" after the dot; never budget its halves independently.
            if (segment.endsWith(".") && /^NET\b/.test(segments[index + 1] ?? "")) segment += segments[++index];
            const sentence = segment.trim();
            if (!/[.!?]["')\]]?$/.test(sentence)) continue;
            if (encoder.encode(excerpt + segment).length <= budget) excerpt += segment;
        }
        if (!excerpt.trim()) continue;
        evidence += header + excerpt.trim() + "\n\n";
        included.push({ ...source, excerpt: excerpt.trim() });
    }
    if (!included.length) throw new Error("No complete evidence fits the composed-answer benchmark context.");
    const content = start + evidence + end;
    return { messages: [{ role: "system", content: system }, { role: "user", content }],
        sources: included, inputBytes: encoder.encode(system + content).length };
}

export function inspectAnswer(text, sourceCount, finishReason) {
    // Only the known empty Qwen wrapper is normalized; nonempty reasoning/HTML is rejected.
    const answer = text.replace(/^\s*<think>\s*<\/think>\s*/, "").trim();
    const refused = finishReason === "stop" && answer === "INSUFFICIENT_EVIDENCE";
    const references = [...answer.matchAll(/\[(\d+)\]/g)];
    const syntaxValid = finishReason === "stop" && references.length > 0
        && references.every(match => Number(match[1]) >= 1 && Number(match[1]) <= sourceCount)
        && !/[\[\]]/.test(answer.replace(/\[\d+\]/g, ""))
        && !/INSUFFICIENT_EVIDENCE|https?:|www\.|<|>|\]\s*\(/i.test(answer)
        && answer.split(/\n\s*\n/).every(paragraph => /\[\d+\](?:\s*\[\d+\])*[.!]?\s*$/.test(paragraph));
    return { answer, refused, syntaxValid, wordCount: answer.split(/\s+/).length,
        requiresClaimReview: !refused };
}
