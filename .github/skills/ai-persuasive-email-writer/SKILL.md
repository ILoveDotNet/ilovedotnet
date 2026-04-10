---
name: ai-persuasive-email-writer
description: >
  Craft persuasive, goal-driven emails using AI. Covers goal definition, audience research,
  narrative structure (Problem-Agitate-Solution), tone calibration, subject-line generation,
  call-to-action sharpening, pre-send draft review, and ethical A/B testing.
  WHEN: write persuasive email, improve email response rate, fix email tone, generate subject
  lines, strengthen CTA, review email draft, split-test email campaign, email not getting replies.
---

# AI Persuasive Email Writer

## Overview

This skill turns AI into a co-author for high-converting emails. Every section maps to a
concrete failure mode in business email and provides an AI-assisted workflow to fix it.

---

## Core Rules

1. **Lock in the goal before writing.** Know the single action you want the reader to take.
   Include that goal explicitly in every AI prompt — without it, AI produces vague output.
2. **One call to action per email.** Multiple asks = no ask. If the email cannot be reduced to
   one CTA, split it into multiple emails.
3. **Structure before prose.** Apply Problem → Agitate → Solution (PAS) before polishing language.
4. **Tone is the reader's mood, not yours.** Always request an AI tone audit before sending.
5. **Change one variable at a time.** When A/B testing, test subject line OR body OR CTA — never all at once.
6. **If it can't fit PAS, it's too long.** Cut until it can.

---

## Workflows

### 1. Define a Persuasive Email Goal

Before opening any AI tool, answer: *"What is the single, specific action I want the reader
to take — and by when?"* Then encode that answer directly in the prompt.

**Prompt template:**
```
Help me write an email that results in [SPECIFIC ACTION] by [DATE/TIME].
Context: [1–2 sentences of background].
```

| ❌ Vague prompt | ✅ Goal-driven prompt |
|-----------------|----------------------|
| `Help me write a response.` | `Help me write a response that secures a 15-minute meeting next Tuesday to discuss the security proposal.` |

> **Why it matters:** An email that impresses the reader but asks for too much can actually
> work against you — the recipient may intend to give it a reply it "deserves", defer it,
> and forget. Too many ideas, too many questions, and no clear action means no reply.

---

### 2. Map Audience Intent and Context

Persuasion starts with understanding what the reader is dealing with — their struggles,
desires, and fears — before a single word is written.

**AI-assisted research prompts:**
```
Summarise this email thread and identify the reader's primary concern and desired outcome:
[PASTE THREAD]
```

```
Based on these LinkedIn posts / reviews / competitor messages, what is this person's
biggest frustration and what outcome are they hoping for?
[PASTE CONTENT]
```

**Principle:** A single sentence directed at a real pain point outperforms an entire paragraph
of well-crafted pitch. Research first; write second.

> **Why it matters:** Studying negative reviews of competing products is one of the fastest ways
> to understand what your audience actually struggles with. When you discover that readers feel
> excluded by advice that assumes resources they do not have, addressing that gap directly in
> your messaging — before the reader even asks — dramatically increases resonance.

---

### 3. Structure a Clear Narrative (PAS)

A wall of text is the fastest way to get an email deleted. Readers are busy — if they cannot
scan the email in 10 seconds and know what you want, they will delete it or defer it until they forget.

Apply **Problem → Agitate → Solution**:

| Section | Purpose | Target length |
|---------|---------|--------------|
| **Problem** | Name the exact pain the reader has | 1–2 sentences |
| **Agitate** | Expand why it matters / what it costs them | 1–3 sentences |
| **Solution** | Your offer or the single proposed next step | 1–2 sentences |

**Prompt template:**
```
Restructure my email using the Problem-Agitate-Solution format.
Make it scannable in under 10 seconds. Here is the draft:
[PASTE DRAFT]
```

> **Diagnostic:** If the draft cannot be broken into PAS cleanly, it is too long — cut it down
> until it fits. Structure is how you respect the reader's time.

> **Why it matters:** Effort that the audience never receives is wasted effort. Decision-makers
> skim first and read only if the opening earns it. If your email's structure does not make the
> value and the ask obvious within the first glance, the rest of the content is invisible.

---

### 4. Calibrate Tone for Trust and Action

Email strips out body language and voice. The mood you are in when you write is irrelevant —
what matters is the mood the reader is in when they read.

**Pre-send tone audit prompts:**
```
Does this email feel [professional / warm / urgent / neutral]?
Flag any sentence that could be read as curt, passive-aggressive, or dismissive.
```

```
Rewrite the following so it reads as confident and friendly without being informal:
[PASTE EMAIL]
```

```
Is this email positive? Is there anything here that could land badly
if the reader is having a bad day?
[PASTE EMAIL]
```

> **Why it matters:** The same words read differently depending on the reader's current state.
> A terse, upbeat reply written in a relaxed moment can land as cold or hostile to someone
> under pressure. The writer's intent is invisible — only what the words convey to the reader matters.

---

### 5. Generate Compelling Subject Lines

The subject line is the gatekeeper. If it fails, the email never gets opened — no matter how
good the body is.

**Workflow:**
1. Write the email body first.
2. Ask AI to generate **at least 5 subject line options**.
3. Choose the option that is specific, curiosity-driving, and avoids spam triggers.
4. Choosing from a selection always produces a better result than committing to the first draft.

**Prompt template:**
```
Generate 5 subject lines for the email below.
Optimise for maximum open rate. Avoid spam-trigger words.
Keep each under 50 characters where possible.
[PASTE EMAIL BODY]
```

