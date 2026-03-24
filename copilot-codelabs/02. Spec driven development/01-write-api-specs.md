# Exercise 1: Write API Specifications

**⏱️ Time: 20 min** | **🎯 Goal: Create OpenAPI spec for appointment booking API**

---

## 📖 What You'll Create

An **OpenAPI 3.0 specification** (machine-readable contract) that documents:
- All API endpoints (GET, POST, PUT, DELETE)
- Request/response formats (what fields? what types?)
- Error codes (400, 404, 500, etc.)
- Authentication & headers
- Examples

This spec can be:
- ✅ Converted to interactive Swagger UI
- ✅ Used to generate server code (Stub)
- ✅ Used to generate client code (SDK)
- ✅ Shared with frontend team for integration

**💡 Pro Tip:** You can create a `.instructions.md` file in your workspace to guide Copilot throughout this exercise. Use instruction files to ensure consistent API style, naming conventions, and business rules across all spec generation tasks.

---

## 📋 Tasks

### Task 1: Identify All Endpoints

**📝 Using Instruction Files:** Create a `.instructions.md` file to define your API naming conventions, authentication approach, and standard response formats. Copilot will follow these guidelines automatically.

Ask Copilot:
```
Based on the pet appointment booking requirements:

List all REST API endpoints needed:

Requirements:
- Owner searches available slots
- Owner books appointment
- Owner gets confirmation
- Vet views their daily schedule

For each endpoint, specify:
1. HTTP method (GET/POST/PUT/DELETE)
2. Path (e.g., /slots, /appointments)
3. Purpose
4. Input parameters
5. Response data

Format as:
- GET /slots → Find available slots
  Input: date, vet_id
  Response: List of slot objects
...
```

**Expected output:** 6-8 endpoints listed with details

---

### Task 2: Create OpenAPI Skeleton

Ask:
```
Create an OpenAPI 3.0 specification skeleton for pet appointment API:

Required sections:
1. openapi: "3.0.0"
2. info: (title, description, version)
3. servers: (base URL)
4. paths: (all endpoints from Task 1)
5. components: (data models: Owner, Pet, Vet, Appointment, Slot)

Start with just structure (no details yet).

Format as YAML.
```

**Expected output:** OpenAPI YAML template with sections

---

### Task 3: Define Data Models (Schemas)

**📝 Using Instruction Files:** Add schema patterns to your instruction file (e.g., "Always include created_at and updated_at timestamps", "Use snake_case for field names", "Include validation rules in descriptions"). This ensures consistent schema design across all models.

Ask:
```
For the appointment booking API, define these data models in OpenAPI schema format:

1. **Slot**
   - id, date, time, vet_id, available (boolean), price

2. **Appointment**
   - id, slot_id, owner_id, pet_id, status (booked/cancelled/completed)
   - created_at, confirmed_at

3. **Owner**
   - id, name, email, phone, address

4. **Vet**
   - id, name, specialty, clinic_id

5. **Error** (for all error responses)
   - code, message, details

For each: list fields, data types (string/integer/boolean/datetime), required fields.

Format as OpenAPI component schemas.
```

**Expected output:** Full schema definitions with types

---

### Task 4: Document All Endpoints

**📝 Using Instruction Files:** Document your API documentation standards in the instruction file (e.g., "Every endpoint must include rate limits", "Always specify timeout values", "Include example requests and responses with real data"). This ensures thorough and consistent endpoint documentation.

Ask:
```
Complete the OpenAPI spec for these endpoints:

1. GET /slots
   - Query params: date (required), vet_id (optional)
   - Response: List[Slot]
   - Success: 200 OK
   - Errors: 400 (invalid date), 404 (no slots)

2. POST /appointments
   - Body: {slot_id, owner_id, pet_id}
   - Response: Appointment object
   - Success: 201 Created
   - Errors: 400 (invalid), 409 (slot already booked)

3. GET /appointments/{id}
   - Path param: id
   - Response: Appointment object
   - Errors: 404 (not found)

4. GET /vets/{id}/schedule
   - Query param: date
   - Response: List[Appointment]
   - Success: 200 OK

For each endpoint:
- Describe purpose (summary, description)
- Document parameters (required, type, description)
- Document response schema (inherit from schemas)
- List error codes with meanings

Format as OpenAPI paths section.
```

**Expected output:** Complete endpoint documentation with all details

---

### Task 5: Generate Swagger UI Preview

