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
