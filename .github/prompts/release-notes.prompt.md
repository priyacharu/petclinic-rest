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