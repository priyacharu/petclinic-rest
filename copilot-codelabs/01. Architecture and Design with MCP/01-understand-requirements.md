# Exercise 1: Understand Requirements with Copilot

## ⏱️ Time: 15 minutes

---

## 🎯 Goal

Use Copilot to analyze a requirements document and extract key information.

---

## 📖 Context

You have a requirements document ([requirements.md](./requirements.md)) that describes the appointment booking system. Now you need to **understand and summarize** it.

**What you'll do:**
1. Ask Copilot to read the requirements file from your workspace
2. Have Copilot extract key information
3. Get a structured summary
4. Validate that you understand the scope

---

## 📋 Tasks

### Task 0 (Optional): Create a Copilot Instruction File

Before asking Copilot questions, create a `.instructions.md` file to improve response quality.

Create `.copilot-instructions.md` in this folder:

```markdown
# Requirements Analysis Instructions

You are a requirements analyst. When reviewing requirements documents:

1. **Always extract these 6 elements**:
   - System Purpose (1-2 sentences)
   - User Actors (list of who)
   - Key Features (4-5 main capabilities)
   - Performance Requirements (times, scale, uptime)
   - Key Constraints (what we're NOT building)
   - Critical NFRs (must-haves)

2. **Format output as structured bullet points** (never prose)

3. **For ambiguous requirements**: Highlight unclear areas with [?]

4. **For risks**: Always mention technical risks after the main analysis

5. **For entities**: Draw ASCII entity relationship diagram
```

**Why create an instruction file?**
- Copilot learns your preferred style
- Consistent outputs across analyses
- MCP reads the file automatically
- Better formatting and structure

Now, when you ask Copilot questions, it will follow these instructions automatically! ✨

---

### Task 1: Ask Copilot to Analyze Requirements

**Open Copilot chat** (Cmd+I) and ask:

```
Using my .copilot-instructions.md guidelines:

Read the file requirements.md in this folder and provide:

1. **System Purpose**: What is the system trying to solve?
2. **User Actors**: Who are the users? (owner, vet, admin, etc.)
3. **Key Features**: List the main capabilities (4-5 bullets)
4. **Performance Requirements**: Response times, uptime, scale
5. **Key Constraints**: What are we NOT building? Limitations?
6. **Critical Non-Functional Requirements**: The "must-haves"

Format as structured bullet points for clarity.
```

**What to expect:**
Copilot will read requirements.md AND your instruction file, providing a structured summary that matches your preferences.

---

### Task 2: Identify Key Entities

Now ask Copilot:

```
Based on the requirements you just analyzed, identify:

1. **Core Entities**: The main data objects (Owner, Pet, Vet, Appointment, Slot, etc.)
2. **For each entity**: Key attributes (e.g., Appointment = {id, date, time, vet, owner})
3. **Relationships**: How entities connect (Owner has many Pets, Vet has many Appointments, etc.)

Draw a simple entity relationship diagram (text-based or ASCII art).
```

**What to expect:**
- A list of entities with attributes
- Text-based diagram showing relationships

---

### Task 3: Extract the Happy Path (Main Flow)

Ask Copilot:

```
Describe the happy path (main success scenario) for the system:

1. Start: What triggers the flow? (example: owner opens app)
2. Step-by-step: What happens in order?
3. End: What's the successful outcome?
4. Time constraint: How long should each step take?

Keep it to 10-12 steps maximum. Focus on booking flow.
```

**What to expect:**
- A numbered sequence of steps
- Clear start and end points
- Time targets for key steps

---

### Task 4: Identify Risks & Challenges

Ask Copilot:

```
What are the technical/operational risks or challenges mentioned in the requirements?

For each risk:
- **Risk**: What could go wrong?
- **Impact**: Why does it matter?
- **Example scenario**: When would this happen?

List at least 5.
```

**What to expect:**
- Double-booking issues
- Notification delivery failures
- Timezone confusion
- Concurrent request handling
- Data consistency

---

## 💡 Sample Copilot Conversation

Here's what a good interaction might look like:

**You ask:**
> Read requirements.md and summarize the system purpose, actors, and main features.

**Copilot responds:**
> **System Purpose**: Online appointment booking for pet owners
>
> **Actors**:
> - Pet Owner (books appointments)
> - Veterinarian (views schedule)
> - Clinic Staff (manages slots)
>
> **Main Features**:
> - Search for available appointment slots
> - Book an appointment (prevent double-booking)
> - Receive confirmation email/SMS
> - View daily schedule for vets

---

## 📊 Deliverables

Save your Copilot outputs to a file called `outputs/exercise-1-summary.md`:

```markdown
# Exercise 1: Requirements Summary

## System Overview
[Copy from Copilot response]

## Key Entities
[Copy from Copilot's entity analysis]

## Main Flow (Happy Path)
[Copy from Copilot's flow description]

## Technical Risks
[Copy from Copilot's risk analysis]

## Key Insights
[Your own observations - what surprised you? What's unclear?]
```

---

## 💡 Best Practices: Using Instructions for Better Outputs

**Instruction File Benefits:**
- ✅ Consistent formatting across all requests
- ✅ Copilot knows your preferences automatically
- ✅ MCP reads and applies instructions without you repeating them
- ✅ Easier to reuse for similar tasks later
- ✅ Team can follow same style if shared

**Pro Tip:** Create instructions for recurring tasks:
- Requirements analysis
- Architecture design
- API design
- Code review

---

## ✅ Success Criteria

Your exercise succeeds if you can answer:

- [ ] What is the system's main purpose?
- [ ] Who are the primary users?
- [ ] What are the 5 main features?
- [ ] What's the expected response time for searching slots?
- [ ] What's the uptime requirement?
- [ ] What's the main risk (double-booking)?
- [ ] Can you draw the entities and relationships?
- [ ] Did you create a .copilot-instructions.md file? (Optional but recommended)

---

## 🔍 Review Questions

After completing this exercise, ask yourself:

1. **Scope**: Am I building the right thing? (Check against requirements)
2. **Clarity**: Are there any ambiguous requirements?
3. **Completeness**: Did we miss any important flows?
4. **Feasibility**: Are the requirements achievable?

---

## 🎓 What You're Learning

✅ How to use MCP to feed files to Copilot
✅ How to structure analysis requests
✅ How to extract actionable insights from requirements
✅ How to validate requirements completeness

---

## 🚀 Next Step

Now that you understand the requirements, it's time to **design the architecture**.

→ [Exercise 2: Generate Architecture from Requirements](./02-design-architecture.md)
