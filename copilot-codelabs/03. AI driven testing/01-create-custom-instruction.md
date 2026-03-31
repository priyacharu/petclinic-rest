# Exercise 1: Create a Custom tester instruction file

## Objective
Create a repository-level custom instruction file for automated testing guidance.

## Why this matters
Custom instruction files help standardize AI test generation across teams, making tests more predictable, robust, and aligned with project standards.

## What you will build
- A new custom instruction markdown file stored at: `.github/instructions/testing.instructions.md`
- An exercise walkthrough that encodes the **Testing Guidelines** below

## Exercise steps

1. Create or verify the folder path:
   - `.github/instructions/`

2. Create the file:
   - `.github/instructions/testing.instructions.md`

3. Copy `resources/01-testing.instructions.md` content into `.github/instructions/testing.instructions.md`:
   - Open `resources/01-testing.instructions.md`, review it and tweak if necessary
   - Copy all its content
   - Paste the content into `.github/instructions/testing.instructions.md`

4. Verify Copilot references the repo-level testing instructions

   - Open the Copilot chat or assistant integrated in your editor.
   - Ask the question: "what are the testing guidelines for this project"
   - Confirm the assistant's response includes key headings or content from the file you created, for example: "Testing Philosophy", "Test Writing Guidelines", "Test Quality", or example test snippets.
   - If the assistant does not reference the repo-level instructions, ensure the file `.github/instructions/testing.instructions.md` exists and contains the guidance, then retry the query.

## Validation
- Confirm file exists and content is correct