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