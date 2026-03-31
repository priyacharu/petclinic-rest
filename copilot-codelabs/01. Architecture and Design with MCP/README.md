# Architecture and Design with MCP — Simplified lab

Use **GitHub Copilot** (and MCP when relevant) to practice architecture thinking on the **Pet Appointment Booking** use case.

## How to run this lab (choose one)

1. **Guided path** — Open [01-build-impact-map.md](./01-build-impact-map.md), then [02-draft-high-level-design.md](./02-draft-high-level-design.md). Each file walks through goals, inputs, and deliverables.
2. **Direct prompts** — Open the [resource prompts](./resources/), copy **everything below the horizontal rule (`---`)**, paste into **Copilot Chat** in one go. You can **attach** input files with `@` so Copilot reads them from the workspace.
3. **Repository prompt commands (recommended for a tidy `docs/` layout)** — In Copilot Chat, run the slash commands defined in [`.github/prompts/`](../../.github/prompts/):
   - **`/create-impact-map`** — Pass the path to your requirements file, e.g. **`docs/requirements.md`**. Copilot creates a **subfolder under `docs/`** (name derived from the document title, e.g. `docs/appointment-booking-system/`) and saves **`impact-map.md`** there. All later artifacts for that topic live in that same folder.
   - **`/create-high-level-design`** — Pass the path to **`impact-map.md`** from step 1 (e.g. **`docs/appointment-booking-system/impact-map.md`**). Add the requirements path if the command asks for a second argument (e.g. **`docs/requirements.md`**). Copilot saves **`high-level-design.md`** in **the same folder as the impact map** (still under `docs/`). The [bundled prompt](../../.github/prompts/create-high-level-design.prompt.md) may also create **`adrs.md`** and reference skills like **`/adr`** and **`/mermaid`** in that folder.

Paths **1** and **2** often save `impact-map.md` / `high-level-design.md` next to this README unless you choose otherwise. Path **3** keeps outputs under **`docs/<topic>/`** by design.

## Flow

| Step | Input | Output (typical) |
|------|--------|------------------|
| **1 — Impact map** | `docs/requirements.md` (or [`requirements.md`](./requirements.md) in this lab, if you use it) | **`docs/<topic>/impact-map.md`** when using `/create-impact-map`; otherwise **`impact-map.md`** where you save it |
| **2 — HLD** | That **`impact-map.md`** (+ optional requirements) | **`docs/<topic>/high-level-design.md`** (same folder as the impact map when using `/create-high-level-design`); may include **`adrs.md`** |

Traceability stays linear: **requirements → impact map → high-level design** (all under `docs/<topic>/` if you use the slash commands).

## Exercises and prompts

| Step | Guide | Resource prompt | Repo command |
|------|--------|-----------------|--------------|
| 1 | [01-build-impact-map.md](./01-build-impact-map.md) | [resources/prompt-impact-map.md](./resources/prompt-impact-map.md) | **`/create-impact-map`** |
| 2 | [02-draft-high-level-design.md](./02-draft-high-level-design.md) | [resources/prompt-high-level-design.md](./resources/prompt-high-level-design.md) | **`/create-high-level-design`** |

Definitions for the commands live under **[`.github/prompts/`](../../.github/prompts/)**.

## Optional: repository skills (`.github/skills/`)

This repo includes **Copilot-ready skills** under **[`.github/skills/`](../../.github/skills/)**—small playbooks the assistant can follow for consistent outputs:

- **`adr`** — structure for Architecture Decision Records (the **`/create-high-level-design`** flow may invoke this to build **`adrs.md`**).
- **`c4-diagrams`** — C4 model diagrams (Context / Container / Component) from your design text.
- **`mermaid`** — Mermaid diagrams for flows and component relationships.

The simplified lab **does not require** opening those files manually if you use **`/create-high-level-design`**, which already points Copilot at the right skills.

## Full course (optional)

The original multi-step course (setup, requirements deep-dive, architecture, tech selection, JIRA MCP) lives under **[`extra/`](./extra/)** — start from [`extra/README.md`](./extra/README.md).

## Files in this folder

- **`requirements.md`** — sample requirements (if present here); classes often use **`docs/requirements.md`** with **`/create-impact-map`**.  
- **`01-build-impact-map.md`**, **`02-draft-high-level-design.md`** — exercise instructions  
- **`resources/`** — plain-text prompts you can paste into chat  
- **`extra/`** — extended exercises and guides  
- **`docs/<topic>/`** (repo root) — when using slash commands, your **impact map**, **HLD**, and related files are written here (see `.gitignore`: usually only **`docs/requirements.md`** is committed unless your instructor says otherwise).
