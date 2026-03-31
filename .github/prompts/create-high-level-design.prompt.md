---
description: "Draft a high-level design (HLD) from an impact map and requirements — components, integrations, end-to-end flow, and architectural decisions"
agent: "agent"
argument-hint: "path to the impact map file (e.g. docs/impact-map.md) and optionally the requirements file"
---

You are helping draft a **high-level design (HLD)** for a web system.

**Primary input — impact map (read it in this workspace):**
`${{ input }}`

**Traceability — requirements (read it in this workspace if provided):**
`${{ requirements }}`

Save the output as **`high-level-design.md`** in the **same folder** as the impact map file.

Produce a single Markdown document with these sections:

1. **Major components and responsibilities** — For each component or service, state its main responsibility and what it does *not* own.

2. **Integrations and end-to-end flow** — Describe the main user journey (e.g. owner books an appointment) as a sequence across components; note synchronous vs asynchronous steps where it matters. Mention external integrations (email/SMS, identity, etc.) if applicable.

3. **Key architectural decisions** — Use the `/adr` skill to create all decisions in a single **`adrs.md`** file saved in the **same folder** as the impact map. In the HLD itself, include a summary table linking to the relevant section:

   | ADR | Decision | Status |
   |-----|----------|--------|
   | [ADR-001](./adrs.md#adr-001-short-title) | Short description | Accepted |

Stay aligned with the impact map; do not invent major scope beyond what the impact map and requirements support. Use the `/mermaid` skill to generate Mermaid diagrams that clarify component relationships and the end-to-end booking flow.
