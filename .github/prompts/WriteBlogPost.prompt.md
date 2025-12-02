---
agent: 'agent'
model: Claude Sonnet 4.5
description: 'Write a new blog post following iLoveDotNet guidelines'
---

# 🚨 CRITICAL: Article Structure Requirements

**EVERY blog article MUST have these four components or it will not build:**

1. `<What>` - Introduction with engaging hook
2. `<Why>` - Why it matters and problems solved  
3. `<How>` - Implementation details and code examples
4. `<Summary>` - Key takeaways and conclusion

**Without all four sections properly structured, the Content component will fail.**

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

3. **Create the blog article with MANDATORY structure**:
   
   ⚠️ **CRITICAL: Every article MUST have this exact structure or it will fail to build:**
   
   ```razor
   @page "/blogs/{slug}"
   @using SharedModels
   @using BaseComponents
   @inherits BasePage

   <Content FileName=@nameof(FileName) UseNewTableOfContentsMenu=true>
       <ContentBody>
           <What>
               <!-- Introduction and what the article covers -->
           </What>

           <Why>
               <!-- Why this matters and the problems it solves -->
           </Why>

           <How>
               <!-- Implementation details, steps, code examples -->
           </How>

           <Summary>
               <!-- Key takeaways and conclusion -->
           </Summary>
       </ContentBody>
   </Content>
   ```
   
   **Each section is REQUIRED:**
   - `<What>` - **Introduction only**: Define the pattern/concept, its origins, and what the article will teach. NO problem examples or implementation details here.
   - `<Why>` - **Problems and benefits only**: Concrete problem examples with code showing what goes wrong, consequences, why traditional approaches fail, and the advantages of the solution. NO implementation details here.
   - `<How>` - **Implementation only**: Start directly with the solution implementation. All technical details, code examples, and step-by-step guides go here. NO repetition of introduction or problems.
   - `<Summary>` - Key takeaways in bullet points
   
   **Critical: Avoid Duplication Between Sections**
   - DO NOT repeat introduction material in How section
   - DO NOT repeat problem examples in How section  
   - DO NOT explain "what is X" or "why use X" in How section
   - Each section should have distinct, non-overlapping content
   
   **Additional requirements:**
   - Use `<ContentHighlight>` for key terms
   - Include practical code examples with proper escaping (use `&lt;` for `<`, `&gt;` for `>`, `@@` for `@`)
   - Add table outputs in code snippets to visualize data transformations where applicable
   - Use ASCII diagrams or mermaid diagrams inside `<CodeSnippet>` for complex concepts
   - Break long code lines to prevent horizontal scrolling
   - ALL code blocks must have `<CodeSnippet CssClass="language-csharp">` (or appropriate language)

4. **Create supporting infrastructure**:
   - Add entry to `{Category}LearningPath.cs` with proper metadata
   - Update `SharedModels/TableOfContents.cs` capacity by +1
   - Ensure all images use WebP format (.webp)
   - Update SEO keywords in all CommonComponents/Pages files if it's a new category

5. **Final validation - MANDATORY CHECKLIST**:
   
   Before completing, verify ALL of these:
   
   ✅ **Structure Verification:**
   - [ ] Article has `<What>` section with clear definition and introduction (NO problem examples)
   - [ ] Article has `<Why>` section with concrete problems and benefits (NO implementation)
   - [ ] Article has `<How>` section starting directly with solution implementation (NO introduction or problem repetition)
   - [ ] Article has `<Summary>` section with key takeaways
   - [ ] `@using BaseComponents` directive is present at top
   - [ ] All four sections are properly closed
   - [ ] NO duplication of content between What, Why, and How sections
   
   ✅ **Code Quality:**
   - [ ] All `<CodeSnippet>` tags have `CssClass` attribute
   - [ ] HTML entities properly escaped (`<` → `&lt;`, `>` → `&gt;`, `@` → `@@`)
   - [ ] Long code lines are broken for readability
   
   ✅ **Metadata:**
   - [ ] Entry added to `{Category}LearningPath.cs`
   - [ ] `TableOfContents.cs` capacity incremented by 1
   - [ ] Slug includes category keyword
   - [ ] Publish date is a Sunday at 22:30 UTC
   
   ✅ **Build Verification:**
   - [ ] Run `dotnet format --verbosity quiet whitespace --folder`
   - [ ] Run `dotnet build ILoveDotNet.slnx` with 0 errors
   
   **DO NOT consider the task complete until ALL checkboxes are verified.**

## Requirements

* **Strictly follow** #file:../../copilot/blog-generation-guidelines.md
* Use **table outputs** in code examples to show before/after data states
* Use **ASCII or mermaid diagrams** inside `<CodeSnippet>` for visual representation of complex concepts
* All **images must be WebP format** (`.webp` extension)
* **Slug must include category** keyword (e.g., `database-normalization-eliminating-wasteful-determinism`)
* **Test the solution builds** successfully before considering the task complete

## Common Pitfalls to Avoid

1. **Content Duplication**: Never repeat introduction or problem examples in the How section. What = definition, Why = problems, How = implementation.
2. Don't forget to add the `Channel` property in `ContentMetaData` (required field)
3. Don't forget to update `TableOfContents.cs` capacity
4. Maintain alphabetical order when adding project references
5. Always use Sunday 22:30 UTC for publish dates
6. Escape HTML entities in code snippets (`<` → `&lt;`, `>` → `&gt;`, `@` → `@@`)
7. Keep each section focused on its purpose - no overlap between What, Why, and How

If you're unsure about any step, consult the guidelines or ask the user for clarification.
