# Anatomy of a Prompt

| Component | Description |
|-----------|-------------|
| **PERSONA** | What role should the model play? |
| **INSTRUCTIONS** | What should the model do? This typically starts with an action word. |
| **INPUT CONTENT** | Text to be processed by the model. |
| **FORMAT** | Requirements around format for the output, if any. |
| **ADDITIONAL INFORMATION** | Any other helpful context or background. |

# Anatomy of a Prompt: Example

| Component | Example |
|-----------|---------|
| **PERSONA** | As a data analyst preparing a report for executives... |
| **INSTRUCTIONS** | ...generate a summary... |
| **INPUT CONTENT** | ...of key findings from the Q2 sales performance dataset... |
| **FORMAT** | ...written in natural language with 3–4 insights, and one chart recommendation. |
| **ADDITIONAL INFORMATION** | Avoid technical terms like p-values or regressions; instead, focus on business impact and trends. |

# Anatomy of a Prompt: Example 2

| Component | Example |
|-----------|---------|
| **PERSONA** | As a technical writer creating internal documentation... |
| **INSTRUCTIONS** | ...please summarize... |
| **INPUT CONTENT** | ...the following API documentation for the /login and /logout endpoints... |
| **FORMAT** | ...into a concise quick reference guide with clear descriptions and example calls. |
| **ADDITIONAL INFORMATION** | Assume the audience is familiar with REST APIs but not this particular service. Avoid overly technical jargon and keep it under 300 words. |

# Broad Categories of Prompts

| Category | Description |
|----------|-------------|
| **Zero-shot** | The prompt includes ZERO examples |
| **Few-shot** | The prompt includes a FEW contextual examples |
| **Chain-of-thought** | Used to solve complex problems by breaking them into smaller steps |

## Zero-shot Prompting
The prompt is simple and includes ZERO examples. The model was not explicitly trained for the task; rather, it uses general knowledge acquired during training.

### Example:
```
Classify the sentiment for the following
text.
Text: This course is awesome!
```

## Few-shot Prompting
The prompt includes a FEW contextual examples to enhance the model’s
performance for a given task.

### Example:
```
Classify the sentiment for the following
text as superb, meh, or neutral.
Text: This course is awesome!
Sentiment: Superb
Text: I’m really confused by this course!
Sentiment: Meh
Text: It was so-so.
Sentiment: Neutral
Text: I loved it!
Sentiment:
```

## Chain-of-thought Prompting
Used to solve complex problems by breaking them into smaller steps.

### Example:
```
A user reports that they can’t log into
your company’s web app. They’re entering
the correct username and password, but
still get an error.
What steps would you take to troubleshoot
this issue? **Think step by step.**
```

# General Strategies for Better Prompting

| Strategy | Description |
|----------|-------------|
| **Be clear on the objective** | Use action verbs, specify the audience and desired output format |
| **Offer background and context** | Provide relevant facts, data, and documents |
| **Be specific** | Use precise language, break down tasks, and quantify the ask where possible |
| **Iterate and refine** | Try different prompt lengths, phrases, and level of detail |
| **Self-reflection** | Ask for gaps in reasoning or assumptions |

# Prompting for Writers and Marketers

| Prompt Example | Important Concepts |
|----------------|-------------------|
| Summarize this blog post into a LinkedIn post targeted at mid-career tech professionals. | Audience targeting and structured outputs |
| Rewrite this product description to sound more conversational and upbeat. | Tone control and specificity |
| Generate five email subject lines for a product launch, using a playful, casual tone. | Temperature tuning and creative ideation |
| Suggest three alternative blog titles that use curiosity to hook the reader. | Creative brainstorming and style shaping |
| Create a 5-bullet executive summary of this 1,500-word case study. | Summarization and formatting control |
| Rewrite this call-to-action to sound less pushy and more helpful. | Tone control and audience sensitivity |
| Brainstorm five Instagram captions promoting a new whitepaper. | Audience-appropriate content generation |
| Critique this blog post intro for clarity, coherence, and engagement. | Output evaluation (coherence, bias) |

---

# Prompting for Developers and IT Pros

| Prompt Example | Important Concepts |
|----------------|-------------------|
| List three reasons to use Kubernetes, written for a beginner audience. | Coherence and audience focus |
| Summarize this GitHub README into three key bullet points. | Summarization and structured output |
| Create a basic AWS Lambda function in Python that returns a JSON greeting message. | Code scaffolding and API output formatting |
| Suggest first possible causes for a Docker container restarting, step-by-step. | Chain-of-thought prompting |
| Draft an onboarding checklist for setting up a local dev environment. | Structured output generation |
| Create a quick cheat sheet explaining REST vs. GraphQL to junior developers. | Comparison framing and clarity |
| Generate a sample SQL query to find the top 5 customers by total purchase amount. | Query generation and business logic prompting |
| Write a Bash script that checks server disk space and sends an email alert if usage exceeds 90%. | Automation scripting and system operations |

---

# Prompting for Business, Operations, and Project Managers

| Prompt Example | Important Concepts |
|----------------|-------------------|
| Draft a polite but firm follow-up email to a vendor about missed deadlines. | Tone control and professionalism |
| Rewrite this internal memo to sound more action-oriented. | Coherence and tone setting |
| List key risks for the next product launch, organized by likelihood. | Structured problem analysis |
| Plan a phased rollout strategy for an internal tool upgrade. | Chain-of-thought and planning |
| Create a short project status update formatted for a Slack message. Use fun emojis. | Brevity and output formatting |
| Suggest three non-confrontational ways to escalate a project timeline. | Tone sensitivity and solution framing |
| Generate a checklist for onboarding a remote employee. | Structured thinking and format control |

---

# Prompting for Creatives and Innovators

| Prompt Example | Important Concepts |
|----------------|-------------------|
| Suggest five unconventional marketing strategies for launching a new product. | Creative ideation and risk-taking |
| Write a product description for a smartwatch that brews coffee. | Creative writing and structure |
| Suggest three fresh ways to explain AI to kids aged 8–10. | Audience targeting and simplification |
| Generate five slogans for a fitness app that emphasizes speed and strength. | Tone matching and brevity |
| Brainstorm alternative launch event ideas for a new book release. | Creative exploration |
| Rewrite this brand manifesto to sound more rebellious and bold. | Tone control and voice amplification |
| Create five Instagram caption ideas promoting a luxury camping experience. | Audience-focused content and brevity |