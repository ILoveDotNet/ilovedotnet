# ✅ GitHub Copilot: Commit Message Instructions

Please analyze my staged changes and study how it is implemented. Take your time to understand the context and dependencies. Once you've fully grasped the code structure, Write a clear and descriptive commit message based **only on the provided changes**. Follow these **strict formatting and content rules**.

---

## 🧠 General Rules

- Focus on **communicating the purpose** of the change, not what changed.
- Use **past tense** in the body.
- Use the **imperative mood** in the summary (e.g., "Fix login bug").
- If a file is **renamed or moved**, mention only that and ignore other details.

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

Author: `git config user.name` Co-Author: Copilot


---

## 🔍 Contextual Awareness

- When necessary, **analyze the entire solution** to gain context on the change's purpose.

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

---

Happy committing! 🧠✍️