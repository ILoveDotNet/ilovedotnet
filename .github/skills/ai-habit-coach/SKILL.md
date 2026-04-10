---
name: ai-habit-coach
description: >
  Design, monitor, and adapt high-impact personal habits using AI as a structured thinking partner.
  Covers outcome definition, behavior-driver mapping, trigger design, execution planning, adherence
  tracking, obstacle diagnosis, feedback loops, and progressive habit scaling.
  WHEN: build a new habit, habit keeps failing, track habits, diagnose why habit broke down,
  scale an existing habit, design a sustainable routine, stop relying on willpower, habit system review.
---

# AI Habit Coach

## Overview

This skill turns AI into a personal performance system designer. Each section addresses a
specific reason habits fail and provides a concrete, AI-assisted workflow to resolve it.
The underlying principle: **habits succeed through design, not discipline.**

---

## Core Rules

1. **Anchor every habit to a measurable outcome.** A habit without a clear result it drives is
   just noise. Define the outcome first; design the habit second.
2. **Design around your real life, not an ideal one.** A routine that fights your biology,
   schedule, or environment will always lose. Use AI to surface this misalignment before building.
3. **Eliminate the start problem with triggers.** Most habits fail at initiation, not execution.
   Attach new behaviors to existing, reliable cues so the habit begins automatically.
4. **Reduce decision fatigue with specific plans.** Vague intentions ("I'll stretch today") lose
   to a full schedule. Lock in when, where, and how — including contingency plans for disruptions.
5. **Track impact, not just consistency.** A long streak that produces no results is a vanity
   metric. Pair every habit with a secondary signal that measures real-world effect.
6. **Diagnose friction before blaming motivation.** When a habit breaks down, look for the design
   flaw — the specific friction point — before defaulting to self-criticism.
7. **Run regular systems checks.** Seasons change, schedules change, goals change. A habit that
   fit six months ago may no longer serve you. Review and adapt rather than abandon.
8. **Scale habits progressively.** Once a habit is mastered, evolve it to meet growing demands.
   Repeating the same routine indefinitely leads to plateau, not growth.

---

## Workflows

### 1. Define Desired Outcomes

**The problem:** Tracking a habit metric (e.g., "minutes of reading per day") feels productive
but may have zero connection to the goal you actually care about.

Before designing any habit, answer: *"What specific, observable change in my life does this habit
need to produce — and how will I know it's working?"* Then use AI to close the gap between the
activity and the result.

**Prompt template:**
```
My real goal is: [GOAL]
I'm currently planning to track: [HABIT]
Help me identify three specific outcomes that would prove this habit is actually
moving me toward my goal — and flag whether my current tracking plan measures any of them.
```

| ❌ Activity tracked | ✅ Outcome anchored |
|---------------------|---------------------|
| Minutes reading per day | Applied one insight from reading in a real conversation this week |
| Days exercised | Resting heart rate trend over 4 weeks |
| Meditation streak | Composure during high-pressure situations (self-rated, weekly) |

> **Rule:** If a habit cannot be traced to at least one measurable outcome, it is a candidate
> for elimination, not optimization.

---

### 2. Map Routines to Behavior Drivers

**The problem:** Most habit plans are built around an idealized version of the day rather than
the one you are actually living. When the habit fights your biology, energy patterns, or
environment, it will consistently lose.

Use AI to audit your current schedule and behavior patterns so the habit design works *with*
your reality, not against it.

**Prompt template:**
```
Here is a rough description of my typical day and energy levels: [DESCRIPTION]
I want to build this habit: [HABIT]
Identify three time windows where this habit would align with my natural energy
and existing schedule — not where I wish I could do it.
```

**Behavior driver checklist:**
- [ ] Does the habit time align with your chronotype (morning person vs evening person)?
- [ ] Does it fit within blocks you already control, or does it depend on others?
- [ ] Does it require energy you reliably have at that time, or energy you often don't?
- [ ] Does the environment at that time support or resist the habit?

> **Diagnostic prompt:**
> ```
> My biggest energy drain during [TIME WINDOW] is: [DESCRIPTION]
> Suggest three alternative approaches to [HABIT] that would work with my
> energy at that time instead of requiring me to overcome it.
> ```

---

### 3. Design Effective Triggers and Cues

**The problem:** Every new behavior requires a mental "start" signal. When that signal depends on
remembering or on motivation, it fails. When it is attached to something that already happens
automatically, it becomes reliable.

