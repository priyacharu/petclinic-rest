# Exercise 3: Generate Release Notes

## Objective
Learn how to use Copilot to automatically generate release notes from commit history and merged PRs, understand how semantic commits and PR descriptions enable better release notes, and master the art of reviewing and refining generated release notes.

## Prerequisites
✅ **Exercise 1 completed** - You should understand commit standards from Exercise 1
✅ **Exercise 2 completed** - You should understand PR creation from Exercise 2
✅ **Commit history available** - You need commits in your repository to generate release notes
✅ **Tags available (optional)** - Having version tags helps generate notes between versions

## What You'll Learn
- How to generate release notes using Copilot `/release-notes` command
- How semantic commits and PR descriptions enable automatic categorization
- How the command analyzes both commits and merged PRs
- How to review and refine generated release notes
- How to use the release notes template
- Best practices for release notes
- How to connect commits → PRs → release notes workflow

## Quick Start
1. **Create the prompt file**: Create `.github/prompts/release-notes.prompt.md` (see "Setting Up the Command" section)
2. **Generate release notes**: Use Copilot to generate release notes automatically
3. **Review and edit**: Review the generated notes and refine as needed
4. **Save to file**: Save the final release notes to `RELEASE_NOTES.md`

**Important**: 
- The `/release-notes` command must be created first in `.github/prompts/release-notes.prompt.md`
- The command analyzes your commit history and merged PRs to generate release notes
- Semantic commits (from Exercise 1) and well-structured PRs (from Exercise 2) make release notes generation much more accurate
- Always review and refine generated release notes before publishing

---
## Step 1: Create the release-note prompt file**

Create the file `github/prompts/release-notes.prompt.md` in your workspace root with the following content:

```markdown
---
description: Generate RELEASE_NOTES.md from merged PRs (between tags or last tag → HEAD)
argument-hint: <from-version> <to-version>
---

You are generating release notes for this repository.

Goal:
- Create (or overwrite) a file named **RELEASE_NOTES.md** in the repository root using the release notes template file:
  - **docs/templates/release-notes-template.md**
  - If the template file is missing, create it first using the exact structure referenced below.

Version range:
- If the user provides $1 and $2, treat them as:
  - FROM_VERSION = $1
  - TO_VERSION   = $2
- If the user provides only one argument, treat it as TO_VERSION and infer FROM_VERSION as the previous tag.
- If the user provides no arguments:
  1) Identify the most recent release tag (FROM_VERSION)
  2) Set TO_VERSION = HEAD (or an equivalent label like "Unreleased")
  3) Use the compare range FROM_VERSION...TO_VERSION

Data sources:
- Use merged PRs included in the FROM_VERSION...TO_VERSION range.
- Summarize PRs primarily from their **titles + descriptions** (and labels if available).
- Also analyze commit history for semantic commits (feat, fix, docs, etc.) to categorize changes.

Output requirements (must follow the template sections):
- Fill in these placeholders in the template:
  {{VERSION}}, {{RELEASE_DATE}}, {{FROM_VERSION}}, {{TO_VERSION}}, {{COMPARE_URL}}, {{CHANGELOG_URL}},
  {{HIGHLIGHTS}}, {{BREAKING_CHANGES}}, {{FEATURES}}, {{FIXES}}, {{IMPROVEMENTS}}, {{SECURITY}},
  {{DEPENDENCIES}}, {{DOCUMENTATION}}, {{DEPRECATIONS}}, {{MIGRATION_GUIDE}}, {{CONFIG_CHANGES}},
  {{KNOWN_ISSUES}}, {{INTERNAL}}, {{CONTRIBUTORS}}, {{INSTALL_COMMAND}}, {{UPGRADE_COMMAND}},
  {{DOCS_URL}}, {{ISSUES_URL}}, {{DISCUSSIONS_URL}}, {{REVIEW_NEEDED}}

Classification rules:
- Group entries by: Features / Bug Fixes / Improvements / Documentation / Dependencies / Security / Internal.
- Map semantic commit types to sections:
  - `feat(...)` → Features section
  - `fix(...)` → Bug Fixes section
  - `docs(...)` → Documentation section
  - `refactor(...)` → Improvements section
  - `perf(...)` → Improvements (Performance)
  - `chore(...)` → Internal Changes section
- Call out explicitly:
  - Breaking changes (commits with `!` or `BREAKING CHANGE:`)
  - Database migrations
  - Configuration changes
- If you cannot confidently populate a section, keep the template's default "No …" line AND add {{REVIEW_NEEDED}} to that section header or a short note.

Style rules:
- Use clear, user-facing language (release note tone), not raw diff narration.
- Keep bullets concise (1–2 lines each), include component/module names when helpful.
- Prefer "what changed + user impact" over implementation details.
- Extract scope from commits when available (e.g., `feat(api): ...` → "API: ...")

Execution:
1) Load the template from docs/templates/release-notes-template.md (or create it if missing).
2) Analyze commit history and PRs in the FROM_VERSION...TO_VERSION range.
3) Categorize commits by type and extract metadata (issues, contributors, breaking changes).
4) Produce a fully-populated RELEASE_NOTES.md using the template.
5) Leave the file ready for a content-team review pass (flag uncertain sections with {{REVIEW_NEEDED}}).
```