Ask:
```
Take the complete OpenAPI spec and create a simple HTML file that loads it with Swagger UI.

Include:
- OpenAPI spec embedded as JSON
- Swagger UI library (CDN)
- Interactive try-it-out console

Output as single line of HTML I can copy-paste to a .html file.

Hint: Use Swagger UI free CDN: https://unpkg.com/swagger-ui-dist/swagger-ui.css
```

**Expected output:** HTML file that renders interactive API docs

---

## 📊 Save: `outputs/exercise-1-api-specs.yaml`

Include full OpenAPI spec with:
- Info section (title, description, version)
- Servers section
- All paths (endpoints)
- All components (schemas)
- Error responses

---

## 💡 OpenAPI Spec Benefits

✅ Documentation (what are all the endpoints?)
✅ Contracts (what data does API take/return?)
✅ Code generation (generate server stubs from spec)
✅ Client SDKs (auto-generate client libraries)
✅ Testing (validate responses match spec)
✅ Version control (track API changes)

---

## 💼 Using Instruction Files Throughout This Exercise

### What Are Instruction Files?
Instruction files (`.instructions.md`, `.prompt.md`, etc.) allow you to define project-wide conventions, standards, and guidelines that Copilot will follow automatically. This ensures consistency across all spec generation, code generation, and implementation tasks.

### Where Instruction Files Help:

#### 1. **API Specification Generation** (This Exercise)
```markdown
# In your .instructions.md:

## API Design Standards
- Use RESTful conventions: GET for retrieval, POST for creation, PUT for updates
- Use kebab-case for URL paths: /pet-appointments, /pet-owners
- Use snake_case for JSON fields: pet_id, created_at, slot_count
- Always include pagination for list endpoints: limit, offset
- Standard response wrapper: { "data": [...], "meta": { "total": 100 }}
- Always include ISO 8601 timestamps: created_at, updated_at
- Rate limits: X-RateLimit-Limit, X-RateLimit-Remaining headers
```

#### 2. **Error Handling Specification** (Exercise 2)
```markdown
## Error Response Format
All errors must follow this structure:
{
  "error": {
    "code": "ERROR_CODE",
    "message": "User-friendly message",
    "status": 400,
    "details": { ... }
  }
}

## Standard HTTP Status Codes
- 400: Bad Request (invalid input)
- 401: Unauthorized (authentication failed)
- 403: Forbidden (no permission)
- 409: Conflict (resource already exists)
- 422: Validation error (detailed field errors)
```

#### 3. **Code Generation** (Exercise 3)
```markdown
## Code Generation Standards
- Language: C# (.NET)
- Framework: ASP.NET Core 8
- Naming: PascalCase for classes, camelCase for methods
- Use dependency injection for all services
- Include validation attributes: [Required], [EmailAddress]
- Add XML documentation comments to all public methods
- Use async/await for database operations
```

#### 4. **Validation Rules** (Exercise 2)
```markdown
## Field Validation Rules
- Email fields: Must match RFC 5322 standard
- Phone numbers: Must be 10+ digits with optional +1 prefix
- Dates: Always future dates only for appointments
- Prices: Exactly 2 decimal places, no negative values
- Names: 1-100 characters, alphanumeric + spaces only
```

### Workflow: How to Use Instruction Files

1. **Create instruction file early:**
   ```bash
   # Create at workspace root
   touch .instructions.md
   ```

2. **Define standards before starting exercises:**
   - API naming conventions
   - Response format templates
   - Error handling approach
   - Code style guidelines
   - Validation rules

3. **Reference in your prompts:**
   ```
   Ask Copilot:
   "Using the standards in .instructions.md, define all error codes for appointment booking API..."
   ```

4. **Copilot will automatically apply the standards** across all generated specs, code, and documentation.

### Benefits of Instruction Files

✅ **Consistency** - Same naming, style, format everywhere
✅ **Efficiency** - Don't repeat requirements in every prompt
✅ **Quality** - Enforce best practices automatically
✅ **Maintainability** - Easier to update standards later
✅ **Team alignment** - Everyone follows same conventions
✅ **Reduced errors** - Less chance of missing validation rules

---

## ✅ Success Criteria

- [ ] 6-8 endpoints defined
- [ ] All data models documented (Slot, Appointment, Owner, Vet)
- [ ] Every endpoint has: params, response, error codes
- [ ] OpenAPI spec is valid YAML
- [ ] Can load into Swagger UI
- [ ] (Bonus) Created Swagger UI preview HTML
- [ ] (Bonus) Created `.instructions.md` file with API standards
- [ ] (Bonus) Referenced instruction file in your prompts to Copilot

---

## 🚀 Next

→ [Exercise 2: Define Error Handling & Validation](./02-define-error-handling-validation.md)
