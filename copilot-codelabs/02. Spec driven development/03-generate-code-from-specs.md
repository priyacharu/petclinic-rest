# Exercise 3: Generate Code from Specs

**⏱️ Time: 25 min** | **🎯 Goal: Use specs + tests to guide code generation**

---

## 📖 What You'll Do

You now have:
1. ✅ **API Spec** (OpenAPI) - "What the API looks like"
2. ✅ **Acceptance Tests** (Gherkin) - "How it should behave"

Now you'll:
- Generate code structure from the spec
- Map acceptance tests to code functions
- Create implementation guide that guarantees spec compliance

**Result:** Code that matches specs (no surprises!)

**💡 Pro Tip:** Your `.instructions.md` file will be crucial here to ensure generated code follows your project's coding standards, language conventions, and architectural patterns. Copilot will apply these standards automatically to all generated code.

---

## 📋 Tasks

### Task 1: Generate Code Structure from OpenAPI Spec

**📝 Using Instruction Files:** Reference your code generation standards from `.instructions.md`:
```markdown
## Code Generation Standards (C# / ASP.NET Core)
- Use PascalCase for class and method names
- Use dependency injection for all services
- All routes must be documented with XML comments
- Use async/await for I/O operations
- Return IActionResult for consistency
- Use FluentValidation for input validation
```

Ask Copilot:
```
Using the standards in .instructions.md, generate code structure for ASP.NET Core API:

Spec has these endpoints:
- GET /slots
- POST /appointments
- GET /appointments/{id}
- GET /vets/{id}/schedule

For each endpoint, generate:
1. Route definition (Express syntax)
2. Request validation (inputs from OpenAPI)
3. Response structure (matches OpenAPI response schema)
4. Error handling (matches OpenAPI error codes)

Don't write full implementation, just the skeleton structure.

Example format:
\`\`\`javascript
// GET /slots
router.get('/slots', (req, res) => {
  // Validation: date (required), vet_id (optional)
  // Response: 200 OK with slots array
  // Errors: 400 (invalid date), 404 (no slots)
});
\`\`\`

Output all 4 endpoints with structure.
```

**Expected output:** Code skeleton for all 4 endpoints

---

### Task 2: Generate Data Validation Code

**📝 Using Instruction Files:** Apply validation patterns from `.instructions.md`:
```markdown
## Validation Standards
- Use [Required], [StringLength], [RegularExpression] attributes
- Add custom validation attributes for business rules
- All validation errors should return 400 BadRequest
- Include normalized error messages for client debugging
```

Ask:
```
Generate validation functions for the API models:

Models (from OpenAPI schemas):
1. Slot - {id, date, time, vet_id, available, price}
2. Appointment - {id, slot_id, owner_id, pet_id, status, created_at}
3. Owner - {id, name, email, phone}
4. Vet - {id, name, specialty}

For each model, generate:
1. Type/interface definition
2. Validation function (check required fields, types, constraints)
3. Error messages (user-friendly)

Example (TypeScript):
\`\`\`typescript
interface Slot {
  id: string;
  date: string; // YYYY-MM-DD
  time: string; // HH:MM
  vet_id: string;
  available: boolean;
  price: number;
}

function validateSlot(slot: any): {valid: boolean, errors: string[]} {
  const errors = [];
  if (!slot.id) errors.push("Slot ID required");
  if (!slot.date || !/^\d{4}-\d{2}-\d{2}$/.test(slot.date))
    errors.push("Date must be YYYY-MM-DD");
  // ... more validations
  return {valid: errors.length === 0, errors};
}
\`\`\`

Generate for all 4 models.
```

**Expected output:** Validation functions for data models

---

### Task 3: Map Acceptance Tests to Code

**📝 Using Instruction Files:** Document test-to-code mapping standards:
```markdown
## Testing Standards
- Use xUnit for unit tests
- Use Moq for mocking dependencies
- Test naming: MethodName_Scenario_Expected
- Each test should validate one specific behavior
```

