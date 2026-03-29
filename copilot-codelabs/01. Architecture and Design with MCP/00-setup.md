# Exercise 0: Setup & Context

## ⏱️ Time: 5 minutes

---

## 🎯 Goal

Understand the use case and prepare for exercises 1-3.

---

## 📖 The Use Case: Pet Appointment Booking

You're building a **simple online appointment booking system** for PetClinic.

**What customers want:**
- Pet owners can see available appointment times online
- Pet owners can book an appointment
- Owners get a confirmation email/SMS
- Vets can see their daily schedule

**What we're NOT building:**
- Multi-clinic support
- Mobile app
- Payment processing
- Surgery scheduling
- Telemedicine

**Why keep it simple?**
This course is about **the process of using Copilot for architecture design**, not about complex business logic. A simple use case lets you focus on *how to ask Copilot the right questions*.

---

## 📄 Requirements Document

In this folder, you'll find: [requirements.md](./requirements.md)

It contains:
- **4 user stories** (owner books, vet sees schedule, etc.)
- **Non-functional requirements** (performance, reliability, scale)
- **Constraints** (single clinic, web only, working hours)
- **Dependencies** (database, email, SMS)

This is the document you'll analyze in Exercise 1.

---

## 🔧 Setup Your Environment

### Step 1: Open Copilot in VS Code
- Open VS Code
- Open your copilot chat (Cmd+I on Mac)
- This is where you'll ask Copilot questions

### Step 2: Reference Files in Your Prompts
When asking Copilot questions, you can reference files from your workspace:

> "Analyze the requirements in requirements.md and summarize..."

Copilot will read that file and analyze it.

### Step 3: Prepare Your Workspace

Create a folder to save outputs from each exercise:

```
01. Architecture and Design with MCP/
├── requirements.md (↑ you already have this)
├── 00-setup.md (← you're reading this)
├── 01-understand-requirements.md (← next)
├── 02-design-architecture.md
├── 03-technology-selection.md
├── outputs/
│   ├── exercise-1-summary.md
│   ├── exercise-2-architecture.md
│   └── exercise-3-tech-decisions.md
```

---

## ✅ Checklist Before Starting

- [ ] You've read [requirements.md](./requirements.md)
- [ ] You understand we're building a **simple appointment booking system**
- [ ] You know the main features: search slots, book, confirm, view schedule
- [ ] Copilot chat is open and ready
- [ ] [Optional] You've read [INSTRUCTION-FILES-GUIDE.md](./INSTRUCTION-FILES-GUIDE.md) to learn about instruction files
- [ ] You're prepared to follow Exercise 1

---

## 💡 Pro Tip: Instruction Files

This course teaches a powerful technique: **Creating instruction files that guide Copilot's analysis**.

Instead of:
```
Every time: "Please analyze requirements as bullet points..."
```

You create:
```
.copilot-instructions.md (once)
---
Then Copilot always follows your preferences automatically!
```

Each exercise will guide you through creating instruction files for that task.

Read [INSTRUCTION-FILES-GUIDE.md](./INSTRUCTION-FILES-GUIDE.md) if you want to understand this concept before starting! ⭐

---

## 🚀 What's Next?

Exercise 1 teaches you how to:
1. **Feed requirements to Copilot** (using MCP to read files)
2. **Ask Copilot to summarize** the requirements
3. **Extract key information** (actors, features, constraints)

Let's go! → [Exercise 1: Understand Requirements with MCP](./01-understand-requirements.md)