## Step 2: Understanding the prompt file

The prompt file (`github/prompts/release-notes.prompt.md`) contains instructions for Copilot on how to generate release notes. Here's what each section does:

**Front Matter (YAML header):**
- `description`: Brief description shown when listing prompts
- `argument-hint`: Help text for prompt arguments

**Goal Section:**
- Defines the primary objective: create `RELEASE_NOTES.md` using the template
- Specifies template location: `docs/templates/release-notes-template.md`

**Version Range Section:**
- Handles different argument scenarios:
  - Two arguments: explicit version range
  - One argument: infer previous tag
  - No arguments: auto-detect last tag to HEAD

**Data Sources Section:**
- Specifies what to analyze: PRs and commit history
- Prioritizes PR titles/descriptions but also uses semantic commits

**Output Requirements Section:**
- Lists all template placeholders that must be filled
- Ensures complete release notes generation

**Classification Rules Section:**
- Maps semantic commit types to release notes sections
- Defines how to categorize different types of changes
- Specifies when to mark sections with `{{REVIEW_NEEDED}}`

**Style Rules Section:**
- Ensures user-friendly language
- Focuses on impact over implementation details
- Extracts scopes from commits for better organization

**Execution Section:**
- Step-by-step process for Copilot to follow
- Ensures template is loaded and used correctly

## Step 3: Generate Release Notes with Copilot

### Method 1: Automatic Generation (Recommended)

**In Copilot chat, simply type:**
```
/release-notes
```

**Copilot will automatically:**
1. Detect the last release tag (e.g., `v3.4.0`)
2. Analyze commits from that tag to HEAD
3. Generate release notes using the template
4. Display the formatted output

### Method 2: Specify Version Range

**Generate notes between specific versions:**
```
/release-notes v3.4.0 HEAD
```

**Generate notes for a specific tag range:**
```
/release-notes v3.3.0 v3.4.0
```

**Generate notes with single version (infers previous tag):**
```
/release-notes v3.5.0
```

## Step 4: Review Generated Release Notes

### What to Review

**1. Completeness:**
- [ ] All major changes are included
- [ ] Breaking changes are clearly marked
- [ ] Migration guides are present (if needed)

**2. Accuracy:**
- [ ] Commit descriptions are accurate
- [ ] Issue references are correct
- [ ] Contributors are properly listed

**3. Clarity:**
- [ ] Descriptions are user-friendly (not too technical)
- [ ] Sections are well-organized
- [ ] Highlights capture the most important changes

**4. Markings:**
- [ ] Sections needing review are marked with `{{REVIEW_NEEDED}}`
- [ ] Breaking changes are clearly documented
- [ ] Migration steps are clear

### Common Issues to Fix

**Issue 1: Too Technical**
```markdown
❌ Bad: "Modified JdbcVisitRepositoryImpl.findAll() to alias visits.id column"
✅ Good: "Fixed SQL query error in visit repository that prevented listing visits"
```

**Issue 2: Missing Context**
```markdown
❌ Bad: "Added pagination"
✅ Good: "Added pagination to owners endpoint to improve performance with large datasets"
```

**Issue 3: Unclear Breaking Changes**
```markdown
❌ Bad: "Changed password encoding"
✅ Good: "Changed from NoOpPasswordEncoder to BCryptPasswordEncoder. Existing passwords will not work. See Migration Guide."
```

## Step 5: Edit and Refine Release Notes

