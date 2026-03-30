---
title: Create PR (auto-generate title & description)
description: |
  Generate a complete GitHub Pull Request title, description, labels, reviewers and recommended `gh` commands
  from the current branch, commit messages, and changed files. Useful for auto-filling PR templates and
  producing a ready-to-run `gh pr create` command or copyable PR body.
inputs:
  - name: branch
    required: false
    description: Branch to create PR from (defaults to current git branch)
  - name: base
    required: false
    description: Target branch for PR (default: `main`)
  - name: remote
    required: false
    description: Git remote to push to (default: `origin`)
  - name: open
    required: false
    description: If true, include exact `gh` command to open PR (default: false)
---

Task: Automate PR creation

Context available: commit history on branch, diff (changed files), repository PR template (if present), and test files.

Instructions for the agent using this prompt:

1. Inspect the recent commits on `branch` (or current branch if not provided). Group related commits into a concise summary.
2. Inspect the changed files and categorize them (API, tests, docs, config, build). Detect added/updated tests and documentation changes.
3. Use the project's PR template if present; otherwise produce a PR body that follows this structure:
   - Summary: one-sentence PR summary
   - Motivation/Why: brief reason for change
   - Type of Change: pick one (Bug fix / New feature / Refactor / Docs / Test / Chore)
   - Changes: bullet list of main code changes and affected modules
   - Testing: list tests added/updated and suggested manual steps
   - Risk/Impact: short assessment and backward compatibility notes
   - Commands: git/gh commands to push and open the PR (when `open=true`)

4. Suggest reviewers and labels by heuristic:
   - If tests changed → add `test` label
   - If API controllers or DTOs changed → add `api` label and suggest `@team-api` reviewer
   - If only docs changed → add `docs` label

Output format (exact):
```
Title: <one-line title>
Body:
<full markdown PR body>
Labels: label1,label2
Reviewers: @handle1,@handle2
Commands:
git push <remote> <branch>
gh pr create --title "<Title>" --body - --base <base>  # (or include suggested command with --body-file)
```

Examples (invocations):
- `Create PR` (uses current branch, base `main`, open=false)
- `Create PR branch=feat/api-pagination base=main open=true`

Notes for users:
- The agent will not run `git` or `gh` commands automatically unless explicitly allowed; it will output recommended commands.
- Review and edit the generated PR body before running any `gh` command.

Helpful tips for best results:
- Use semantic commit messages (feat/fix/docs) and descriptive branch names to improve auto-detection.
- Ensure local commits are pushed or available for inspection by the agent.
