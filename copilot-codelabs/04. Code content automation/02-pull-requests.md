# Exercise 2: Create and Manage Pull Requests

## Objective
Learn how to use Copilot to generate pull requests (PRs), create PR templates, and write effective PR descriptions that follow best practices.

## Prerequisites
✅ **Exercise 1 completed** - You should understand commit standards from Exercise 1
✅ **Changes committed** - You need committed changes on a branch to create a PR
✅ **Push access** - You must have push access to the remote repository

## What You'll Learn
- How to generate PRs using Copilot
- How to create and customize PR templates
- How to write effective PR descriptions
- How to summarize changes in PRs
- Best practices for PR creation

## Quick Start
1. **Verify prerequisites**: Ensure you have committed changes on a branch
2. **Create PR template**: Create a PR template file in your project
3. **Generate PR description with Copilot prompt command**: Use Copilot prompt command to generate a PR description
4. **Review and edit**: Review the generated description and edit if needed
5. **Create PR**: Submit the PR using the generated description

---
## Step 1: Create a PR Template

PR templates help ensure consistency across all PRs. Copilot automatically detects and uses templates. **Copilot can auto-fill template sections** by analyzing your commits, branch name, and file changes.

### Template Locations

Github searches for templates in these locations (relative to project root):
- `pull_request_template.md`
- `docs/pull_request_template.md`
- `.github/pull_request_template.md`
- `.github/PULL_REQUEST_TEMPLATE/pull_request_template.md`
- `PULL_REQUEST_TEMPLATE/pull_request_template.md`
- `docs/PULL_REQUEST_TEMPLATE/pull_request_template.md`

### How Copilot Auto-Fills Templates

Copilot can analyzes your changes to automatically populate template sections:

**From Commit Messages:**
- **Type detection**: Analyzes commit types (`feat`, `fix`, `docs`, etc.) to determine PR type
- **Scope detection**: Uses commit scopes (`api`, `service`, etc.) to understand affected areas
- **Description extraction**: Summarizes commit messages into "Changes Made" section

**From Branch Name:**
- Infers purpose: `feat/api-pagination` → suggests "New feature" type
- Identifies area: `fix/service-null-handling` → suggests service layer fix

**From File Changes:**
- Detects test files → auto-checks "Tests added/updated"
- Detects documentation → suggests "Documentation update"
- Analyzes code patterns → infers implementation details

**From Commit History:**
- Counts commits to estimate PR size
- Groups related commits for summary
- Identifies breaking changes from commit messages

### Create Your PR Template (Optimized for Auto-Fill)

Create `.github/pull_request_template.md` in workspace root. This template uses comments to guide Copilot on what to auto-fill:

```markdown
# Pull Request

## Summary
<!--
Copilot agent: Summarize the change based on branch diff and commit history.
Human: Adjust wording if needed.
-->

## Motivation / Why
<!--
Copilot agent: Infer the reason for the change when possible.
Human: Add product or business context if not obvious from code.
-->

## Type of Change
<!--
Copilot agent: Infer from commits and diff.
-->
- [ ] Bug fix
- [ ] New feature
- [ ] Refactor
- [ ] Documentation
- [ ] Test
- [ ] Chore / Maintenance

## Changes
<!--
Copilot agent: List the main changes and affected areas (modules, APIs, configs).
-->
- 
- 

## Testing
<!--
Copilot agent: Infer from test changes or common practices.
Human: Confirm what was executed.
-->
- [ ] Unit tests
- [ ] Integration tests
- [ ] Manual testing
- [ ] Not applicable

Details:
<!--
Copilot agent: Describe detected tests or suggest what should be tested.
-->

## Risk / Impact
<!--
Copilot agent: Highlight risks if migrations, configs, or core modules changed.
Human: Validate and add rollout considerations.
-->
- Risk level: Low / Medium / High
- Impacted components:
- Backward compatibility concerns:

## Rollout / Rollback
<!--
Copilot agent: Suggest rollback strategy if applicable.
Human: Confirm or refine.
-->

## References
<!--
Human: Add links to issues, tickets, ADRs, or specs.
-->
- Issue:
- Design doc:
- Related PRs:

## Checklist
- [ ] Code follows project standards
- [ ] Tests added or updated
- [ ] Documentation updated (if needed)
- [ ] No breaking changes (or explicitly called out)
```

**Template Sections Explained:**

