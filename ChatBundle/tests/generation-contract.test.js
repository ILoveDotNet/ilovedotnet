import test from "node:test";
import assert from "node:assert/strict";
import { createGroundedPrompt, validateAnswer, provisionalDraft, normalizeAnswer, models, selectedModel } from "../../CommonComponents/wwwroot/js/chat/generation-contract.js";

const sources = Array.from({ length: 6 }, (_, index) => ({
    title: "Garbage collection", heading: "Sweep", excerpt: `Original evidence ${index}. ${"Sweeping reclaims memory. ".repeat(100)}`,
}));

test("evidence and history fit the conservative Qwen3 context budget", () => {
    const prompt = createGroundedPrompt("What is sweeping?", sources, "previous question ".repeat(1000));
    assert.equal(prompt.sourceCount, 6);
    assert.ok(new TextEncoder().encode(prompt.messages.map(message => message.content).join("")).length <= 6000);
    assert.equal(prompt.responseFormat, undefined);
    assert.match(prompt.messages[0].content, /Paraphrase naturally/);
    assert.equal(selectedModel.model_id, "Qwen3-4B-q4f16_1-MLC");
    assert.equal(selectedModel.overrides.context_window_size, 8192);
    assert.ok(models.every(model => /resolve\/[a-f0-9]{40}\//.test(model.model)));
});

test("complete paraphrases with per-paragraph citations are accepted, not certified as factual", () => {
    const evidence = [{ excerpt: "Sweeping reclaims dead objects. Sweeping does not move surviving objects." }];
    const answer = "During sweeping, the collector makes dead objects' memory available again. [1]";
    assert.equal(validateAnswer(answer, evidence, "stop"), answer);
    assert.equal(validateAnswer(`<think>\n\n</think>\n${answer}`, evidence, "stop"), answer);
    // ID checks cannot determine entailment. This intentional behavior is disclosed to readers.
    assert.equal(validateAnswer("Sweeping moves surviving objects. [1]", evidence, "stop"), "Sweeping moves surviving objects. [1]");
    for (const text of [
        "An answer without citations.", "Unsupported fact. [2]", "Invalid zero. [0]",
        "A fact. [1]\n\nAnother unsupported fact.", "See https://example.com [1]",
        "<script>alert(1)</script> [1]", "INSUFFICIENT_EVIDENCE", "Fake reference [abc]",
        "A fact. [1](javascript:alert(1))", "<think>private reasoning</think>Answer. [1]",
        "A fact [1,2].", `${"word ".repeat(151)}[1]`,
    ]) assert.throws(() => validateAnswer(text, evidence, "stop"), text);
    assert.throws(() => validateAnswer(answer, evidence, "length"));
    assert.throws(() => validateAnswer(answer, [], "stop"));
    assert.throws(() => validateAnswer(answer, [{ excerpt: "" }], "stop"));
    assert.equal(validateAnswer('Set node["temperature"] = 18 to update the value. [1]', evidence, "stop"),
        'Set node["temperature"] = 18 to update the value. [1]');
});

test("invalid questions, unusable evidence and unrelated topic fail explicitly", () => {
    assert.throws(() => createGroundedPrompt("", sources));
    assert.throws(() => createGroundedPrompt("question", []));
    assert.throws(() => createGroundedPrompt("x".repeat(1001), sources));
    assert.throws(() => createGroundedPrompt("question", [{ title: "Bad", heading: "Bad", excerpt: "" }]));
    assert.throws(() => createGroundedPrompt("Which database index should I use?", sources), /do not match/);
    assert.throws(() => createGroundedPrompt("How do I call GC.EnableTeleportation()?", sources), /requested API is not present/);
    assert.doesNotThrow(() => createGroundedPrompt("How do I call GC.Collect()?", [
        { title: "Garbage collection", heading: "Explicit collection", excerpt: "Call GC.Collect() to request garbage collection." },
    ]));
});

test("provisional drafts allow composed text but hide empty wrappers and refusals", () => {
    assert.equal(provisionalDraft("Here is a composed explanation"), "Here is a composed explanation");
    for (const text of ["", "<", "<think>", "<think>\n\n", "<think>reasoning", "INSUFFICIENT_", "INSUFFICIENT_EVIDENCE"])
        assert.equal(provisionalDraft(text), null);
    assert.equal(provisionalDraft("<think>\n\n</think>\nThe red phase starts"), "The red phase starts");
    assert.equal(provisionalDraft("<think>private reasoning</think>Answer"), null);
    assert.equal(provisionalDraft("See https://example.com"), null);
    assert.equal(normalizeAnswer("<think>\n</think>\nINSUFFICIENT_EVIDENCE"), "INSUFFICIENT_EVIDENCE");
});

test("packing preserves lists and bounded code with original evidence slots", () => {
    const passages = [
        { title: "Value Objects", heading: "Decision guide", excerpt: "Use a value object when: values are interchangeable\nExamples: Money, DateRange" },
        { title: "Value Objects", heading: "Oversized", excerpt: "x".repeat(7000) },
        { title: "Value Objects", heading: "Definition", excerpt: "A value object is defined by its attributes rather than identity." },
        { title: "Value Objects", heading: "Code", excerpt: '```csharp\nvar price = new Money(10);\n```\nValidation keeps domain values consistent.' },
    ];
    const prompt = createGroundedPrompt("what is a value object and why we need it?", passages);
    assert.match(prompt.sources[0].excerpt, /Examples: Money, DateRange/);
    assert.equal(prompt.sources[1].excerpt, "");
    assert.match(prompt.messages[1].content, /\[3\] Value Objects/);
    assert.match(prompt.sources[3].excerpt, /```csharp\nvar price = new Money\(10\);\n```/);
    assert.throws(() => validateAnswer("A value object represents a domain value. [2]", prompt.sources, "stop"));
});

test("TDD Red definition survives prompt packing alongside implementation code", () => {
    const prompt = createGroundedPrompt("what is red phase in TDD?", [
        { title: "Introducing TDD", heading: "Basics", excerpt: "TDD means writing tests before implementation.\n\nThen you take the first requirement and write a failing test.\n\nThe test fails and it is RED as you haven't developed it yet." },
        { title: "Implementing TDD", heading: "Create a Red unit test", excerpt: `\`\`\`csharp\n${"var request = new Request();\n".repeat(200)}\`\`\`\nThe test fails with NotImplementedException.\nGreat, we completed the 1st step of TDD (Red Phase).` },
    ]);
    assert.match(prompt.sources[0].excerpt, /write a failing test/);
    assert.match(prompt.sources[1].excerpt, /test fails with NotImplementedException/);
    assert.doesNotMatch(prompt.sources[1].excerpt, /```/);
});

test("comparison packing retains each definition and complete .NET sentences within the byte budget", () => {
    const passages = ["Transient", "Scoped", "Singleton"].map(heading => ({
        title: "Service lifetimes", heading,
        excerpt: `${heading} has an authored definition in .NET. ${"Long evidence with Unicode \u2014 text. ".repeat(100)}`,
    }));
    const prompt = createGroundedPrompt("Compare transient scoped and singleton service lifetimes.", passages);
    assert.ok(new TextEncoder().encode(prompt.messages.map(message => message.content).join("")).length <= 6000);
    assert.deepEqual(prompt.sources.map(source => source.heading), ["Transient", "Scoped", "Singleton"]);
    for (const [index, source] of prompt.sources.entries()) {
        assert.ok(source.excerpt.startsWith(`${passages[index].heading} has an authored definition in .NET.`));
        assert.ok(passages[index].excerpt.includes(source.excerpt.replaceAll("\n", " ")));
        assert.match(source.excerpt, /\.$/);
        assert.match(prompt.messages[1].content, new RegExp(`\\[${index + 1}\\] Service lifetimes / ${source.heading}`));
    }
});