Ask:
```
Map acceptance test scenarios to code functions:

Test scenario: "Owner successfully books appointment"
  Given owner viewing available slots
  When clicks slot and confirms
  Then appointment status is "booked"

Code this requires:
1. GET /slots endpoint (list slots)
2. POST /appointments endpoint (create appointment)
3. Status field = "booked"
4. Response contains appointment ID

For each of these acceptance test scenarios, identify:
1. Which endpoints are called?
2. What data flows?
3. What validation happens?
4. What database changes?
5. What external services (email) are triggered?

Test scenarios:
1. Owner successfully books appointment
2. Prevent double-booking
3. Email confirmation sent
4. Appointment cancellation
5. Invalid date error handling

Format as:
Scenario: [Name]
  Endpoints: [GET/POST/etc used]
  Data flow: [Request → Processing → Response]
  Validations: [What gets checked]
  External calls: [Email, SMS, etc]
```

**Expected output:** Test-to-code mapping for 5+ scenarios

---

### Task 4: Generate Core Business Logic

**📝 Using Instruction Files:** Specify business logic patterns:
```markdown
## Business Logic Standards
- Each business operation should be in a separate service class
- Use repository pattern for data access
- All operations should be transactional when modifying data
- Log all business-critical operations for audit trail
- Return domain entities, not DTOs, from business layer
```

Ask:
```
Generate core business logic functions from acceptance tests:

From tests, these are required:

1. findAvailableSlots(date, vet_id?)
   - Input: date (string), optional vet_id
   - Logic: Query database, filter unavailable, return ordered by time
   - Validation: date must be today or future, format YYYY-MM-DD
   - Returns: List of Slot objects

2. bookAppointment(slot_id, owner_id, pet_id)
   - Input: slot_id, owner_id, pet_id
   - Logic:
     a. Check if slot is still available (prevent double-booking!)
     b. Lock slot (mark as booked)
     c. Create appointment record
     d. Send confirmation email (async)
   - Returns: Appointment object with status "booked"
   - Errors: 409 if slot already booked, 400 if invalid inputs

3. cancelAppointment(appointment_id)
   - Input: appointment_id
   - Logic:
     a. Get appointment
     b. Mark as "cancelled"
     c. Release slot back to available
   - Returns: Updated appointment

4. sendConfirmationEmail(appointment_id)
   - Input: appointment_id
   - Logic:
     a. Fetch appointment + owner details
     b. Format email body
     c. Send via email service
     d. Log send (for audit)
   - Errors: Retry 3x if email service down

For each function, generate:
- Function signature
- Input validation
- Business logic steps
- Database operations (pseudocode)
- Error handling
- Return value

Don't write full code, just pseudocode + comments.
```

**Expected output:** Business logic functions with pseudocode

---

### Task 5: Create Implementation Checklist

Ask:
```
Create a developer checklist to implement the appointment booking API:

The checklist should be organized by:
1. Database schema (tables, fields, relationships)
2. API endpoints (what to implement)
3. Validation (what to check)
4. Business logic (core functions)
5. External integrations (email service)
6. Testing (which scenarios to test)
7. Error handling (what can fail)

For each item, reference:
- The OpenAPI spec requirement
- The acceptance test that validates it
- The code function needed

Example:
[ ] Create "appointments" database table
    OpenAPI: Appointment schema requires {id, slot_id, owner_id, pet_id, status, created_at}
    Test: "Owner successfully books appointment" expects status="booked"
    Code: bookAppointment() function
[ ] Implement GET /slots endpoint
    OpenAPI: Takes date (required), vet_id (optional); returns List[Slot]
    Test: "Owner views slots for tomorrow" expects 3-5 slots
    Code: findAvailableSlots() + router.get('/slots')
...

Generate full checklist for all spec + test requirements.
```

**Expected output:** Implementation checklist linked to specs/tests

---

## 📊 Save: `outputs/exercise-3-code-generation.md`

Include:
- API code skeleton
- Data validation functions
- Test-to-code mapping
- Business logic pseudocode
- Implementation checklist

---

## 💼 Using Instruction Files for Code Generation

### Why Instruction Files Matter for Code Generation

When generating code from specs, instruction files ensure:

