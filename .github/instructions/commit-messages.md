# ✅ GitHub Copilot: Commit Message Instructions

Please analyze my staged changes and study how it is implemented. Take your time to understand the context and dependencies. Once you've fully grasped the code structure, Write a clear and descriptive commit message based **only on the provided changes**. Follow these **strict formatting and content rules**.

---

## 🧠 General Rules

- Focus on **communicating the purpose** of the change, not what changed.
- Use **past tense** in the body.
- Use the **imperative mood** in the summary (e.g., "Fix login bug").
- If a file is **renamed or moved**, mention only that and ignore other details.
- **IMPORTANT**: When analyzing git changes, look for file moves/renames by checking:
  - Files deleted and added with similar/same content
  - Path changes where the filename remains the same
  - Use `git status` or `git diff --summary` to detect renames
  - If detected, treat it as a move/rename operation using ✏️ gitmoji

---

## 📦 Commit Message Format

### **Summary Line** (max 50 characters)

- Start with the correct **gitmoji**.
- Write a **concise summary** in **imperative mood** (e.g., ✨ Add login button).
- Use **no more than 50 characters**.

### **Body** (optional but recommended, 72 characters per line)

- Separate the summary from the body with **one blank line**.
- Focus on **why** the change was made.
- Use **bullet points** for readability.
- Avoid details that are already visible in the Git diff.
- You may write as much as needed, but **wrap at 72 characters per line**.
- **Do not use vague terms** like _update_, _enhance_, _improve_, or _better_.

> ✅ **Total message length must not exceed 500 characters**

---

## 🧑‍🤝‍🧑 Authorship

This is must at the bottom of the message, include:

Author: Bhai Co-Author: Copilot


---

## 🔍 Contextual Awareness

- When necessary, **analyze the entire solution** to gain context on the change's purpose.
- **Before generating commit message, always check for file moves/renames**:
  - Run `git diff --summary` or `git status` to detect renames
  - Look for patterns where files are deleted and added in different locations
  - Check if content is identical or nearly identical between deleted and added files
  - Prioritize detecting moves over treating them as deletions + additions
- **When changes are only whitespace/formatting**:
  - Use the 🎨 gitmoji (not listed above, but valid for formatting)
  - Keep the message simple: "🎨 Format code"
  - In the body, specify which files or projects were formatted
  - Example: "🎨 Format code\n\nApplied dotnet format to ensure consistent whitespace and indentation across the solution."

---

## 🚀 Gitmoji Usage

| Gitmoji | Meaning                          |
|--------:|----------------------------------|
| 🚧     | Work in progress                  |
| ⚡️     | Improve performance               |
| 🐛     | Fix a bug                         |
| ✨     | Introduce a new feature           |
| 📝     | Add or update documentation       |
| ✅     | Add, update, or pass tests        |
| 🔒️     | Fix security issues               |
| ♻️     | Refactor code                     |
| ✏️     | Rename or move files              |
| 🎨     | Format code or improve structure  |

---

Happy committing! 🧠✍️