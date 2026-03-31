# Exercise 2: Draft the high-level design (HLD)

## Goal

Turn the impact map into a **high-level design**: major components, integrations, end-to-end flow, and explicit architectural decisions.

## Choose how to work

- **Copilot prompt command:** Run **`/create-high-level-design`** in Copilot Chat. Pass the path to **`impact-map.md`** from Exercise 1 (e.g. **`docs/appointment-booking-system/impact-map.md`**). If the UI offers a second input, pass **`docs/requirements.md`** for traceability. Copilot writes **`high-level-design.md`** in **the same folder as the impact map** (still under **`docs/`**). The [bundled prompt](../../.github/prompts/create-high-level-design.prompt.md) can also produce **`adrs.md`** and use skills such as **`/adr`** and **`/mermaid`** in that folder.
- **Step-by-step:** Read the sections below and use Copilot in your own words, or  
- **Plain prompt:** Open **[`resources/prompt-high-level-design.md`](./resources/prompt-high-level-design.md)**, copy everything **below** the `---` line, attach **`impact-map.md`** / **`requirements.md`** with `@` if you like, then save the result (ideally next to the impact map under **`docs/<topic>/`**).

## Context

Same PetClinic booking scenario as in your requirements document. Your impact analysis should already be captured in **`impact-map.md`** (typically **`docs/<topic>/impact-map.md`** if you used **`/create-impact-map`**).

## Input

- **`impact-map.md`** — **required** (output from [Exercise 1](./01-build-impact-map.md)).
- **`requirements.md`** / **`docs/requirements.md`** — optional, for traceability (recommended for **`/create-high-level-design`** when a second argument is available).

## Activity guidelines

Whether you use **`/create-high-level-design`**, **[`resources/prompt-high-level-design.md`](./resources/prompt-high-level-design.md)**, or adapt it, produce:

1. **Major components and responsibilities** — Name each logical part of the system and what it owns.
2. **Integrations and end-to-end flow** — How does an owner booking journey move through those components? What external systems or teams are involved?
3. **Key architectural decisions** — Document important decisions (rationale / trade-offs). The repo prompt may fold these into **`adrs.md`** and summarize them in the HLD.

## Deliverable

1. Refine the HLD with Copilot until it is consistent with `impact-map.md` and the requirements.
2. **Save or keep** **`high-level-design.md`** next to **`impact-map.md`** (e.g. under **`docs/<topic>/`**).

## Optional follow-up

For C4 diagrams or extra polish, see **[`.github/skills/`](../../.github/skills/)** (also described in the [lab README](./README.md)).

## Done?

You have a traceable chain: **requirements → impact map → HLD** under **`docs/`** when using the slash commands. For more exercises (instruction files, tech selection, JIRA MCP), see **[`extra/README.md`](./extra/README.md)**.
