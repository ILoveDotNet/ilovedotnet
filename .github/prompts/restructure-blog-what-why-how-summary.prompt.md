---
agent: 'agent'
description: 'Restructure an existing iLoveDotNet blog/talk Razor page to follow the What, Why, How, Summary pattern.'
---

Restructure the following Razor file(s) to follow the **What / Why / How / Summary** pattern as defined in `copilot/blogpost-rules.md`.

## Files to restructure

<!-- List the file(s) to restructure here, e.g.: -->
<!-- - `SomeDemoComponents/Pages/Blogs/SomeArticle.razor` -->

## Rules

Follow `copilot/blogpost-rules.md` strictly. Key requirements:

1. Add `UseNewTableOfContentsMenu=true` to the `<Content>` tag.
2. Add `@using BaseComponents` at the top of the file (if not already present via `_Imports.razor` convention, add explicitly per blog rules).
3. Ensure `@inherits BasePage` is present.
4. Remove any existing manual Table of Contents `<ol>` / `<NavLink>` navigation lists (the new menu handles this automatically).
5. Map existing content to the correct sections:
   - **`<What>`** — Use the existing introduction/intro paragraph **exactly as-is**. If an explicit `<h3 id="introduction">` section exists, include it here too.
   - **`<Why>`** — Use the existing Why section **exactly as-is** if one exists. If there is no Why section, **write one** based on the blog post context: explain the problem being solved, what goes wrong without this knowledge/feature, and why the reader should care. Keep it concise (2–3 paragraphs).
   - **`<How>`** — Place all remaining body content **exactly as-is** here (technical steps, code examples, demos, figures, tables, etc.).
   - **`<Summary>`** — Use the existing summary section **exactly as-is** if one exists.
6. After placing content into sections, demote heading levels inside each section: any `<h3>` tags inside `<What>`, `<Why>`, `<How>`, or `<Summary>` should be changed to `<h4>`, and any `<h4>` tags should be changed to `<h5>`. This maintains proper heading hierarchy since the section components render their own heading level.
7. Do **not** lose any existing content.
8. Do **not** add content that doesn't exist in the original file (exception: the Why section if absent).
9. Keep all `<Highlight>`, `<CodeSnippet>`, `<DemoSnippet>`, `<figure>`, `<NavLink>` and other component tags unchanged.
10. Remove any `@inject` directives that become unused after the restructuring (e.g. `@inject TableOfContents tableOfContents` that was only needed for the old manual TOC).
11. Fix any pre-existing typos or factual errors you notice while restructuring — do not carry them forward.

## After restructuring

Once the files are updated, have the **`technical-content-evaluator`** agent review them for:
- Technical accuracy
- Correct section mapping (right content in the right section)
- No content loss
- New Why section accuracy (if one was written)
- Standards compliance (UseNewTableOfContentsMenu, @inherits BasePage, code escaping, Highlight usage)

Fix any issues the evaluator identifies before considering the task complete.
