# Code Content Automation

This folder contains codelab exercises for automating code-related content — commit messages, pull requests, and release notes — using AI and GitHub Copilot.

## Exercises

1. [Apply Git Commit Standards](01-commit-standards.md)

> The exercise covers setting up `.github/copilot-instructions.md` with Conventional Commits standards and practising semantic commit message authoring using Copilot.

2. [Create and Manage Pull Requests](02-pull-requests.md)

> The exercise includes creating a PR template in `.github/pull_request_template.md` and using the `/create-pr` custom prompt to auto-generate PR titles, descriptions, labels, and reviewer suggestions from commit history and changed files.

3. [Generate Release Notes](03-release-notes.md)

> The exercise covers creating a `.github/prompts/release-notes.prompt.md` command and using it to automatically categorise commits and merged PRs into a structured `RELEASE_NOTES.md` file.

## Resources

Supporting files referenced by the exercises are stored in the [`resources/`](resources/) folder:

| File | Used by |
|---|---|
| `copilot-instructions.md` | Exercise 1 — copy to `.github/copilot-instructions.md` |
| `pull_request_template.md` | Exercise 2 — copy to `.github/pull_request_template.md` |
| `create-pr.prompt.md` | Exercise 2 — copy to `.github/prompts/create-pr.prompt.md` |
