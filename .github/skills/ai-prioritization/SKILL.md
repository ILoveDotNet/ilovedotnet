---
name: ai-prioritization
description: >
  Evaluate, rank, and communicate work priorities using AI as a structured thinking partner.
  Covers the impact-urgency-alignment framework, OKR mapping, scoring models, stakeholder
  communication, daily/weekly workload balancing, output validation, and adaptive criteria
  management over time.
  WHEN: prioritize projects, rank tasks, map work to OKRs, build a scoring model, communicate
  priorities to leadership, validate AI rankings, adapt priorities as goals change.
---

# AI Prioritization

## Overview

This skill turns AI into a systematic prioritization engine — one that can evaluate projects
against company goals, score them objectively, and surface which work deserves attention right
now versus later. The underlying principle: **prioritization is a continuous practice, not a
one-time exercise. Design a system you revisit often, and let AI do the heavy lifting.**

---

## Core Rules

1. **Anchor every prioritization decision to three axes: impact, urgency, and alignment.**
   Work that scores low on all three is a candidate for deferral or elimination.
2. **Urgency is not the same as priority.** Just because something is due tomorrow does not
   make it the most important thing to do. Validate urgency by examining dependencies, not deadlines.
3. **OKRs are only useful if you consult them.** Load your company objectives into AI at the
   start of every prioritization session; never prioritize from memory alone.
4. **Use a scoring model to remove subjectivity.** Numbers make priorities legible and defensible
   to leadership. Let AI construct and run the model — you supply the criteria.
5. **Always validate AI output against your own judgment.** AI has no access to political context,
   interpersonal dynamics, or unspoken constraints you hold. It is a thought partner, not a
   decision-maker.
6. **Communicate priorities with evidence, not assertions.** "This is important" is not enough.
   Surface the scoring data, the OKR alignment, and the dependency chain when presenting to
   stakeholders.
7. **Update AI as business context changes.** Stale inputs produce stale rankings. Treat your
   AI tool as a team member who needs ongoing briefings, not a static calculator.
8. **Close the feedback loop.** After projects are completed, brief AI on what worked and what
   did not. This improves the quality of future prioritization cycles.

---

## Workflows

### 1. Define the Prioritization Criteria

**The problem:** Teams often prioritize based on whoever is loudest or most recent, rather than
on a consistent set of criteria applied equally across all work.

Before evaluating any project, define the three core dimensions you will use to assess priority:

- **Impact** — Does this drive revenue, grow users, or improve team effectiveness?
- **Urgency** — Does delaying this block another team or create downstream risk?
- **Alignment** — Does this ladder up to a stated company goal or OKR?

**Prompt template:**
```
These are our company goals for this period: [PASTE OKRs OR GOALS]
These are the projects currently under consideration: [LIST PROJECTS]
Using impact, urgency, and strategic alignment as evaluation criteria,
give me an initial ranking with a one-sentence justification for each project's position.
```

> **Rule:** Run this prompt at the start of every planning cycle — quarter, month, or sprint.
> Never prioritize from memory.

---

### 2. Evaluate Urgency with AI

**The problem:** Everything is called urgent. Real urgency is defined by dependency chains, not
by who sent the loudest message.

Use AI to distinguish genuine urgency from perceived urgency by examining what gets unblocked
if you act now.

**Prompt template:**
```
A new project has been flagged as urgent: [PROJECT DESCRIPTION]
These are the other projects currently in flight: [LIST ACTIVE PROJECTS]
Identify any concrete dependencies this urgent item would unblock
if we prioritized it immediately. Are those dependencies significant enough
to warrant reprioritizing our current work?
```

**Urgency validation checklist:**
- [ ] Does acting on this immediately unblock another team or person?
- [ ] Does delaying this create a measurable risk to an active project?
- [ ] Is the deadline externally imposed (customer, contract) or internally invented?
- [ ] If we do this now, which current priority do we displace — and what is the cost of that?

> **Diagnostic prompt:**
> ```
> If we drop our current highest-priority work to address [URGENT ITEM],
> what is the realistic downstream cost? List the dependencies we would delay
> and the risk level of each.
> ```