### How to Edit Generated Notes

**Option 1: Edit in Copilot Chat**
- Ask Copilot to modify specific sections
- Example: "Add more context to the pagination feature description"
- Copilot will update the release notes

**Option 2: Edit the File Directly**
- Open `RELEASE_NOTES.md` in your editor
- Make manual edits
- Save the file

**Option 3: Regenerate with Refinements**
- Ask Copilot: "Regenerate release notes with more detail on breaking changes"
- Copilot will regenerate with improvements

### What to Add/Improve

**1. Add User-Facing Context:**
```markdown
# Before (technical)
- feat(api): add pagination to owners endpoint

# After (user-friendly)
- **Pagination Support**: Added pagination to owners endpoint. You can now request specific pages using query parameters: `?page=0&size=20&sort=lastName,asc`. This improves performance when dealing with large datasets.
```

**2. Enhance Breaking Changes:**
```markdown
# Before
- Changed password encoding

# After
- **Password Encoding Change**: Changed from NoOpPasswordEncoder to BCryptPasswordEncoder
  - **Impact**: Existing passwords in databases will not work
  - **Migration Required**: 
    1. Update database: `ALTER TABLE users MODIFY password VARCHAR(60)`
    2. Re-hash all passwords with BCrypt
    3. See Migration Guide section for details
```

**3. Add Migration Guides:**
```markdown
## 🔄 Migration Guide

### Database Migration
1. Backup your database
2. Run schema migration: `ALTER TABLE users MODIFY password VARCHAR(60)`
3. Re-hash passwords using BCryptPasswordEncoder
4. Test authentication before deploying
```

**4. Improve Highlights:**
```markdown
# Before
- Spring Boot upgrade
- Security improvements

# After
- **Spring Boot 3.5.7 Upgrade**: Updated to latest version with improved performance and security patches
- **Enhanced Security**: Implemented BCrypt password encoding replacing no-op encoder for production-ready security
```

## Step 5: Understanding the Template Structure

### Template Location

The release notes template is located at:
- `docs/templates/release-notes-template.md`

### Template Sections

**1. Header:**
- Version number
- Release date
- Comparison URL (GitHub compare link)

**2. Highlights:**
- Most important changes (3-5 bullet points)
- User-facing benefits

**3. Breaking Changes:**
- Changes that require user action
- Migration guides
- Marked with `{{REVIEW_NEEDED}}` if uncertain

**4. Features:**
- New functionality
- Organized by component/area

**5. Bug Fixes:**
- Issues resolved
- Impact description

**6. Improvements:**
- Enhancements (non-breaking)
- Performance improvements
- Code quality improvements

**7. Security:**
- Security updates
- Vulnerability fixes

**8. Dependencies:**
- Dependency updates
- Version changes

**9. Documentation:**
- Documentation updates
- API spec changes

**10. Deprecations:**
- Deprecated features
- Removal timeline

**11. Migration Guide:**
- Step-by-step upgrade instructions
- Database migrations
- Configuration changes

**12. Configuration Changes:**
- New configuration options
- Changed defaults
- Removed options

**13. Known Issues:**
- Current limitations
- Workarounds

**14. Internal Changes:**
- Refactoring
- Code quality improvements
- Not user-facing

**15. Contributors:**
- List of contributors
- Recognition

**16. Installation & Upgrade:**
- Installation instructions
- Upgrade steps

### Template Placeholders

The template uses placeholders that Copilot fills:
- `{{VERSION}}` → Version number (e.g., "v3.5.0")
- `{{RELEASE_DATE}}` → Release date
- `{{FROM_VERSION}}` → Starting version
- `{{TO_VERSION}}` → Ending version
- `{{COMPARE_URL}}` → GitHub compare URL
- `{{HIGHLIGHTS}}` → Generated highlights
- `{{FEATURES}}` → Generated features list
- `{{FIXES}}` → Generated bug fixes
- `{{REVIEW_NEEDED}}` → Mark sections needing review

## Step 6: How Semantic Commits Enable Better Release Notes

### Commit Type → Release Notes Section Mapping

