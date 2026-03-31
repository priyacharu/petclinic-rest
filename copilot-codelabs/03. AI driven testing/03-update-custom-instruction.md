# Exercise 3: Update Custom Instructions to Include Integration Tests with Hurl

## Objective
Update the existing repository-level custom instruction file to add guidance for writing integration tests using [Hurl](https://hurl.dev) — a plain-text HTTP testing tool.

## Why this matters
Unit tests validate isolated logic, but integration tests verify that your API endpoints behave correctly end-to-end. Hurl provides a simple, readable, version-controllable format for HTTP integration tests that fits naturally into CI/CD pipelines and pairs well with AI-assisted test generation.

## What you will build
- An updated version of `.github/instructions/testing.instructions.md` that appends an **Integration Testing with Hurl** section
- Hurl file conventions and example patterns for this project's REST API

## Prerequisites
- Exercise 1 completed: `.github/instructions/testing.instructions.md` already exists
- Hurl installed locally (`brew install hurl` on macOS, or see [hurl.dev/docs/installation](https://hurl.dev/docs/installation.html))

## Exercise steps

1. Open the existing instruction file:
   - `.github/instructions/testing.instructions.md`

2. Copy `resources/03-testing.instructions.md` content into `.github/instructions/testing.instructions.md`:
   - Open `resources/03-testing.instructions.md`, review it and tweak if necessary
   - Copy all its content
   - Paste the content into `.github/instructions/testing.instructions.md`

3. Save the file.

4. Verify Copilot references the updated integration test guidance

   - Open the Copilot chat or assistant integrated in your editor.
   - Ask: `"how should I write integration tests for this project?"`
   - Confirm the response references Hurl, mentions `.hurl` files stored under `tests/hurl/`, and includes guidance on assertions or running tests.
   - If the assistant does not reflect these updates, confirm that the file was saved and retry the query.

## Validation
- `.github/instructions/testing.instructions.md` contains the new **Integration Testing with Hurl** section