- **Summary**: Copilot agent summarizes from branch diff and commits; Human adjusts wording
- **Motivation / Why**: Copilot agent infers technical reason; Human adds business context
- **Type of Change**: Copilot agent infers from commit types; Human verifies
- **Changes**: Copilot agent lists from commit messages; Human reviews completeness
- **Testing**: Copilot agent detects test files; Human confirms execution
- **Risk / Impact**: Copilot agent highlights risks; Human validates and adds rollout considerations
- **Rollout / Rollback**: Copilot agent suggests strategy; Human confirms or refines
- **References**: Copilot agent extracts issue numbers; Human adds design docs, ADRs, related PRs

---
## Step 2: Create and run the `create-pr` prompt

### Process Flow

1. **Create the `create-pr` prompt** (one-time setup):
   - Command Palette → "Create Prompt" or use the `create-prompt` command
   - Name the prompt `create-pr` and paste the prompt content
   - Save the prompt at `.github/prompts/create-pr.prompt.md` so Copilot/Chat can run it on demand (Sample `create-pr.prompt.md` is available under resources folder for reference)

2. **Generate PR using the prompt**:
   - Run the `create-pr` prompt (Command Palette → "Run Prompt" or invoke `/create-pr` in Copilot chat)
   - The prompt analyzes the current branch commits, changed files, and PR template (if present) and returns a suggested title, full PR body, labels, reviewers, and recommended `git`/`gh` commands

3. **Select base branch**:
   - The generated output will suggest likely target branches (usually `main`, `master`, `develop`)
   - Choose the target branch for your PR

4. **Select remote repository**:
   - If you have multiple remotes, pick the appropriate one (usually `origin`)

5. **Review and edit the generated description (optional)**:
   - The prompt output opens as editable markdown (or copy it into your PR template)
   - Make any final wording or context edits

6. **Create PR**:
   - Use the recommended `git`/`gh` commands provided by the prompt to push your branch and create the PR
   - Confirm creation and copy the PR link from the CLI or UI

---

## PR Best Practices

❌ **Bad PR practices:**
- Vague PR titles: "Update code"
- Missing description or "See commits"
- No testing information
- Mixing unrelated changes
- Not linking related issues
- Poor branch names

✅ **Good PR practices:**
- Clear, descriptive titles
- Comprehensive descriptions
- Testing information included
- Focused, single-purpose PRs
- Related issues linked
- Descriptive branch names

## Tips

- **Use templates**: Create a PR template to ensure consistency
- **Descriptive branches**: Name branches clearly to help Copilot understand purpose
- **Review before submit**: Always review generated descriptions
- **Edit when needed**: Don't hesitate to edit the generated description
- **Link issues**: Always link related issues in PR description
- **Test instructions**: Include clear steps for reviewers to test
- **Keep focused**: One feature/fix per PR


## Advanced: Maximizing Auto-Fill with Copilot

### How to Help Copilot Auto-Fill Better

**1. Use Semantic Commit Messages:**
Copilot analyzes commit types to auto-select PR type:
- `feat(...)` → Auto-selects "New feature"
- `fix(...)` → Auto-selects "Bug fix"
- `docs(...)` → Auto-selects "Documentation update"
- `refactor(...)` → Auto-selects "Refactoring"
- `perf(...)` → Auto-selects "Performance improvement"

**2. Use Descriptive Branch Names:**
Copilot uses branch names to infer purpose:
- `feat/api-pagination` → Understands it's a feature for API
- `fix/service-null-handling` → Understands it's a bug fix in service
- `docs/update-readme` → Understands it's documentation

**3. Include Issue References in Commits:**
Copilot extracts issue numbers from commit messages:
```bash
# Commit message with issue reference
git commit -m "fix(service): handle null owner

Fixes #123"
# Copilot agent will auto-fill: "Fixes #123" in Related Issues section
```

**4. Structure Commit Messages for Auto-Fill:**
Use clear, descriptive commit messages:
```bash
# Good - Copilot agent can extract clear information
git commit -m "feat(api): add pagination to owners endpoint

Implements pagination using Pageable interface.
Adds query parameters: page, size, sort.

Closes #123"

# Copilot agent will auto-fill:
# - Type: New feature
# - Changes: "Add pagination to owners endpoint", "Add query parameters"
# - Related Issues: "Closes #123"
```

**5. Use Consistent File Patterns:**
Copilot detects patterns to auto-check items:
- `*Test.java` files → Auto-checks "Tests added/updated"
- `README.md`, `*.md` → Suggests "Documentation updated"
- `openapi.yml` → Suggests API documentation update

## Next Steps

After completing this exercise, you should:
- Know how to generate PRs with Copilot
- Have a PR template in your project
- Understand how to write effective PR descriptions
- Be ready to create PRs following best practices

**Ready for the next exercise?** Continue to Exercise 3 (when available) to learn about summarizing PRs and spec updates.
