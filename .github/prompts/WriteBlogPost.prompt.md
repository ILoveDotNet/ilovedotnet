---
agent: 'agent'
model: Claude Sonnet 4.5
tools: ['githubRepo', 'search/codebase']
description: 'Write a new blog post following iLoveDotNet guidelines'
---
Your goal is to follow #file:../../copilot/blog-generation-guidelines.md and generate a blog post for the given topic.

## Initial Assessment

Before starting, clarify:
1. **Topic and scope**: What is the exact topic to cover?
2. **Source material**: Is there any content, documentation, or reference material provided?
3. **Article structure**: Should this be a single comprehensive article (recommended) or a series? Only create a series if:
   - The topic is genuinely too large for one article (e.g., covering 5+ distinct major concepts)
   - Each part can stand alone as valuable content
   - There's a clear logical separation between parts

If the user hasn't provided topic details or you need clarification on scope, ask before proceeding.

## Implementation Steps

Follow these steps in order:

1. **Check if the category project exists**:
   - Verify if a `{Category}DemoComponents` project exists for this topic
   - If not, you'll need to create it following the guidelines in #file:../../.github/copilot-instructions.md under "Creating a New Topic/Demo Components Project"

2. **Determine publish date**:
   - Check `CommonComponents/wwwroot/atom.xml` for the latest article's publish date
   - Add 7 days to get the next Sunday (articles always publish Sundays at 22:30 UTC)

3. **Create the blog article**:
   - Follow the What-Why-How-Summary structure
   - Use `<ContentHighlight>` for key terms
   - Include practical code examples with proper escaping (use `&lt;` for `<`, `&gt;` for `>`)
   - Add table outputs in code snippets to visualize data transformations where applicable
   - Use ASCII diagrams or mermaid diagrams inside `<CodeSnippet>` for complex concepts
   - Break long code lines to prevent horizontal scrolling

4. **Create supporting infrastructure**:
   - Add entry to `{Category}LearningPath.cs` with proper metadata
   - Update `SharedModels/TableOfContents.cs` capacity by +1
   - Ensure all images use WebP format (.webp)
   - Update SEO keywords in all CommonComponents/Pages files if it's a new category

5. **Final validation**:
   - Run `dotnet format --verbosity quiet whitespace --folder` from solution root
   - Build the solution to verify no errors
   - Verify the article follows the complete checklist in the guidelines

## Requirements

* **Strictly follow** #file:../../copilot/blog-generation-guidelines.md
* Use **table outputs** in code examples to show before/after data states
* Use **ASCII or mermaid diagrams** inside `<CodeSnippet>` for visual representation of complex concepts
* All **images must be WebP format** (`.webp` extension)
* **Slug must include category** keyword (e.g., `database-normalization-eliminating-wasteful-determinism`)
* **Test the solution builds** successfully before considering the task complete

## Common Pitfalls to Avoid

1. Don't forget to add the `Channel` property in `ContentMetaData` (required field)
2. Don't forget to update `TableOfContents.cs` capacity
3. Maintain alphabetical order when adding project references
4. Always use Sunday 22:30 UTC for publish dates
5. Escape HTML entities in code snippets (`<` → `&lt;`, `>` → `&gt;`, `@` → `@@`)

If you're unsure about any step, consult the guidelines or ask the user for clarification.
