# Spec-Driven Development with Copilot

**Learn how to write detailed specifications FIRST, then use them to guide implementation.**

---

## 📚 What is Spec-Driven Development?

**Traditional**: Write code → Test it → Fix bugs

**Spec-Driven**: Write specs → Write code to match specs → Validate match

**Why?** Specs serve as:
- 📋 Living documentation
- ✅ Acceptance test source
- 🎯 Code generation guide
- 🔄 Dev/QA alignment
- 📝 API contract enforcement

---

## 🎯 Use Case: Pet Appointment Booking

Same simplified system from Course 01:
- Owner searches and books appointments
- Vet views schedule
- Email/SMS confirmations
- Prevent double-booking

**Why start here?** You already understand the requirements from Course 01!

---

## 📖 Exercises

### [Exercise 0: Setup & Concepts](./00-setup.md)
**Time: 5 minutes** - Understand spec-driven approach

### [Exercise 1: Write API Specifications](./01-write-api-specs.md)
**Time: 20 minutes** - Create OpenAPI/Swagger specs from requirements

### [Exercise 2: Define Error Handling & Validation](./02-define-error-handling-validation.md)
**Time: 20 minutes** - Specify error codes, validation rules, edge cases

### [Exercise 3: Generate Code from Specs](./03-generate-code-from-specs.md)
**Time: 25 minutes** - Use specs to guide or generate implementation

---

## 🎓 Learning Goals

✅ How to write API specifications (OpenAPI/Swagger)
✅ How to specify error codes, validations, and edge cases
✅ How to ensure data integrity through field validation
✅ How to handle failures gracefully (retries, idempotency)
✅ How Copilot can generate code from specs
✅ How to use instruction files for consistent spec & code generation

---

## 💡 Key Principles

**Specs → Tests → Code**

Not: Code → Tests → Hope it works

**Instruction Files → Consistent Specs & Code**

Use `.instructions.md` to define standards that Copilot applies across all exercises:
- API naming conventions (Exercise 1)
- Validation patterns (Exercise 2)
- Code generation standards (Exercise 3)

---

## ⏱️ Total Time: ~70 minutes

| Exercise | Time |
|----------|------|
| Setup | 5 min |
| API Specs | 20 min |
| Error Handling & Validation | 20 min |
| Generate Code | 25 min |

---

## 📚 What Gets Generated

| Exercise | Deliverable |
|----------|-------------|
| 1 | `api-specs.yaml` - OpenAPI spec + Swagger UI |
| 2 | `error-handling-validation.md` - Errors, validations, edge cases |
| 3 | `code-generation.md` - Code skeleton + implementation checklist |

---

## 🚀 Prerequisites

- Completed [Course 01: Architecture & Design](../01.%20Architecture%20and%20Design%20with%20MCP/) (recommended but not required)
- Understand pet appointment booking requirements
- Copilot available in VS Code
- 70 minutes of focused time

---

## 📁 Outputs

Save outputs to `outputs/`:
```
outputs/
├── exercise-1-api-specs.yaml (OpenAPI)
├── exercise-2-error-handling-validation.md (Errors + Validation)
└── exercise-3-code-generation.md (Code structure)
```

---

## 🔄 How This Relates to Course 01

| Course | Focus | Output |
|--------|-------|--------|
| 01: Architecture | **Design & structure** | Architecture diagrams |
| 02: Spec-Driven Dev | **Detailed contracts** | API specs + tests |
| Combine | Architecture drives specs | Aligned design → code |

---

**Ready?** → [Start with Exercise 0](./00-setup.md)
