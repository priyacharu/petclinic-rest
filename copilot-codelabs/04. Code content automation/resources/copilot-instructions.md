## Git Commit Standards

### Commit Message Format
- Use semantic commit messages following Conventional Commits specification
- Format: `type(scope): description`
- Write commit messages in imperative mood ("add feature" not "added feature" or "adds feature")
- Keep subject line under 72 characters
- Use lowercase for type and scope (except for breaking changes)
- Do not end subject line with a period

### Commit Types
- Organize the commits by type. Analyze the files and create separate commits.
- **feat**: New feature or functionality
- **fix**: Bug fix
- **docs**: Documentation changes only (README, JavaDoc, comments)
- **refactor**: Code refactoring without changing functionality
- **test**: Adding or updating tests
- **chore**: Maintenance tasks (dependencies, build config, CI/CD)
- **style**: Code style changes (formatting, whitespace, no code change)
- **perf**: Performance improvements
- **ci**: CI/CD configuration changes
- **build**: Build system or dependency changes

### Scope Guidelines
- Use scope to indicate the area of codebase affected
- Common scopes: `api`, `service`, `repository`, `controller`, `dto`, `mapper`, `config`, `security`, `test`
- Use package name or module name when relevant (e.g., `owner`, `pet`, `visit`)
- Omit scope if change affects multiple areas or is unclear
- Examples:
  - `feat(api): add pagination to owners endpoint`
  - `fix(service): resolve N+1 query in OwnerService`
  - `refactor(repository): extract query method to interface`

### Commit Message Structure
```
type(scope): short description

Longer description explaining:
- What was changed and why
- Any relevant context or background
- References to issues or tickets

BREAKING CHANGE: description of breaking change (if applicable)
```

### Subject Line Best Practices
- Start with a verb: "add", "fix", "update", "remove", "refactor"
- Be specific: "add user authentication" not "add auth"
- Focus on what changed, not why (why goes in body)
- Examples:
  - ✅ `feat(api): add pagination support to owners endpoint`
  - ✅ `fix(service): handle null owner in findById method`
  - ❌ `feat: stuff` (too vague)
  - ❌ `fix: bug` (not descriptive)

### Commit Body (Optional but Recommended)
- Use when commit needs more explanation
- Explain the "why" behind the change
- Reference related issues: "Fixes #123" or "Closes #456"
- Describe any side effects or considerations
- Separate paragraphs with blank lines
- Wrap lines at 72 characters

### Breaking Changes
- Mark with `BREAKING CHANGE:` in commit body
- Include description of what broke and migration path
- Use `!` after type/scope: `feat(api)!: change response format`
- Examples:
  ```
  feat(api)!: change owner response structure
  
  BREAKING CHANGE: Owner response now includes nested pets array.
  Previous format had pets as separate endpoint. Migration:
  1. Update client code to handle nested structure
  2. Remove separate /owners/{id}/pets endpoint calls
  ```

### Commit Granularity
- One logical change per commit
- Commit related changes together
- Separate unrelated changes into different commits
- Good granularity examples:
  - ✅ One feature = one commit (or logical commits if feature is large)
  - ✅ One bug fix = one commit
  - ✅ Refactoring = separate commit from feature addition
  - ❌ Multiple unrelated changes in one commit
  - ❌ Partial feature implementation (unless it's a WIP commit)

### What to Include in Commits
- ✅ Working code that compiles and passes tests
- ✅ Related test changes with feature/fix
- ✅ Documentation updates for new features
- ✅ Configuration changes related to the change
- ✅ Migration scripts for database changes

### What NOT to Commit
- ❌ Debugging code or commented-out code
- ❌ Temporary files or build artifacts
- ❌ Secrets, credentials, or sensitive data
- ❌ Large binary files
- ❌ Code that doesn't compile or breaks tests
- ❌ Unrelated changes (use separate commits)

### Commit Frequency
- Commit often (small, logical commits)
- Commit when a logical unit of work is complete
- Don't wait until end of day to commit
- Use meaningful commit messages even for small changes

### Commit Message Examples

**Good Examples:**
```
feat(api): add pagination to owners endpoint

Implements pagination using Pageable interface with default
page size of 20. Adds query parameters: page, size, sort.

Closes #123
```

```
fix(service): handle null owner in findById method

Added null check and proper exception handling to prevent
NullPointerException when owner ID doesn't exist.

Fixes #456
```

```
refactor(repository): extract custom query to interface

Moved complex JPQL query from implementation to repository
interface for better testability and maintainability.
```

```
docs(api): update OpenAPI spec for new pagination params

Updated openapi.yml to document new pagination query parameters
and response structure changes.
```

**Bad Examples:**
```
fix: bug
```
```
update code
```
```
WIP
```
```
feat: added stuff
```

### Commit Messages for PR Auto-Generation
**To maximize CoPilot ability to auto-fill PR templates, follow these guidelines:**

**1. Use Semantic Commit Types:**
- CoPilot analyzes commit types to auto-select PR type
- `feat(...)` → PR type: "New feature"
- `fix(...)` → PR type: "Bug fix"
- `docs(...)` → PR type: "Documentation update"
- `refactor(...)` → PR type: "Refactoring"
- `perf(...)` → PR type: "Performance improvement"

**2. Include Clear Descriptions:**
- CoPilot extracts commit subjects for "Changes Made" section
- Use descriptive subjects: "add pagination" not "add stuff"
- Each commit subject becomes a bullet point in PR description

**3. Reference Issues in Commits:**
- Include issue references in commit body: "Fixes #123" or "Closes #456"
- CoPilot automatically extracts these for "Related Issues" section
- Use standard formats: `Fixes #`, `Closes #`, `Related to #`

**4. Use Descriptive Branch Names:**
- CoPilot uses branch names to infer PR purpose
- Good: `feat/api-pagination`, `fix/service-null-handling`
- Bad: `my-changes`, `fix`, `update`

**5. Structure Commit Body for Context:**
- CoPilot uses commit body to understand "why" behind changes
- Include context: "This change improves performance by..."
- Explain impact: "Reduces query time from X to Y"

**Example Commit for Auto-Fill:**
```
feat(api): add pagination to owners endpoint

Implements pagination using Pageable interface with default
page size of 20. This improves performance when dealing with
large datasets by allowing clients to request specific pages.

Adds query parameters: page, size, sort.

Closes #123
```

**CoPilot will auto-fill PR with:**
- Type: "New feature" (from `feat`)
- Changes: "Add pagination to owners endpoint", "Add query parameters"
- Description: Summary from commit body
- Related Issues: "Closes #123"