| Commit Type | Release Notes Section | Example |
|------------|----------------------|---------|
| `feat(...)` | Features | `feat(api): add pagination` → Features |
| `fix(...)` | Bug Fixes | `fix(service): handle null` → Bug Fixes |
| `docs(...)` | Documentation | `docs: update README` → Documentation |
| `refactor(...)` | Improvements | `refactor(repo): extract query` → Improvements |
| `perf(...)` | Improvements | `perf(api): optimize query` → Improvements |
| `chore(...)` | Internal Changes | `chore: update dependencies` → Internal Changes |
| `feat(...)!` | Breaking Changes | `feat(api)!: change format` → Breaking Changes |

### Example: Commit to Release Notes

**Commit:**
```
feat(api): add pagination to owners endpoint

Implements pagination using Pageable interface with default
page size of 20. Adds query parameters: page, size, sort.

Closes #123
```

**Generated Release Notes Entry:**
```markdown
## 🚀 Features

- **API**: Added pagination to owners endpoint. Implements pagination using Pageable interface with default page size of 20. Adds query parameters: page, size, sort. (#123)
```

### Breaking Changes Detection

**Commit with breaking change:**
```
feat(api)!: change owner response structure

BREAKING CHANGE: Owner response now includes nested pets array.
Previous format had pets as separate endpoint. Migration:
1. Update client code to handle nested structure
2. Remove separate /owners/{id}/pets endpoint calls
```

**Generated Release Notes Entry:**
```markdown
## ⚠️ Breaking Changes

### API Response Structure Change
- **Changed**: Owner response now includes nested pets array
- **Impact**: Previous format had pets as separate endpoint
- **Migration Required**:
  1. Update client code to handle nested structure
  2. Remove separate /owners/{id}/pets endpoint calls
```

### Scope-Based Organization

**Commits with scopes:**
```
feat(api): add pagination
feat(service): implement pagination logic
feat(repository): add pagination queries
```

**Generated Release Notes:**
```markdown
## 🚀 Features

### API
- Added pagination to owners endpoint

### Service
- Implemented pagination logic in OwnerService

### Repository
- Added pagination queries to OwnerRepository
```

## Step 7: Best Practices for Release Notes

### Writing User-Facing Descriptions

**✅ Good Practices:**
- Write for end users, not developers
- Focus on "what" and "why", not "how"
- Use clear, non-technical language
- Highlight benefits and impact

**Example:**
```markdown
✅ Good: "Added pagination to owners endpoint. You can now request specific pages of results, improving performance when dealing with large datasets."

❌ Bad: "Modified OwnerRestController.findAll() to accept Pageable parameter and implemented pagination logic in service layer."
```

### Organizing Content

**1. Prioritize by Impact:**
- Breaking changes first
- Major features next
- Bug fixes and improvements follow

**2. Group Related Changes:**
- Group by component (API, Service, Repository)
- Group by feature area (Authentication, Data Access, etc.)

**3. Use Consistent Formatting:**
- Bullet points for lists
- Code blocks for commands
- Tables for comparisons

### Highlighting Important Information

**1. Breaking Changes:**
- Always in a dedicated section
- Clear migration instructions
- Impact description

**2. Security Updates:**
- Prominent placement
- Vulnerability details (if appropriate)
- Upgrade urgency

**3. New Features:**
- User benefits
- Usage examples
- Configuration options

### Review Checklist

Before publishing release notes, verify:

- [ ] All major changes are included
- [ ] Breaking changes are clearly documented
- [ ] Migration guides are complete (if needed)
- [ ] Descriptions are user-friendly
- [ ] Issue references are correct
- [ ] Contributors are properly credited
- [ ] Version numbers are correct
- [ ] Dates are accurate
- [ ] Links work correctly
- [ ] Sections marked `{{REVIEW_NEEDED}}` are reviewed

## Deliverable

After completing this exercise, you should have:
1. ✅ Created the `/release-notes` prompt file (`.github/prompts/release-notes.prompt.md`)
2. ✅ Understand how to generate release notes with Copilot
3. ✅ Know how to use the `/release-notes` command
4. ✅ Can review and refine generated release notes
5. ✅ Understand how semantic commits enable better release notes
6. ✅ Know the release notes template structure
7. ✅ Understand best practices for release notes


**Congratulations!** You've completed Lab2:
- ✅ Exercise 1: Commit Standards
- ✅ Exercise 2: Pull Requests
- ✅ Exercise 3: Release Notes

You now understand the complete workflow: **Commits → PRs → Release Notes**

**Next:** Apply these practices in your daily development workflow!
