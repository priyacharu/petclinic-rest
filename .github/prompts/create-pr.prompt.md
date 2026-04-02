---
description: >
  Generate a complete GitHub Pull Request title, description, labels, reviewers and executable git/gh commands
  from the current branch, commit messages, and changed files. When `open=true`, create
  the PR automatically and only push if the branch is not already up to date on remote.
agent: "agent"
---

## Parameters

| Name | Required | Default | Description |
|------|----------|---------|-------------|
| `branch` | No | current git branch | Branch to create PR from |
| `base` | No | `main` | Target branch for PR |
| `remote` | No | `origin` | Git remote to push to |
| `open` | No | `false` | If `true`, execute PR creation and conditionally push only when needed |

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

5. If `open=true`:
  - Check whether local `branch` is ahead of `remote/branch`.
  - Execute `git push <remote> <branch>` only if local has commits not on remote.
  - If branch is already up to date on remote, skip push and continue.
  - Execute `gh pr create --title "<Title>" --body-file <temp-file> --base <base> --head <branch>`.
  - If `gh` is unavailable or command fails, clearly report the error and still output the exact commands.

Output format (exact):
```
Title: <one-line title>
Body:
<full markdown PR body>
Labels: label1,label2
Reviewers: @handle1,@handle2
Commands:
git push <remote> <branch>
gh pr create --title "<Title>" --body-file <temp-file> --base <base> --head <branch>
```

Examples (invocations):
- `Create PR` (uses current branch, base `main`, open=false)
- `Create PR branch=feat/api-pagination base=main open=true`

Notes for users:
- If `open=true`, the agent should run `gh pr create` and push only when local branch is ahead of remote.
- If `open=false`, the agent should only output the commands without executing them.
- If command execution fails, the agent must include the failure reason and next-step commands to run manually.

Helpful tips for best results:
- Use semantic commit messages (feat/fix/docs) and descriptive branch names to improve auto-detection.
- Ensure local commits are pushed or available for inspection by the agent.
