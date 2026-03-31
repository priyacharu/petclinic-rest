---
description: "Generate an impact map from a requirements document — architecture discovery for modules, risks, and scope boundaries"
agent: "agent"
argument-hint: "path to the requirements file (e.g. docs/requirements.md)"
---

You are helping with architecture discovery for a small product team.

**Context file (read it in this workspace):**
`${{ input }}`

Using that document as the single source of truth, produce an **impact map** in Markdown with these sections:

1. **Requirements and acceptance criteria** — Table or bullet list of the main functional requirements and their acceptance criteria; flag gaps, ambiguities, or dependencies on assumptions.

2. **Affected modules, dependencies, and risks** — Identify likely application/domain modules (or bounded contexts), technical dependencies (databases, APIs, queues, email/SMS, auth, etc.), and delivery risks (performance, consistency, security, operational complexity).

3. **Assumptions and scope boundaries** — Explicit assumptions; clear **in scope** vs **out of scope** for this design iteration.

**Output instructions:**
- Read the document's title or main heading to identify the requirement name (e.g. "Appointment Booking System").
- Convert it to a kebab-case folder name (e.g. `appointment-booking-system/`): lowercase, spaces replaced by hyphens, strip special characters.
- Create that folder **next to the requirements file** (same parent directory).
- Save the result as **`impact-map.md`** inside that new folder.
- This folder is the **shared output directory** for all artifacts derived from this requirement (impact map, high-level design, ADRs, diagrams, etc.).

Write clearly, without extra commentary outside those three sections.
