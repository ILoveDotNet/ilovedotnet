import { CreateMLCEngine, deleteModelAllInfoInCache } from "../../lib/webllm/webllm.js";
import { models, selectedModel, maximumResponseTokens, createGroundedPrompt, validateAnswer, provisionalDraft, normalizeAnswer } from "./generation-contract.js";

let engine;
let busy = false;
const appConfig = { model_list: models, useIndexedDBCache: true };

self.addEventListener("message", async ({ data }) => {
    const { requestId, type } = data;
    if (busy) {
        self.postMessage({ requestId, error: "The local answer model is busy." });
        return;
    }
    busy = true;
    let rawAnswer;
    try {
        if (type === "load") {
            const model = models.find(model => model.model_id === (data.modelId ?? selectedModel.model_id));
            if (!model) {
                throw new Error("The requested answer model is not approved.");
            }
            engine = await CreateMLCEngine(model.model_id, {
                appConfig,
                logLevel: "SILENT",
                initProgressCallback: report => self.postMessage({
                    requestId, update: { text: report.text, progress: report.progress, phase: "loading" },
                }),
            });
            self.postMessage({ requestId, result: true });
        } else if (type === "clear") {
            if (engine) {
                await engine.unload();
                engine = undefined;
            }
            await deleteModelAllInfoInCache(selectedModel.model_id, appConfig);
            self.postMessage({ requestId, result: true });
        } else if (type === "generate") {
            if (!engine) {
                throw new Error("The local answer model is not loaded.");
            }
            const prompt = createGroundedPrompt(data.question, data.sources, data.history);
            await engine.resetChat();
            const stream = await engine.chat.completions.create({
                messages: prompt.messages, stream: true, max_tokens: maximumResponseTokens,
                temperature: 0, seed: 42, enable_thinking: false,
            });
            let text = "";
            let finishReason;
            let lastUpdate = -Infinity;
            for await (const chunk of stream) {
                const choice = chunk.choices[0];
                text += choice?.delta?.content ?? "";
                rawAnswer = text;
                finishReason = choice?.finish_reason ?? finishReason;
                if (performance.now() - lastUpdate > 150) {
                    const draft = provisionalDraft(text);
                    if (draft !== null) {
                        self.postMessage({ requestId, update: { phase: "streaming", text: draft } });
                    }
                    lastUpdate = performance.now();
                }
            }
            if (finishReason === "stop" && normalizeAnswer(text) === "INSUFFICIENT_EVIDENCE") {
                self.postMessage({
                    requestId,
                    result: { text: "", sourceCount: prompt.sourceCount, outcome: "refused" },
                });
                return;
            }
            const answer = validateAnswer(text, prompt.sources, finishReason);
            self.postMessage({ requestId, result: { text: answer, sourceCount: prompt.sourceCount, outcome: "answered" } });
        } else {
            throw new Error("Unknown local answer operation.");
        }
    } catch (error) {
        self.postMessage({ requestId, error: error instanceof Error ? error.message : "Local generation failed.", rawAnswer });
    } finally {
        busy = false;
    }
});
