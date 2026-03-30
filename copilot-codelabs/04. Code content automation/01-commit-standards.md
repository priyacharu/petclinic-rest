# Exercise 1: Apply Git Commit Standards

## Objective
Verify and understand the Git Commit Standards defined in `.github/copilot-instructions.md` and learn how to create proper commit messages following best practices.

## Prerequisites
✅ **No prerequisites** - Start here! This is the first exercise in Lab2.

## What You'll Learn

- How to write semantic commit messages
- When to use different commit types (feat, fix, docs, etc.)
- How to use scopes effectively
- How to structure commit messages properly

## Quick Start

1. **Verify or create copilot-instructions.md**: Check if `.github/copilot-instructions.md` exists in workspace root. If it doesn't exist, copy content from resources (see Step 0 below)
2. **Review Commit Standards**: Read the "Git Commit Standards" section in `.github/copilot-instructions.md`
3. **Understand the format**: Learn the semantic commit message format
4. **Practice**: Create example commit messages following the guidelines

---

## Step 1: Set Up copilot-instructions.md with Git Commit Standards

Before starting, you need to ensure your `copilot-instructions.md` file contains the Git Commit Standards section.

### Check if copilot-instructions.md exists

1. **Check workspace root**:
   - Look for `.github/copilot-instructions.md` file
   - If it exists, continue to "If .github/copilot-instructions.md already exists" below
   - If it doesn't exist, continue to "If .github/copilot-instructions.md doesn't exist" below

### Option A: If .github/copilot-instructions.md doesn't exist

1. **Copy the resource file**:
   - Open `resources/copilot-instructions.md`
   - Copy all its content
   - Create `.github/copilot-instructions.md` in workspace root
   - Paste the content into `copilot-instruction.md`

### Option B: If .github/copilot-instructions.md already exists

1. **Check if Git Commit Standards section exists**:
   - Open your existing `copilot-instructions.md` file
   - Search for "## Git Commit Standards"
   - If found, you can skip this step or update it with the latest content
   - If not found, continue to step 2

2. **Append the Git Commit Standards section**:
   - Open `resources/copilot-instructions.md`
   - Copy the content starting from "## Git Commit Standards"
   - Open your `.github/copilot-instructions.md` file
   - Append the content to the end of the file (or in an appropriate location)

### Verify Setup

After copying/appending, verify:
- [ ] `.github/copilot-instructions.md` exists in workspace root
- [ ] File contains "## Git Commit Standards" section

### Review the Commit Standards

Open `.github/copilot-instructions.md` in workspace root and review the "Git Commit Standards" section. It should include:

- ✅ Commit Message Format (Conventional Commits)
- ✅ Commit Types (feat, fix, docs, refactor, test, chore, style, perf, ci, build)
- ✅ Scope Guidelines
- ✅ Commit Message Structure (subject, body, footer)
- ✅ Subject Line Best Practices
- ✅ Commit Body Guidelines
- ✅ Breaking Changes Format
- ✅ Commit Granularity
- ✅ What to Include/Exclude in Commits

### Understanding Commit Types

**When to use each type:**

- **feat**: Adding new functionality or features
- **fix**: Fixing bugs or issues
- **docs**: Documentation changes only
- **refactor**: Code restructuring without changing behavior
- **test**: Adding or updating tests
- **chore**: Maintenance tasks (dependencies, config)
- **style**: Code formatting (no logic changes)
- **perf**: Performance improvements
- **ci**: CI/CD configuration changes
- **build**: Build system changes

### Understanding Scopes

**Common scopes for Spring PetClinic:**
- `api` - API/Controller changes
- `service` - Service layer changes
- `repository` - Repository/Data access changes
- `dto` - DTO changes
- `mapper` - Mapping changes
- `config` - Configuration changes
- `security` - Security-related changes
- `test` - Test-related changes
- `owner`, `pet`, `visit` - Domain-specific changes

---

## Step 2 Generate Commit Messages using Copilot chat

1. **Make code changes to support given requirement**:

- Ask Copilot to add code and test cases to support given requirement "return 200 with empty array instead of 404 for empty owners list"
- Analyse code changes and tweak them if necessary


2. **Generate code-commit message based on changed files**

- Ask Copilot to generate code-commit message based on changed files. 
- Analyse commit message and tweak it if required

```
#Sample commit message

fix(api): return 200 with empty array for empty owners list

Ensure OwnersController.GetOwners() returns 200 OK with an empty array instead of 404 when no owners exist.

Map owner entities to DTOs and return Ok(ownerDtos) even when list is empty.
Add unit test OwnerControllerTests.GetOwners_EmptyDatabase_ReturnsOkWithEmptyArray to verify status 200 and an empty array.
Files:

OwnersController.cs (behavior change)
UnitTest1.cs (new test)
```

---

## Next Steps

After completing this exercise, you should:
- Understand how to write proper commit messages
- Know when to use each commit type
- Be ready to apply these standards when making actual commits

**Ready for the next exercise?** Continue to Exercise 2 (when available) to learn about PR creation and updates.