---

### 3. Map Work to Strategic Alignment (OKRs)

**The problem:** OKRs are set at the start of the year, then ignored. When teams prioritize
without reference to stated goals, effort diffuses across low-value work.

Use AI to run a ruthless alignment check: which projects on the list actually connect to a
company objective, and which are orphaned work that has accumulated over time?

**Prompt template:**
```
These are our current company OKRs: [PASTE OKRs]
These are all the projects we are currently working on or considering:
[LIST ALL PROJECTS]
Classify each project as:
  - Directly supports an OKR (specify which one)
  - Indirectly supports an OKR (explain the connection)
  - No clear OKR alignment
Sort the output by alignment strength, strongest first.
```

**Using the alignment map:**
- Projects with direct OKR alignment → highest scheduling priority
- Projects with indirect alignment → second tier; review whether indirect connection is real
- Projects with no alignment → strong candidates for deferral or cancellation

> **Verification rule:** Always review AI's alignment classifications manually.
> AI may misread the connection between a project and an OKR, especially for domain-specific work.
> If the classification looks wrong, correct it and feed the correction back to AI.

---

### 4. Quantify Impact Using a Scoring Model

**The problem:** Comparing projects by feel is unreliable and indefensible. A scoring model
converts qualitative judgment into a structured number that can be compared, sorted, and explained.

Let AI build and run the scoring model — you define the factors.

**Prompt template:**
```
I want to score projects for prioritization using the following factors:
  - Strategic impact (1–5): How directly does this drive our top company goal?
  - Urgency (1–5): How much does delaying this harm active dependencies?
  - Effort-to-value ratio (1–5): How much output do we get relative to the work required?
  - OKR alignment (1–3): Direct / indirect / none

Here are the projects to score: [LIST PROJECTS WITH BRIEF DESCRIPTIONS]

Score each project on every factor, calculate a total, and rank them from highest to lowest.
Include a one-sentence rationale for any score you assign.
```

**Score interpretation guide:**

| Total score range | Recommended action |
|---|---|
| 12–18 | Highest tier — schedule immediately |
| 7–11 | Mid tier — schedule in next planning cycle |
| 3–6 | Low tier — defer or deprioritize |
| Below 3 | Candidate for cancellation — revisit quarterly |

> **Rule:** The scoring model is a decision aid, not a decision-maker. If your judgment
> conflicts with a score, investigate why. The discrepancy is usually useful signal.

---

### 5. Communicate Priorities with Clarity

**The problem:** Presenting priorities without evidence forces stakeholders to either trust you
blindly or challenge every decision. Evidence makes priorities defensible and speeds alignment.

Use AI to help you build a concise, evidence-backed communication package before taking
your priority ladder to leadership.

**Prompt template:**
```
I need to present a priority ranking to leadership. Here is the ranked list: [LIST]
For each project, help me generate:
  1. A one-sentence statement of why this is rank [N] using the scoring data
  2. The OKR it supports (or why it is deprioritized for lacking alignment)
  3. The key dependency or urgency factor that influenced its position
Format as a table I can paste into a slide or document.
```

**Communication checklist:**
- [ ] Priority rank stated clearly (1 = highest)
- [ ] Scoring data referenced (not just the conclusion)
- [ ] OKR alignment called out for top-tier projects
- [ ] The displacement cost of top priorities is acknowledged (what we are not doing)
- [ ] Low-priority items listed with a brief explanation of why they are deprioritized

> **Rule:** "We think this is important" is not a priority rationale.
> "This project scores highest on strategic impact and directly supports OKR #2" is.

---

### 6. Apply AI Rankings to Real Workloads

**The problem:** A ranked project list does not map directly to a usable weekly task plan.
High-priority projects need consistent time; low-priority work still needs to advance.
AI can translate the ranking into a practical schedule.

**Prompt template:**
```
Here is our current priority ranking: [RANKED LIST]
Here is everything the team needs to accomplish this week: [TASK LIST]
Build a weekly task plan that:
  - Allocates the majority of available hours to the top 2–3 priorities
  - Reserves [X]% of capacity for lower-priority tasks that cannot be deferred further
  - Flags any tasks that are not associated with a current priority and suggests whether
    to defer, delegate, or eliminate them
```