**Subject line checklist:**
- [ ] Specific — not generic ("Following up" fails; "Your 3-min feedback on the Tuesday proposal" works)
- [ ] Creates curiosity or signals clear, immediate value
- [ ] Free of spam words: FREE, URGENT, !!!, ALL CAPS
- [ ] Tone matches the email body
- [ ] Avoids "Fwd:" / "Re:" prefixes that trigger spam filters

> **Why it matters:** Readers make split-second judgements based on the subject line alone.
> A subject that looks automated, spammy, or irrelevant will be deleted before the email is
> ever opened — regardless of how important or well-written the body is.

---

### 6. Strengthen Calls to Action

A vague CTA destroys deals that the body already won. "Looking forward to working with you"
tells the reader nothing — they are left waiting while you wait for them.

A strong CTA must answer:
1. **What** should the reader do?
2. **When** should they do it?
3. Can it be answered with **yes / no** or a single action?

**CTA checklist:**
- [ ] Specifies exactly one action
- [ ] Includes a timeline or deadline
- [ ] Answerable with yes/no or one step
- [ ] Zero ambiguity about who does what next

| ❌ Vague CTA | ✅ Clear CTA |
|-------------|-------------|
| `Looking forward to working with you.` | `I'll send the invoice by EOD today. Please review the project steps and we'll kick off Monday.` |
| `Let me know your thoughts.` | `Reply with your available slots on Tuesday or Wednesday and I'll book us a 15-minute call.` |

**Prompt template:**
```
Review the call to action in my email. Rewrite it so it:
1. Specifies one clear action
2. Includes a timeline
3. Can be answered with yes/no or a single step
4. Leaves no room for ambiguity about who does what next
[PASTE EMAIL]
```

> **Why it matters:** When both sides are waiting for the other to act, nothing moves.
> A warm, enthusiastic closing message feels good to write but gives the reader no clear
> next step — creating confusion that can stall or kill a deal that was already won.

---

### 7. Evaluate Drafts with AI Feedback

Familiarity blinds. After staring at your own writing, you stop seeing the problems —
especially cultural, tonal, or assumption-based blind spots.

**Pre-send review prompt (run before every high-stakes email):**
```
Review this email for:
1. Any sentence that could be misinterpreted or read negatively
2. Assumptions the reader may not share
3. Tone inconsistencies or cultural blind spots
4. Anything that might cause the reader to hesitate or not reply
[PASTE EMAIL]
```

```
Is there anything in this email that could land flat or be misunderstood
by someone from a different cultural or professional background?
[PASTE EMAIL]
```

> **Why it matters:** Writing styles, humour, and professional norms vary across cultures and
> backgrounds. What reads as wit or casual warmth in one context can seem dismissive or sarcastic
> in another. Assumptions you do not know you are making are the hardest blind spots to catch —
> a fresh AI review surfaces them before the email is sent.

---

### 8. Design Ethical A/B Tests

Stop guessing what works. Data beats intuition every time.

**Rules:**
- Test **one variable at a time**: subject line, opening hook, CTA, or body.
- Keep both variants honest — no misleading subject lines or false urgency.
- Use results to improve future emails, not to manipulate individuals.
- Run the test, measure replies/opens/conversions, adopt the winner.

**Test priority order (highest leverage first):**
1. Subject line — gates open rates
2. Call to action — gates conversions
3. Opening hook — gates continued reading
4. Body structure / length

**Prompt template:**
```
Write two meaningfully different versions of the [subject line / CTA / opening hook]
for the email below, so I can A/B test which drives more replies.
Keep both honest and on-brand.
[PASTE EMAIL]
```

> **Why it matters:** Intuition — even experienced intuition — is frequently wrong about what
> an audience will respond to. A single test that contradicts your assumption can redirect an
> entire strategy. The only way to know what works is to measure it, not guess.

---

## Quick-Reference Prompt Cheat Sheet

| Goal | Prompt starter |
|------|---------------|
| Write with a specific goal | `Write an email that results in [ACTION] by [DATE]` |
| Research the reader | `Summarise this thread and identify the reader's primary concern and desired outcome` |
| Restructure for clarity | `Rewrite using Problem-Agitate-Solution, scannable in under 10 seconds` |
| Tone audit | `Flag sentences that could be read as [curt / passive-aggressive / unclear]` |
| Generate subject lines | `Generate 5 subject lines optimised for open rate, free of spam triggers` |
| Sharpen the CTA | `Rewrite the CTA so it specifies one action, includes a timeline, and can be answered yes/no` |
| Pre-send blind spot check | `Is there anything here that could be misinterpreted or read in a negative way?` |
| A/B variants | `Write two meaningfully different versions of [element] so I can split-test them` |

---

## Common Mistakes to Avoid

| Mistake | Fix |
|---------|-----|
| Prompting AI with no stated goal | Always include the desired reader action in the prompt |
| Multiple CTAs in one email | Pick one; save others for follow-up emails |
| Sending without a tone audit | Always run the pre-send review prompt before high-stakes emails |
| Committing to the first subject line | Always generate at least 5 options and choose deliberately |
| Vague timeline in CTA | Use specific dates/times — never "soon" or "when you get a chance" |
| Testing multiple variables at once | Change exactly one element per test cycle |
| Writing from your own mood | Write for the reader's mood — run the tone audit |
| Overly long emails with many ideas | One email = one goal = one CTA; cut everything else |