**Habit stacking** embeds a new behavior next to an existing anchor behavior, so the existing
behavior becomes the cue that fires the new one.

**Prompt template:**
```
I never forget to do: [ANCHOR BEHAVIOR]
I want to add: [NEW HABIT]
Design a habit stack that attaches the new habit to the anchor,
using environmental setup so no extra willpower is needed at execution time.
```

**Trigger design principles:**
- The anchor must be something you already do without thinking (making coffee, locking your
  computer, brushing your teeth, opening a specific app).
- The new habit must be placed *immediately after* the anchor — not "around the same time."
- Use environmental design (physical placement, visual cues, prepared materials) to eliminate
  the start effort entirely.

**Environmental setup prompt:**
```
My new habit is: [HABIT]
My chosen anchor is: [ANCHOR]
What physical or environmental changes could I make tonight so that when [ANCHOR]
happens tomorrow, [HABIT] begins automatically without any decision required?
```

> **Rule:** The best trigger is one that makes it harder *not* to do the habit than to do it.

---

### 4. Plan Habit Execution

**The problem:** A habit without a specific time, location, and contingency plan is competing
with every other priority on your schedule — and it will usually lose.

Implementation intentions ("I will do X at time Y in location Z") significantly increase
follow-through because they eliminate the in-the-moment decision about whether and when to act.

**Prompt template:**
```
Here is my schedule for the week: [SCHEDULE OR DESCRIPTION]
The habit I want to execute is: [HABIT — target duration/frequency]
Find three specific windows where this habit fits realistically.
For each option, also give me a contingency plan: if [DISRUPTION SCENARIO],
then I will [BACKUP PLAN].
```

**If-then planning prompt:**
```
Based on my schedule, what are the three most likely disruptions that will
try to derail [HABIT] this week? For each one, write a concrete if-then plan:
"If [disruption], then I will [specific action]."
```

**Implementation checklist:**
- [ ] Specific time window committed (not "in the morning" but "7:15 AM")
- [ ] Specific location identified
- [ ] Required materials or setup prepared in advance
- [ ] At least two if-then contingency plans written down

> **Rule:** Disruption is not optional — it will happen. A plan that hasn't accounted for
> disruption is a plan that will fail on the first difficult week.

---

### 5. Track Adherence with Meaningful Signals

**The problem:** Tracking streaks measures consistency, not impact. A 30-day streak of a habit
that isn't producing any change is a 30-day investment in the wrong direction.

For every habit, track *two* things: the behavior itself, and a secondary signal that captures
the effect you actually care about.

**Prompt template:**
```
I am tracking this habit: [HABIT]
My ultimate goal is: [GOAL]
Suggest one simple secondary metric I can log alongside the habit that would
tell me whether it is actually producing results — not just whether I'm doing it.
```

**Signal design examples:**

| Habit tracked | Secondary signal |
|---------------|-----------------|
| Sleep (hours) | Afternoon energy level (1–5 scale, logged next day) |
| Daily journaling | Clarity / decision quality self-rating (weekly) |
| Exercise sessions | Weekly workout completion trend (not just individual days) |
| Meditation | Composure during stressful events (event log) |

**Pattern analysis prompt:**
```
Here is my habit log for the past [N] weeks: [DATA]
And here are my secondary signal readings: [DATA]
What patterns do you see? Is there any correlation between how I'm doing
the habit and the results I'm tracking? What should I change?
```

> **Rule:** If the secondary signal isn't moving in the right direction after four weeks,
> the habit design needs review — not more willpower.

---

### 6. Diagnose Obstacles and Breakdowns

**The problem:** When a habit breaks down, the default response is self-blame. The more useful
response is a design audit: looking for the specific friction point where the system broke.

Habits don't break randomly. They break at predictable moments — preparation, initiation,
continuation, or recovery after a miss. Identifying *where* the breakdown happens makes
it repairable.

**Breakdown audit prompt:**
```
I keep skipping or failing at: [HABIT]
Here's what my week looked like when it failed: [BRIEF DESCRIPTION]
Identify the most likely friction point — not a motivation problem, but a design flaw.
Then suggest one concrete fix.
```

**Friction categories:**

| Category | Common causes | Fix pattern |
|----------|--------------|-------------|
| **Preparation** | Materials not ready, environment not set up | Prepare everything the night before |
| **Initiation** | No clear trigger, decision fatigue at start | Add a stronger cue or habit stack |
| **Execution** | Habit too long, too hard for available energy | Reduce scope; make the minimum viable version |
| **Recovery** | Skipping two days after one miss | Add an explicit rule: never miss twice in a row |

