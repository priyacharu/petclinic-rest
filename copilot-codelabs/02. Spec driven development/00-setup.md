# Exercise 0: Setup & Spec-Driven Concepts

**⏱️ Time: 5 min** | **🎯 Goal: Understand the spec-first approach**

---

## 📖 What is Spec-Driven Development?

**The Spec-Driven Workflow:**

```
1. Write detailed specs (OpenAPI, validation rules, error codes)
   ↓
2. Define error handling & validation requirements
   ↓
3. Generate code from specs
   ↓
4. Validate code matches specs
```

**Why?**
- Specs = Contract (developer, QA, PM all agree on behavior)
- Error specs = Validation (what can fail, how to handle it)
- Code = Implementation (follows the plan)
- No surprises! Requirements → Specs → Error Handling → Code

---

## 🎯 This Course Uses Pet Appointment Booking

**Recap from Course 01:**

| Story | What It Does |
|-------|-------------|
| Story 1 | Owner searches available slots |
| Story 2 | Owner books appointment |
| Story 3 | Confirmation sent (email/SMS) |

See [../01. Architecture and Design with MCP/requirements.md](../01.%20Architecture%20and%20Design%20with%20MCP/requirements.md) for full details.

---

## 📋 What We'll Create

### Exercise 1: API Specification
**Goal:** Write detailed OpenAPI spec for all endpoints
```yaml
GET /slots - List available slots
POST /appointments - Book appointment
GET /appointments/{id} - Get appointment details
```

**Output:** `api-spec.yaml` (machine-readable contract)

---

### Exercise 2: Define Error Handling & Validation
**Goal:** Specify error codes, validation rules, and edge cases
```markdown
# Error Handling & Validation Specification

## Validation Rules
- Field constraints (format, length, range)
- Required vs optional fields
- Valid/invalid examples

## Error Codes
- HTTP status codes
- Error messages (user-facing & developer)
- Recovery hints

## Edge Cases
- Boundary conditions
- Concurrent operations
- Race conditions

## Idempotency & Retries
- Which operations are idempotent?
- Retry logic for failures
```

**Output:** `error-handling-validation.md` (error specs + validation rules)

---

### Exercise 3: Generate Code from Specs
**Goal:** Use specs to guide code generation
```swift
Generate POST /appointments handler
  - Takes: slot_id, owner_id
  - Returns: appointment confirmation
  - Side effect: Sends email
```

**Output:** Generated code structure (or implementation guide)

---

## 💡 Key Concepts

### 1. OpenAPI (Swagger)
- Industry standard for REST API specs
- Machine-readable (can generate code from it!)
- Documents: endpoints, parameters, responses, errors
- Example: [openapis.org](https://www.openapis.org/)

### 2. Error Handling & Validation Specification
- Define all error codes and HTTP status codes
- Specify validation rules for each field
- Document edge cases and boundary conditions
- Define idempotency and retry logic
- Ensures consistent error messages and robust input validation
- Example:
  ```markdown
  Field: email
    Required: Yes
    Format: RFC 5322
    Error: "Please provide a valid email address"
  ```

### 3. Code Generation from Specs
- Use specs to guide code implementation
- Map error specs to error handling code
- Apply validation rules to code validation layers
- All generated code follows the specs

### 4. Instruction Files
- Special files (`.instructions.md`, `.prompt.md`) that define project standards
- Guide Copilot to generate consistent code/specs throughout
- Examples: coding conventions, validation rules, error handling patterns
- Applied automatically to all Copilot requests in the workspace
- **You can use instruction files in ANY exercise where consistency matters!**

---

## 💡 Using Instruction Files Throughout This Course

You can create an `.instructions.md` file in your workspace to guide Copilot across all three exercises:

**Exercise 1 (Specs):**
```markdown
## API Naming Conventions
- Use kebab-case for endpoints: /pet-appointments, /vet-schedules
- Use snake_case for JSON fields: pet_id, created_at
- Always include timestamps: created_at, updated_at
```

**Exercise 2 (Validation):**
```markdown
## Validation Standards
- Email: RFC 5322 format
- Dates: Always ISO 8601
- Phone: 10+ digits with optional +1 prefix
- Currency: Exactly 2 decimal places, no negatives
```

**Exercise 3 (Code Generation):**
```markdown
## Code Generation Standards
- Language: C# (.NET 8)
- Architecture: Repository pattern + Service layer
- Use dependency injection for all services
- Use async/await for I/O operations
```

**Benefit:** Same instruction file works across all exercises, ensuring consistency!

---

## ✅ Checklist Before Starting

- [ ] You've read requirements.md from Course 01
- [ ] You understand the 3 user stories (slots, booking, confirmation)
- [ ] Copilot is open (Cmd+I)
- [ ] You have ~70 minutes for all exercises
- [ ] Create `outputs/` folder for saving work
- [ ] (Optional) Create `.instructions.md` in workspace root for consistent code/spec generation

---

## 🔄 Connection to Course 01

**Course 01 (Architecture)** gave you:
- System architecture (Frontend, API, DB, Services)
- Component diagram
- Technology choices

**Course 02 (Spec-Driven Dev)** adds:
- Detailed API contracts (every endpoint, every field)
- Error handling & validation specifications
- Code generation guidance (from specs)

**Together:** Architecture describes *structure*, Specs describe *behavior*

---

## 🚀 Next Step

Ready to spec the API? → [Exercise 1: Write API Specs](./01-write-api-specs.md)
