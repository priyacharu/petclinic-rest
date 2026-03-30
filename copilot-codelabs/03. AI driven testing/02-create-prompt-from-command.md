# Exercise 2: Generate a custom test-generation prompt with `/create-prompt`

## Objective
Create a reusable custom prompt `/generate-unit-test` using the `/create-prompt` command that generates unit tests for selected functions or files.

## Why this matters
Using `/generate-unit-test` helps standardize AI test generation workflow and lets developers experiment with prompt style, coverage, and scenario depth.

## What you will build
- A new prompt definition in your local workspace
- A focus on function-level or file-level test generation, with a strategy anchored by the existing `Testing Guidelines`
- Exploration steps for how to tune the prompt

## Exercise steps

1. Read the existing policy that you already stored in `.github/instructions/testing.instructions.md`.
2. Create a custom prompt using `/create-prompt` in your CoPilot chat interface. Use the following sample as starting point and save it under `.github/prompts/generate-unit-test.prompt.md`:

```markdown
---
agent: 'agent'
description: 'Generate unit tests for selected functions or files'
---
## Task
Analyze the selected function/files and generate focused unit tests that thoroughly validate its behavior.
## Test Generation Strategy
1. **Core Functionality Tests**
- Test the main purpose/expected behavior
- Verify return values with typical inputs
- Test with realistic data scenarios
2. **Input Validation Tests**
- Test with invalid input types
- Test with null/undefined values
- Test with empty strings/arrays/objects
- Test boundary values (min/max, zero, negative numbers)
3. **Error Handling Tests**
- Test expected exceptions are thrown
- Verify error messages are meaningful
- Test graceful handling of edge cases
4. **Side Effects Tests** (if applicable)
- Verify external calls are made correctly
- Test state changes
- Validate interactions with dependencies

Create tests that give confidence the function works correctly and help catch regressions
```

3. Experiment and iterate:
   - Add or modify prompt sections
   - Try “/generate-unit-test OwnersController.cs” and “/generate-unit-test PetClinicDbContext".
   - Confirm the generated tests follow AAA/Given-When-Then, have clear names, and include success/error/edge scenarios.

---

## Validation
- The test generation example is present and executable via `/generate-unit-test`.
- Generated test code is validated against the `Testing Guidelines` in `.github/instructions/testing.instructions.md`. 