# Prompt: Exercise 1 — Impact map

You can **run this in one step** or copy the instruction above the rule and work section-by-section.

**Quick run:** In GitHub Copilot Chat, paste **only the text below** the `---` line (or attach `requirements.md` with `@` and paste the same block—the paths in the block still tell the model which file is canonical).

**Workspace root:** These paths assume the repository root is the **`petclinic-rest`** folder. If you open a parent folder in the editor, prefix accordingly.

**Alternative:** Use Copilot’s **`/create-impact-map`** with **`docs/requirements.md`** — the repo prompt writes **`impact-map.md`** under **`docs/<topic>/`**. See the [lab README](../README.md).

---

You are helping with architecture discovery for a small product team.

**Context file (read it in this workspace):**  
`copilot-codelabs/01. Architecture and Design with MCP/requirements.md`  
(If your class uses `docs/requirements.md` instead, read that file and treat it as the single source of truth.)

Using that document as the single source of truth, produce an **impact map** in Markdown with these sections:

1. **Requirements and acceptance criteria** — Table or bullet list of the main functional requirements and their acceptance criteria; flag gaps, ambiguities, or dependencies on assumptions.

2. **Affected modules, dependencies, and risks** — Identify likely application/domain modules (or bounded contexts), technical dependencies (databases, APIs, queues, email/SMS, auth, etc.), and delivery risks (performance, consistency, security, operational complexity).

3. **Assumptions and scope boundaries** — Explicit assumptions; clear **in scope** vs **out of scope** for this design iteration.

Write clearly so the output can be saved as **`impact-map.md`** next to the lab README (or where the instructor asked), without extra commentary outside those three sections.