1. **Language Consistency**
   ```markdown
   # .instructions.md
   ## Language: C# (.NET 8)
   - Target framework: .NET 8.0
   - Use async/await for all I/O
   - Use LINQ for data queries
   - Use PascalCase for public members
   ```

2. **Framework Standards**
   ```markdown
   ## Framework: ASP.NET Core
   - Use endpoint routing (MapGet, MapPost)
   - Use middleware for cross-cutting concerns
   - Use configuration from appsettings.json
   - Use dependency injection via AddScoped/AddSingleton
   ```

3. **Architectural Patterns**
   ```markdown
   ## Architecture
   - Use repository pattern for data access
   - Use service layer for business logic
   - Use DTOs for API request/response
   - Use domain events for cross-service communication
   ```

4. **Error Handling**
   ```markdown
   ## Error Handling
   - All validation errors return 400 BadRequest
   - All not-found errors return 404 NotFound
   - All conflict errors return 409 Conflict
   - Include error code and user-friendly message
   ```

5. **Testing Patterns**
   ```markdown
   ## Testing
   - Use xUnit test framework
   - Use Moq for mocking
   - Test naming: [MethodName]_[Scenario]_[Expected]
   - Aim for 80%+ code coverage
   ```

### Workflow: Using Instructions for Code Gen

1. **Finalize your `.instructions.md` before code generation**
   - Include language, framework, architectural choices
   - Add code style preferences
   - Document business logic standards

2. **Reference instructions in prompts**
   ```
   "Using the standards in .instructions.md, generate code for:
   1. POST /appointments endpoint
   2. AppointmentService business logic
   3. AppointmentValidator data validation"
   ```

3. **Copilot applies standards automatically**
   - All generated code will follow your conventions
   - Naming, architecture, error handling will be consistent
   - Less cleanup and refactoring needed

4. **Update instructions collaboratively**
   - If team discovers better patterns, update `.instructions.md`
   - All future code generation will adopt new patterns

---

## 💡 Spec-Driven Code Benefits

✅ **No surprises** - Code matches spec by design
✅ **Traceable** - Every acceptance test maps to code
✅ **Complete** - Checklist ensures nothing is missed
✅ **Consistent** - Spec enforces naming, structure, behavior
✅ **Testable** - Tests validate code matches spec
✅ **Documented** - Spec is living documentation

---

## 🔄 The Full Workflow

```
Requirements
    ↓
OpenAPI Spec (what API does)
    ↓
Acceptance Tests (how API should behave)
    ↓
Code Structure (generated from spec)
    ↓
Implementation (developers code to match spec)
    ↓
Test Execution (tests validate spec compliance)
↓
Spec ← Code stays aligned!
```

---

## ✅ Success Criteria

- [ ] Code skeleton generated for all 4 endpoints
- [ ] Validation functions for all 4 data models
- [ ] Test scenarios mapped to code functions
- [ ] Business logic functions defined (5+ functions)
- [ ] Implementation checklist created
- [ ] Every checklist item links to spec + test
- [ ] (Bonus) `.instructions.md` includes language/framework/architecture standards
- [ ] (Bonus) Referenced `.instructions.md` in all code generation prompts
- [ ] (Bonus) All generated code follows instruction file standards

---

## 🎓 What You've Learned

✅ How to write API specifications (Exercise 1)
✅ How to write acceptance tests (Exercise 2)
✅ How to generate code from specs (Exercise 3)
✅ How specs + tests + code stay aligned
✅ Spec-driven development reduces bugs & surprises

---

## 🎉 Course Complete!

| Exercise | What | Output |
|----------|------|--------|
| 0 | Concepts | Understanding |
| 1 | Specs | api-specs.yaml + Swagger UI |
| 2 | Tests | acceptance-tests.feature (12+ scenarios) |
| 3 | Code | Code skeleton + checklist |

**Total time:** ~70 minutes

**You now know:**
- ✅ How to write machine-readable API specs
- ✅ How to write executable acceptance tests
- ✅ How to generate code that matches specs
- ✅ How specs + tests prevent bugs
- ✅ Why specification-first beats code-first

**Next:** Apply this to your own project!
