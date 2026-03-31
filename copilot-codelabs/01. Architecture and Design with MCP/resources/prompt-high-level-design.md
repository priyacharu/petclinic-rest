# Prompt: Exercise 2 — High-level design (HLD)

You can **run this in one step** after `impact-map.md` exists: paste **only the text below** the `---` line into Copilot Chat, and optionally attach `impact-map.md` / `requirements.md` with `@`.

**Workspace root:** Paths below assume the repository root is **`petclinic-rest`**. Adjust if you opened a different folder.

**Alternative:** Use **`/create-high-level-design`** with the path to **`docs/<topic>/impact-map.md`** (and **`docs/requirements.md`** if prompted). Output: **`high-level-design.md`** in that same **`docs/<topic>/`** folder. See the [lab README](../README.md).

---

You are helping draft a **high-level design (HLD)** for a web system.

**Primary input (read it in this workspace):**  
`copilot-codelabs/01. Architecture and Design with MCP/impact-map.md`

**Optional traceability:**  
`copilot-codelabs/01. Architecture and Design with MCP/requirements.md`  
(If requirements live only in `docs/requirements.md`, use that path instead.)

Produce a single Markdown document suitable for saving as **`high-level-design.md`**, with:

1. **Major components and responsibilities** — For each component or service, state its main responsibility and what it does *not* own.

2. **Integrations and end-to-end flow** — Describe the main user journey (e.g. owner books an appointment) as a sequence across components; note synchronous vs asynchronous steps where it matters. Mention external integrations (email/SMS, identity, etc.) if applicable.

3. **Key architectural decisions** — Bullet list of decisions (format: decision, rationale, main trade-off). Keep it concise (ADR-style bullets are fine).

Stay aligned with the impact map; do not invent major scope beyond what the impact map and requirements support. Use diagrams in Mermaid only if they clarify the flow.