**Time allocation model:**

| Priority tier | Recommended weekly time allocation |
|---|---|
| Top priority (rank 1–2) | 50–60% of available capacity |
| Mid priority (rank 3–5) | 25–35% of available capacity |
| Low priority / maintenance | 10–20% of available capacity |

> **Weekly prompt:**
> ```
> Given our priorities this week and the task list below, build the most
> impact-focused daily plan for the team. Flag any day where low-priority
> work is consuming more than 25% of capacity.
> [PASTE TASK LIST]
> ```

---

### 7. Validate AI Outputs with Judgment

**The problem:** AI can produce confident-sounding priorities that are contextually wrong.
Domain knowledge, political context, and interpersonal dynamics are invisible to AI.
Validation is not optional; it is part of the workflow.

**Forward validation (before acting):**
```
Here is the priority ranking AI produced: [LIST]
Here are our actual OKRs: [OKRs]
Does this ranking reflect our stated priorities?
Are the highest-impact projects at the top?
Flag any item where the AI ranking and your OKR hierarchy are in conflict.
```

**Retroactive validation (after a completed period):**
```
Here are the projects we completed last quarter: [LIST]
Looking back, which of these were genuinely high-impact?
Which consumed significant effort but produced limited results?
Does the AI ranking match what actually happened?
```

**Interpreting validation results:**

| Validation outcome | What it means |
|---|---|
| AI ranking matches reality | Inputs are well-calibrated. Continue current approach. |
| AI over-ranked low-impact work | Missing criteria or context. Add more domain specifics to prompts. |
| AI under-ranked high-impact work | OKR language is too vague. Provide more concrete success metrics. |
| Mixed results | AI needs more business context. Treat it as a new team member who needs onboarding. |

> **Rule:** If AI and your judgment consistently conflict, do not discard AI — brief it better.
> Add the context it is missing rather than abandoning the process.

---

### 8. Review and Adapt Priority Criteria

**The problem:** Business priorities shift during the year. An AI tool trained on January's
goals will produce January's rankings in September if it is never updated.

Build a regular briefing cadence to keep your AI tool current.

**Monthly update prompt:**
```
Since our last prioritization session, the following has changed:
  - [New company goals or OKR updates]
  - [Projects completed and their outcomes]
  - [Projects that were deprioritized — and whether that was the right call]
  - [New constraints or dependencies that have emerged]
Update your understanding of our priorities and flag if any of our current
top-ranked projects should be reconsidered given this new context.
```

**End-of-cycle retrospective prompt:**
```
Here is the list of projects we worked on this quarter: [LIST]
For each, note: what was the actual impact, the level of urgency we applied,
and whether it aligned to an OKR.
What patterns do you see? Where did our prioritization model serve us well,
and where did it produce poor outcomes?
```

**Adaptation checklist (run quarterly):**
- [ ] OKRs reviewed and updated in AI context
- [ ] Completed projects logged with outcome notes
- [ ] Scoring weights adjusted based on retrospective findings
- [ ] Any new constraint types (resource, regulatory, dependency) added to the model

> **Rule:** Every completed project is a training data point. Log the outcome —
> success, partial success, or failure — so the next prioritization cycle benefits from it.

---

## Quick Reference: Prompt Library

### Rapid daily prioritization
```
Here is my task list for today: [LIST]
Here are my current top priorities: [PRIORITIES]
Reorder the list so the most impactful items are at the top
and flag any tasks I should defer or delegate.
```

### Deciding whether to take on new work
```
We have been asked to take on this new project: [DESCRIPTION]
Here are our current company OKRs: [OKRs]
Here is our current priority ranking: [LIST]
Should we take this on now, defer it, or decline it?
What would we have to displace if we accepted?
```

### Breaking a tie between two equally scored projects
```
These two projects have nearly identical scores: [PROJECT A] vs [PROJECT B]
Consider: which has more irreversible consequences if delayed?
Which has the stronger OKR connection?
Which has external stakeholders waiting on its output?
Recommend one to do first and explain why.
```
