import test from "node:test";
import assert from "node:assert/strict";
import { readFile } from "node:fs/promises";
import vm from "node:vm";
import { meaningfulTerms } from "../../CommonComponents/wwwroot/js/chat/topic-terms.js";

const source = await readFile(new URL("../../CommonComponents/wwwroot/js/chat/retrieval-worker.js", import.meta.url), "utf8");

async function embeddingInput(question, context) {
    let input;
    const extractor = async text => {
        input = text;
        return { data: new Float32Array(384) };
    };
    extractor.tokenizer = { encode: text => Array(text.trim().split(/\s+/).length + 2).fill(1) };
    const sandbox = vm.createContext({
        env: {}, self: { addEventListener() {} }, meaningfulTerms, testExtractor: extractor,
    });
    vm.runInContext(source.replace(/^import .*;\r?\n/gm, "") + `
        initialize = async () => {};
        applicationBase = "https://example.test/";
        initializeEmbedder = async () => testExtractor;
        searchIndex = { passages: [], frequencies: new Map() };
        globalThis.searchForTest = search;
    `, sandbox);
    await sandbox.searchForTest(question, 6, context);
    return input;
}

test("retrieval drops the oldest question while preserving fitting recent context", async () => {
    const oldest = "older ".repeat(150).trim();
    const recent = "recent ".repeat(120).trim();
    assert.equal(await embeddingInput("How does it work?", `${oldest}\n${recent}`),
        `${recent}\nHow does it work?`);
});

test("retrieval keeps the exact 256-token boundary and drops context beyond it", async () => {
    const context = "recent ".repeat(250).trim();
    assert.equal(await embeddingInput("How does it work?", context), `${context}\nHow does it work?`);
    assert.equal(await embeddingInput("How does it work?", `${context} extra`), "How does it work?");
});

test("retrieval preserves the full current question and ignores context for a new topic", async () => {
    const question = `How does it ${"work ".repeat(251).trim()}?`;
    assert.equal(await embeddingInput(question, "earlier question"), question);
    assert.equal(await embeddingInput("Explain dependency injection", "earlier ".repeat(270)),
        "Explain dependency injection");
});

test("retrieval rejects a current question that exceeds the limit on its own", async () => {
    await assert.rejects(embeddingInput("word ".repeat(255), "earlier question"), /your question uses 257/);
});
