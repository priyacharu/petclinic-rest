# Exercise 1: Build the impact map

## Goal

Produce an **impact map** from the product requirements so you can reason about scope, risk, and dependencies before drafting a high-level design.

## Choose how to work

- **Copilot prompt command:** Run **`/create-impact-map`** in Copilot Chat and provide the path to your requirements file, e.g. **`docs/requirements.md`**. The prompt in [`.github/prompts/create-impact-map.prompt.md`](../../.github/prompts/create-impact-map.prompt.md) tells Copilot to create a folder **under `docs/`** (kebab-case name from the document title, e.g. `docs/appointment-booking-system/`) and save **`impact-map.md`** inside it. Use that path as the input for Exercise 2.
- **Step-by-step:** Read the sections below and use Copilot in your own words, or  
- **Plain prompt:** Open **[`resources/prompt-impact-map.md`](./resources/prompt-impact-map.md)**, copy everything **below** the `---` line, paste into Copilot Chat, optionally attach requirements with `@`, then save the answer yourself (e.g. under **`docs/<topic>/impact-map.md`** to match the layout above).

## Context

The lab uses the **PetClinic appointment booking** scenario. Prefer **`docs/requirements.md`** as the source of truth when using **`/create-impact-map`**. If you only have **`requirements.md`** in this lab folder, use that path instead.

## Input

- **Requirements** — read the file end-to-end before you start (`docs/requirements.md` or this folder, per your instructor).

## Activity guidelines

Whether you use **`/create-impact-map`**, **[`resources/prompt-impact-map.md`](./resources/prompt-impact-map.md)**, or your own wording, cover:

1. **Requirements and acceptance criteria** — Summarize what must be true for the booking flow; call out ambiguous or missing assumptions.
2. **Affected modules, dependencies, and risks** — What parts of a system would change or integrate (e.g. scheduling, notifications, identity)? What could block or slow delivery?
3. **Assumptions and scope boundaries** — What are you explicitly assuming? What is **out of scope** for this design pass?

## Deliverable

1. Complete the analysis with Copilot (iterate until it is concrete enough to hand to a teammate).
2. **Save or keep** the artifact as **`impact-map.md`**. With **`/create-impact-map`**, it already lands under **`docs/<topic>/impact-map.md`**. With the manual prompts, save it wherever your instructor asked (often the same **`docs/<topic>/`** pattern).
3. Use that **`impact-map.md`** as the main input for **[Exercise 2](./02-draft-high-level-design.md)**.

## Done?

Continue to **[Exercise 2: Draft the HLD](./02-draft-high-level-design.md)** after `impact-map.md` exists.
