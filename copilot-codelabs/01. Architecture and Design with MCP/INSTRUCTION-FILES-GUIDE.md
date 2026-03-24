# Copilot Instruction Files: A Quick Guide

## 🎯 What Are Instruction Files?

Instruction files (`.instructions.md` or `.copilot-instructions.md`) are special files that tell Copilot how you want it to behave.

Instead of repeating instructions every time, you write them once and Copilot uses them automatically via MCP.

---

## 📝 Three Examples from This Course

### Example 1: Requirements Analysis Instructions

**File**: `.copilot-instructions.md`

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
```

**Result:** Every time you ask Copilot to analyze requirements, it automatically provides all 6 elements in bullet point format.

---

### Example 2: Architecture Design Instructions

**File**: `.architecture-instructions.md`

```markdown
# Architecture Design Instructions

You are a software architect. When designing architectures:

1. **Always include these components**:
   - Frontend
   - API Layer
   - Business Logic
   - Data Layer
   - External Systems

2. **Always provide**:
   - Text-based component list
   - Mermaid diagram (visual)
   - Data flow description
   - List of all external systems
```

**Result:** Copilot always produces complete, consistent architecture documentation.

---

### Example 3: Technology Selection Instructions

**File**: `.tech-selection-instructions.md`

```markdown
# Technology Selection Instructions

You are a technology architect evaluating tools.

1. **For each technology comparison, always provide**:
   - Pros/cons
   - Cost comparison
   - Team expertise consideration
   - Justification

2. **Evaluation criteria to consider**:
   - Cost
   - Performance
   - Learning curve
   - Community support
   - Scalability
```

**Result:** Technology evaluations are always fair, complete, and justified.

---

## 🚀 How to Use Instruction Files

### Step 1: Create the File

Create `.copilot-instructions.md` in your project folder:

```bash
touch .copilot-instructions.md
```

### Step 2: Write Your Instructions

Describe how you want Copilot to behave:

```markdown
# My Copilot Instructions

You are a [ROLE]. When [TASK]:

1. Always include...
2. Format as...
3. Never do...
```

### Step 3: Copilot Automatically Uses It

Just ask normally! MCP will read the instruction file:

```
Analyze the requirements in requirements.md
```

Copilot will follow your instruction file automatically! ✨

---

## 💡 Best Practices

### ✅ DO:
- Write clear, specific instructions
- Include 3-5 key rules per instruction file
- Focus on format and completeness
- Include examples if possible
- Create one file per task type

### ❌ DON'T:
- Make instructions too long (keep to 1 page)
- Include contradictory rules
- Use vague language ("good" → specify what "good" means)
- Create too many instruction files (organize related tasks)

---

## 🎯 When to Create Instruction Files

Create instruction files for **recurring tasks**:

| Task | Instruction File |
|------|-----------------|
| Requirements analysis | `.requirements-instructions.md` |
| Architecture design | `.architecture-instructions.md` |
| API design | `.api-instructions.md` |
| Code review | `.code-review-instructions.md` |
| Testing | `.testing-instructions.md` |
| Documentation | `.documentation-instructions.md` |

---

## 🔄 Example Workflow

### Without Instruction Files:
```
You: "Analyze requirements... provide 6 elements... format as bullets"
Copilot: [Response in prose]
You: "That's nice but I wanted bullets, not prose"
Copilot: "Let me redo it..."
[Wasted time]
```

### With Instruction Files:
```
[Create .copilot-instructions.md once with your preferences]

You: "Analyze requirements.md"
Copilot: [Automatically follows instruction file, provides bullets]
[Done instantly, correctly formatted]
```

---

## 🎓 Key Insight

**Instruction files are like training Copilot once, then reusing that training forever.**

Instead of:
- Repeating yourself to Copilot
- Getting inconsistent outputs
- Having to massage/reformat responses

You get:
- Consistent, predictable outputs
- Correct format every time
- Time savings across your entire team

---

## 📚 Further Reading

- [Copilot Documentation](https://github.com/features/copilot)
- [MCP: Model Context Protocol](https://modelcontextprotocol.io/)
- [System Prompting Best Practices](https://www.anthropic.com/research)

---

## 🎯 In This Course

We recommend creating three instruction files:

| Exercise | Instruction File | Download |
|----------|-----------------|----------|
| 1 | `.copilot-instructions.md` | See Exercise 1 |
| 2 | `.architecture-instructions.md` | See Exercise 2 |
| 3 | `.tech-selection-instructions.md` | See Exercise 3 |

Each exercise will show you the exact file to create!

---

**Ready?** Go back to your exercise and create your first instruction file! 🚀
