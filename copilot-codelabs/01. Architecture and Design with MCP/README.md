# Architecture & Design with Copilot - Training Course

## 📚 Overview

This course teaches how to use **GitHub Copilot for architecture and system design**. Through 4 practical exercises, you'll learn:

1. **How to use Copilot to analyze and understand requirements**
2. **How to generate system architecture from user stories**
3. **How to make technology choices based on design constraints**
4. **How to integrate real data sources (like JIRA) using MCP**

## 🎯 Use Case: Pet Appointment Booking System

We'll use a **simple, focused use case** throughout all exercises:

> **Problem**: PetClinic needs an online system where pet owners can book appointments with vets.
> 
> **Scope**: Just the appointment booking flow, nothing complex
> - Owner searches for available slots
> - Owner books an appointment
> - System confirms the appointment

This is intentionally simple so you can focus on **the process** (how to use Copilot + MCP), not on complex business logic.

---

## 📖 Exercises

### [Exercise 0: Setup & Context](./00-setup.md)
**Time: 5 minutes**
- Understand the use case
- Prepare sample requirements document
- Set up your workspace

### [Exercise 1: Understand Requirements](./01-understand-requirements.md)
**Time: 15 minutes**
- Analyze a local requirements document
- Use Copilot to:
  - Extract key requirements
  - Identify actors and systems
  - Get a structured summary
- **Deliverable**: Summary of requirements in structured format

### [Exercise 2: Generate Architecture from Requirements](./02-design-architecture.md)
**Time: 20 minutes**
- Use Copilot to generate system architecture
- Create architecture diagram
- Identify key services and data flows
- **Deliverable**: Architecture diagram (Mermaid/ASCII)

### [Exercise 3: Technology Decisions Based on Architecture](./03-technology-selection.md)
**Time: 15 minutes**
- Use Copilot to evaluate technology options
- Make decisions on: database, API, deployment
- Document decisions with justification
- **Deliverable**: Technology selection document

### [Exercise 4: Read Requirements from JIRA using MCP](./04-read-from-jira-with-mcp.md)
**Time: 25 minutes**
- Set up JIRA MCP integration with Copilot
- Read real requirements directly from JIRA issues
- Generate architecture based on JIRA data
- **Deliverable**: Architecture designed from live JIRA requirements

---

## 🎓 Learning Goals

By completing this course, you'll understand:

✅ How to use Copilot for requirements analysis
✅ How to generate architecture diagrams from text
✅ How to make tech stack decisions systematically
✅ **How to create instruction files for consistent outputs** ⭐
✅ The workflow: Requirements → Architecture → Technology
✅ How to integrate real data sources (JIRA) using MCP

---

## 🔄 Course Structure Changes

**Note**: Exercises 1-3 use **local files and instruction files** to teach the fundamentals. Exercise 4 introduces **real MCP integration with JIRA** to show how these techniques apply to live data.

- **Exercises 1-3**: Local analysis + Instruction files for consistent outputs
- **Exercise 4**: Cloud integration via JIRA MCP + Live data sources

---

## 🎯 Key Innovation: Copilot Instruction Files & MCP

**Part 1: Instruction Files (Exercises 1-3)**  
This course introduces a powerful technique: **Creating instruction files to guide Copilot**.

Instead of repeating yourself, you create `.instructions.md` files that Copilot uses automatically:

- **Exercise 1**: Create `.copilot-instructions.md` for requirements analysis
- **Exercise 2**: Create `.architecture-instructions.md` for architecture design  
- **Exercise 3**: Create `.tech-selection-instructions.md` for technology decisions

**Part 2: Real MCP Integration (Exercise 4)**  
Once you understand instruction files, Exercise 4 shows how to use **Model Context Protocol (MCP)** to connect real data sources like JIRA:

- **Exercise 4**: Connect JIRA via MCP to read live requirements
- Generate architecture based on real JIRA issues
- Maintain traceability from requirements → architecture → code

**Benefits:**
- ✅ Consistent outputs every time
- ✅ Your preferences are remembered
- ✅ Team can reuse your instructions
- ✅ Works with external systems (JIRA, Confluence, etc.)
- ✅ Better formatting, structure, completeness

---

## 🚀 Getting Started

1. **[Recommended] Read the guide** → [How Instruction Files Work](./INSTRUCTION-FILES-GUIDE.md) ⭐
2. Start with [Exercise 0: Setup](./00-setup.md)
3. Follow exercises in order (1, 2, 3)
4. Each exercise builds on the previous one
5. Total time: ~55 minutes

---

## 📖 Course Files

- **README.md** ← you're here
- [INSTRUCTION-FILES-GUIDE.md](./INSTRUCTION-FILES-GUIDE.md) - **How to use Copilot instruction files** ⭐
- [requirements.md](./requirements.md) - Sample requirements document
- [00-setup.md](./00-setup.md) - Preparation (5 min)
- [01-understand-requirements.md](./01-understand-requirements.md) - Exercise 1 (15 min) 
- [02-design-architecture.md](./02-design-architecture.md) - Exercise 2 (20 min)
- [03-technology-selection.md](./03-technology-selection.md) - Exercise 3 (15 min)

---

## 💡 Key Principle

Each exercise demonstrates:
- **What you ask Copilot** (the prompt)
- **What you should get** (expected output)
- **How to create instruction files** (to automate your preferences)
- **How to validate** (what makes it good)

The use case is simple intentionally—focus on the **process**, not the complexity!

---

**Ready?** Start with [Exercise 0: Setup & Context](./00-setup.md) →
