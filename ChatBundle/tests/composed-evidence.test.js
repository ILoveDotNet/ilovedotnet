import test from "node:test";
import assert from "node:assert/strict";
import { selectEvidence, buildEvidencePrompt, inspectAnswer } from "../../ILoveDotNet.DistributedTest/TestData/composed-evidence.mjs";
import { meaningfulTerms } from "../../CommonComponents/wwwroot/js/chat/topic-terms.js";

test("comparison evidence covers all requested lifetimes from retrieved articles only", () => {
    const source = (heading, excerpt, contentUrl = "blogs/lifetimes/") =>
        ({ title: "Lifetimes", section: "How", heading, excerpt, contentUrl });
    const retrieved = [source("Captive Dependency", "A lifetime dependency can be unsafe.")];
    const corpus = [
        ...retrieved,
        source("Transient", "Transient services are created each time requested."),
        source("Scoped", "Scoped services are disposed at the end of the request."),
        source("Singleton", "Singleton services reuse the same instance."),
        source("Singleton", "Other irrelevant content.", "blogs/unretrieved/"),
    ];
    const selected = selectEvidence("Compare transient scoped and singleton service lifetimes.", retrieved, corpus, meaningfulTerms);
    assert.deepEqual(new Set(selected.slice(0, 3).map(source => source.heading)), new Set(["Transient", "Scoped", "Singleton"]));
    assert.ok(selected.every(source => source.contentUrl === "blogs/lifetimes/"));
    assert.deepEqual(selectEvidence("What is a unicorn?", [], corpus, meaningfulTerms), []);
});

test("prompt preserves complete sentences, multiple facets and byte budget", () => {
    const sources = ["Transient", "Scoped", "Singleton"].map(heading => ({
        title: "Lifetimes", heading, excerpt: `${heading} has an authored definition in .NET. ${"Long evidence with Unicode — text. ".repeat(100)}`,
    }));
    const prompt = buildEvidencePrompt("Compare service lifetimes.", sources, "Use only evidence.");
    assert.ok(prompt.inputBytes <= 6000);
    assert.equal(prompt.sources.length, 3);
    for (const source of prompt.sources) assert.match(source.excerpt, /\.$/);
    assert.equal(prompt.sources[1].heading, "Scoped");
    assert.ok(prompt.sources.every(source => source.excerpt.includes(".NET") && !source.excerpt.includes(". NET")));
});

test("format checks never masquerade as claim correctness", () => {
    const incorrect = inspectAnswer("Scoped services are disposed when resolved. [1]", 1, "stop");
    assert.equal(incorrect.syntaxValid, true);
    assert.equal(incorrect.requiresClaimReview, true);
    assert.equal(inspectAnswer("<think>\n</think>\nINSUFFICIENT_EVIDENCE", 1, "stop").refused, true);
    assert.equal(inspectAnswer("Explanation. INSUFFICIENT_EVIDENCE", 1, "stop").refused, false);
    assert.equal(inspectAnswer("<think>private reasoning</think>Answer. [1]", 1, "stop").syntaxValid, false);
    assert.equal(inspectAnswer("Answer. [2]", 1, "stop").syntaxValid, false);
    assert.equal(inspectAnswer("Answer. [1]", 1, "length").syntaxValid, false);
});

test("evidence packing skips non-prose, preserves provenance, and fails explicitly when nothing fits", () => {
    const sources = [
        { title: "Decision guide", heading: "List", excerpt: "Use a value object when: values are interchangeable" },
        { title: "Implementation", heading: "Code", excerpt: "```csharp\nvar x = new Money(10);\n```" },
        { title: "Definition", heading: "Value objects", contentUrl: "blogs/value-objects/", section: "What",
            excerpt: "A value object is defined by its attributes rather than a unique identity." },
    ];
    const prompt = buildEvidencePrompt("What is a value object?", sources, "Use only evidence.");
    assert.equal(prompt.sources.length, 1);
    assert.equal(prompt.sources[0].contentUrl, "blogs/value-objects/");
    assert.equal(prompt.sources[0].section, "What");
    assert.match(prompt.messages[1].content, /\[1\] Definition/);
    assert.throws(() => buildEvidencePrompt("What is a value object?", sources.slice(0, 2), "Use only evidence."),
        /No complete evidence/);
    assert.throws(() => buildEvidencePrompt("x".repeat(7000), sources, "Use only evidence."),
        /No complete evidence/);
});

test("a budget boundary cannot keep only the dot from a .NET sentence", () => {
    const prompt = buildEvidencePrompt("Explain memory.", [{
        title: "Memory", heading: "Summary",
        excerpt: `Memory is reclaimed. Each .NET process ${"has an isolated address space ".repeat(100)}.`,
    }], "Use only evidence.", "", 500);
    assert.equal(prompt.sources[0].excerpt, "Memory is reclaimed.");
});
