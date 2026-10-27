import test from "node:test";
import assert from "node:assert/strict";
import { createGroundedPrompt, validateAnswer, verifiedDraft, models } from "../../CommonComponents/wwwroot/js/chat/generation-contract.js";

const sources = Array.from({ length: 6 }, (_, index) => ({
    title: "Garbage collection", heading: "Sweep", excerpt: `Original evidence ${index}. ${"Sweeping reclaims memory. ".repeat(100)}`,
}));

test("evidence and history stay within the conservative generation token budget", () => {
    const prompt = createGroundedPrompt("What is sweeping?", sources, "previous question ".repeat(1000));
    assert.ok(prompt.sourceCount > 0 && prompt.sourceCount <= 6);
    assert.ok(new TextEncoder().encode(prompt.messages.map(message => message.content).join("")).length <= 3000);
    assert.match(prompt.messages[1].content, /\[1\]/);
    assert.equal(models.length, 3);
    assert.ok(models.every(model => /resolve\/[a-f0-9]{40}\//.test(model.model)));
});

test("only complete answers with valid per-paragraph evidence IDs are accepted", () => {
    const evidence = [{ excerpt: "Sweeping reclaims dead objects. Sweeping does not move surviving objects." }];
    assert.equal(validateAnswer("Sweeping reclaims dead objects. [1]", evidence, "stop"), "Sweeping reclaims dead objects. [1]");
    assert.equal(validateAnswer("Sweeping does not move surviving objects. [1]", evidence, "stop"), "Sweeping does not move surviving objects. [1]");
    assert.equal(validateAnswer("Sweeping reclaims dead objects. [1]\n\nSweeping reclaims dead objects. [1]", evidence, "stop"),
        "Sweeping reclaims dead objects. [1]");
    for (const text of [
        "An answer without citations.", "Unsupported fact. [2]", "Invalid zero. [0]",
        "A fact. [1]\n\nAnother unsupported fact.", "See https://example.com [1]",
        "<script>alert(1)</script> [1]", "INSUFFICIENT_EVIDENCE", "Fake reference [abc]",
        "A fact. [1](javascript:alert(1))",
        "Sweeping moves surviving objects. [1]",
        "Sweeping reclaims dead objects and moves survivors. [1]",
        "Sweeping reclaims dead objects. Sweeping does not move surviving objects. [1]",
    ]) {
        assert.throws(() => validateAnswer(text, evidence, "stop"));
    }
    assert.throws(() => validateAnswer("Sweeping reclaims dead objects. [1]", evidence, "length"));
    assert.throws(() => validateAnswer("Sweeping reclaims dead objects. [1]", [], "stop"));
    assert.throws(() => validateAnswer("Sweeping reclaims dead objects. [2]", [
        ...evidence, { excerpt: "Compaction moves surviving objects together." },
    ], "stop"));
});

test("invalid or oversized questions and absent evidence are explicit errors", () => {
    assert.throws(() => createGroundedPrompt("", sources));
    assert.throws(() => createGroundedPrompt("question", []));
    assert.throws(() => createGroundedPrompt("x".repeat(1001), sources));
    assert.throws(() => createGroundedPrompt("question", [{ title: "Bad", heading: "Bad", excerpt: "" }]));
    assert.throws(() => createGroundedPrompt("Which database index should I use?", sources), /do not match/);
});

test("streamed drafts contain only prefixes of original sentences and their correct citations", () => {
    const evidence = [{ excerpt: "Sweeping reclaims dead objects. Sweeping does not move surviving objects." }];
    assert.equal(verifiedDraft("Sweeping reclaims", evidence), "Sweeping reclaims");
    assert.equal(verifiedDraft("Sweeping reclaims dead objects. [1]", evidence), "Sweeping reclaims dead objects. [1]");
    assert.equal(verifiedDraft("Sweeping moves surviving objects.", evidence), null);
    assert.equal(verifiedDraft("Sweeping reclaims dead objects. [2]", evidence), null);
    assert.equal(verifiedDraft("Sweeping reclaims dead objects. [1]\n\nSweeping does not", evidence),
        "Sweeping reclaims dead objects. [1]\n\nSweeping does not");
    assert.equal(verifiedDraft("Invented explanation. [1]\n\nSweeping does not", evidence), null);
});

test("a leading decision list does not hide a later value-object definition or change citation IDs", () => {
    const passages = [
        { title: "Entities vs Value Objects", heading: "Decision Guide",
            excerpt: "Use a Value Object when: The concept is a measurement Two instances are interchangeable Examples: Money, Email Address, Date Range" },
        { title: "Entities vs Value Objects", heading: "Ownership",
            excerpt: "x".repeat(1500) + "." },
        { title: "Entities vs Value Objects", heading: "Implementing Value Objects",
            excerpt: "A Value Object has no identity—it's defined entirely by its attributes. Records provide structural equality automatically." },
    ];
    const prompt = createGroundedPrompt("what is a value object and why we need it?", passages);
    assert.equal(prompt.sourceCount, 3);
    assert.equal(prompt.sources[0].excerpt, "");
    assert.equal(prompt.sources[1].excerpt, "");
    assert.match(prompt.messages[1].content, /\[3\] Entities vs Value Objects/);
    assert.ok(new TextEncoder().encode(prompt.messages.map(message => message.content).join("")).length <= 3000);
    const answer = "A Value Object has no identity—it's defined entirely by its attributes. [3]";
    assert.equal(validateAnswer(answer, prompt.sources, "stop"), answer);
    assert.throws(() => validateAnswer(answer.replace("[3]", "[1]"), prompt.sources, "stop"));
    assert.throws(() => createGroundedPrompt("What is a value object?", passages.slice(0, 2)), /No supporting article evidence/);
});