**Weekly review prompt:**
```
Here are the habits I intended to do last week and which ones I missed: [LIST]
For each miss, identify the most likely point of breakdown and one fix I can
implement before this week starts.
```

> **Rule:** No habit fails because of a character flaw. Every failure has a structural cause.
> Find it, fix it, and move on.

---

### 7. Adjust Habits Through Feedback Loops

**The problem:** Life changes — seasons, workloads, relationships, health — but habit systems
often stay static. A routine that worked in one context may actively resist you in a different one.

The solution is to treat the habit system as a living document that gets a brief scheduled
review, not a fixed commitment that either succeeds or fails.

**Weekly systems check prompt (5 minutes):**
```
Here is what I committed to this week: [HABITS]
Here is what actually happened: [BRIEF SUMMARY]
What is one small tweak that would make my habit system better fit my life
right now — not the life I had when I designed it?
```

**Seasonal / context change prompt:**
```
My circumstances have changed in this way: [CHANGE — e.g., new job, season, health event]
These are my current habits: [LIST]
Which habits are now misaligned with my new reality, and what adaptation
would restore their fit without abandoning the underlying goal?
```

**Review cadence:**
- **Weekly:** 5-minute check — what worked, what didn't, one tweak
- **Monthly:** Broader audit — are outcomes still being reached? Do goals still reflect priorities?
- **Quarterly:** Full system review — what habits to keep, evolve, pause, or replace?

> **Rule:** The goal of a review is improvement, not judgment. Adapt the system;
> don't grade yourself.

---

### 8. Scale Habits for Long-Term Growth

**The problem:** A habit that stays the same while your goals grow will eventually produce
diminishing returns. What earned the result at one level won't earn it at the next.

Progressive habit scaling means deliberately increasing the challenge or depth of an existing
habit — not adding more habits, but advancing the ones already working.

**Scaling prompt:**
```
I've been doing [HABIT] consistently for [DURATION] and it feels comfortable.
My larger goal is: [GOAL]
Design a Level 2 version of this habit that increases impact toward that goal
without changing the time investment by more than [X minutes/days].
```

**Scaling patterns:**

| Habit type | Level 1 | Level 2 |
|------------|---------|---------|
| Focus practice | 10-min daily meditation | 20-min deep session before high-stakes meetings |
| Learning habit | Read 15 min/day | Apply one concept per week in a real situation |
| Physical training | 3 x 30-min steady-state cardio | 2 x HIIT + 1 x long session |
| Writing practice | Daily journaling (freeform) | Weekly structured reflection with action items |

> **Rule:** The habits that produce the most growth are not the ones repeated most often —
> they are the ones that evolve as you do. Same skill, higher demand, continued progress.

---

## Quick-Start: One-Week Habit Launch Checklist

Use this when starting a new habit from scratch:

- [ ] **Day 1 — Outcome:** Write one measurable goal. Ask AI to identify three outcomes that confirm the habit is working.
- [ ] **Day 1 — Alignment:** Describe your real schedule and energy pattern. Ask AI to find the best window.
- [ ] **Day 2 — Trigger:** Identify an anchor behavior you never skip. Design an environment that makes the habit automatic.
- [ ] **Day 2 — Plan:** Lock in specific time, location, and two if-then contingencies.
- [ ] **Day 3 — Signals:** Choose a secondary metric to track alongside the behavior.
- [ ] **End of Week 1 — Review:** Run the 5-minute systems check prompt. Make one small adjustment.

---

## Prompt Library

| Situation | Prompt |
|-----------|--------|
| Starting fresh | `"I want to build [HABIT]. My real goal is [GOAL]. Help me design a complete habit system: outcome, trigger, schedule, and tracking signal."` |
| Habit keeps failing | `"I keep failing at [HABIT]. Here's what my week looked like: [CONTEXT]. Find the friction point and suggest one fix."` |
| Habit feels stale | `"[HABIT] feels easy now. Design a Level 2 version that advances toward [GOAL] without burning me out."` |
| Life just changed | `"My circumstances changed: [CHANGE]. Review my habit system and tell me what needs to adapt."` |
| Weekly review | `"Here's what happened this week with my habits: [SUMMARY]. What's one tweak that makes next week better?"` |
| Energy audit | `"My biggest energy drain is at [TIME]. Suggest three approaches to [HABIT] that work with my energy at this time."` |
