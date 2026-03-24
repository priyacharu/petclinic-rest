# Exercise 2: Define Error Handling & Validation

**⏱️ Time: 20 min** | **🎯 Goal: Specify error cases, validations, and edge cases**

---

## 📖 What You'll Create

**Error handling specification** that defines:
- What can go wrong? (400, 401, 404, 409, 500, etc.)
- How should the API respond? (error messages, codes)
- What validation rules? (field constraints, formats, boundaries)
- What edge cases? (empty inputs, extreme values, missing fields)
- When should we retry? (idempotency, transient failures)

**This ensures:**
- ✅ No surprises in production (you planned for failures)
- ✅ Consistent error messages (users know what went wrong)
- ✅ Input validation prevents bad data (garbage in → garbage out)
- ✅ Developers know what to implement (not left guessing)

**💡 Pro Tip:** Use your `.instructions.md` file to document global error handling standards and validation rules. This ensures consistency across all exercises and makes code generation much more accurate!

---

## 📋 Tasks

### Task 1: Define Validation Rules for Each Field

**📝 Using Instruction Files:** Add validation patterns to your `.instructions.md` file such as:
- "Email fields must match RFC 5322"
- "Phone numbers: 10+ digits with optional country code"
- "Dates: Use ISO 8601 format only"
- "Currency amounts: Exactly 2 decimal places"

Then reference these standards in your prompt.

Ask Copilot:
```
Define validation rules for appointment booking API data models:

For each field, specify:
1. Required or optional?
2. Data type (string, integer, boolean, datetime)
3. Format constraints (regex, length, range)
4. Examples of valid & invalid values

Models:

1. **Slot**
   - id: string (UUID format)
   - date: string (YYYY-MM-DD format, today or future)
   - time: string (HH:MM format, 09:00 to 17:00)
   - vet_id: string (non-empty)
   - available: boolean
   - price: number (positive, max 2 decimals)

2. **Appointment**
   - id: string (UUID)
   - slot_id: string (must exist in database)
   - owner_id: string (must exist)
   - pet_id: string (must exist)
   - status: enum (booked, cancelled, completed)
   - created_at: ISO 8601 datetime

3. **Owner**
   - id: string (UUID)
   - name: string (1-100 chars, no special chars)
   - email: string (valid email format)
   - phone: string (valid phone format)

4. **Vet**
   - id: string (UUID)
   - name: string (1-100 chars)
   - specialty: string (enum: General, Dentist, Surgeon, etc.)

For each field, format as:
Field Name: [Required/Optional]
  Type: [data type]
  Format: [constraints]
  Valid examples: [...]
  Invalid examples: [...]
  Error message: [what to tell user if invalid]
```

**Expected output:** Validation rules for all fields

---

### Task 2: Define Error Codes & Messages

**📝 Using Instruction Files:** Document your error handling approach in `.instructions.md`:
```markdown
## Error Response Standards
- All errors return consistent JSON structure
- Include error code (e.g., SLOT_NOT_FOUND) along with HTTP status
- Provide user-facing message AND developer message
- Include hints for client-side retry logic
```

Ask:
```
Define all error codes for appointment booking API:

For each endpoint, what can go wrong?

GET /slots endpoint:
- 400: Invalid date format (user sent "tomorrow" instead of date)
- 400: Invalid vet_id format
- 404: No slots found for that date
- 500: Database connection failed

POST /appointments endpoint:
- 400: Missing required field (slot_id, owner_id, pet_id)
- 400: Invalid owner_id format (not a UUID)
- 404: Slot not found (doesn't exist)
- 409: Slot already booked (concurrent booking)
- 409: Slot is in the past (can't book old slots)
- 500: Failed to send confirmation email (non-blocking)
- 503: Payment service temporarily down

GET /appointments/{id} endpoint:
- 404: Appointment not found
- 400: Invalid appointment ID format

GET /vets/{id}/schedule endpoint:
- 404: Vet not found
- 400: Invalid date format

For each error, specify:
1. HTTP status code
2. Error code (unique identifier)
3. User-facing message (what to show in UI)
4. Developer message (details for debugging)
5. Suggested fix

Format as table or structured list.
```

**Expected output:** Complete error code catalog with HTTP status + messages

---

### Task 3: Define Edge Cases & Boundaries

**📝 Using Instruction Files:** Document business rules that define edge cases:
```markdown
## Business Logic Standards
- Appointments must be booked 24 hours in advance
- Slots available 90 days out
- Cancellations must be at least 2 hours before appointment
- Overlapping appointments not allowed
```

Ask:
```
Define edge cases and boundary conditions for appointment booking:

Edge cases to handle:

1. **Date boundaries**
   - What's the earliest bookable date? (same day, next day, 7 days out?)
   - What's the latest bookable date? (30 days, 90 days?)
   - Booking at 23:59 vs 00:01?
   - Leap year dates? (Feb 29)
   - Daylight saving time transitions?

2. **Timezone handling**
   - User in timezone A, vet in timezone B → display which timezone?
   - "Tomorrow" means tomorrow in user's timezone? Or vet's?
   - Store appointments in UTC? Show in local?

3. **Concurrent bookings (race condition)**
   - Two users book same slot simultaneously
   - How to prevent? (database locking? optimistic locking?)
   - Which user wins?

4. **Cascade operations**
   - Cancel appointment → what happens to confirmation email?
   - Vet unavailable → auto-cancel their appointments?

5. **Boundary values**
   - Minimum appointment price: $0? $0.01?
   - Maximum price: (no limit? $10,000?)
   - Appointment duration: 15 min, 30 min, 1 hour?

6. **Special cases**
   - Client with no pets (can they book?)
   - Owner books multiple appointments same day?
   - Owner cancels 5 mins before appointment?

For each edge case:
  Case: [description]
  Condition: [what triggers this]
  Behavior: [what should happen]
  Error handling: [is it an error or allowed?]
```

**Expected output:** Comprehensive edge case specification

---

### Task 4: Define Idempotency & Retry Logic

**📝 Using Instruction Files:** Specify resilience patterns in `.instructions.md`:
```markdown
## Reliability Standards
- POST endpoints must support idempotency keys (X-Idempotency-Key header)
- Notifications should retry 3 times with exponential backoff
- Database failures should not rollback user-facing operations
- Timeout threshold: 30 seconds for synchronous operations
```

Ask:
```
Define idempotency and retry requirements:

Idempotency: If user clicks "Book" twice, does it create 2 appointments or 1?

For each endpoint:

1. **GET endpoints** (always safe & idempotent)
   - GET /slots: Safe to call multiple times
   - GET /appointments/123: Safe to call multiple times

2. **POST /appointments (NOT idempotent by default)**
   - Issue: User clicks "Confirm booking", network drops, clicks again
   - Should we create 1 appointment or 2?
   - Solution: Use idempotency key (request_id)
   - Example: Client sends X-Idempotency-Key header
   - Server checks: "Have I seen this key before?"
   - If yes: Return same response (don't remake appointment)
   - If no: Create appointment, remember the key

3. **Notification service retries**
   - Send confirmation email fails (service down)
   - Retry 3 times? Exponential backoff?
   - If still fails, what? (queue for later? alert admin?)

4. **Transaction rollback**
   - Booking starts: lock slot, create appointment, send email
   - Email fails → rollback?) Keep appointment?)
   - Recommendation: Email failure should NOT rollback booking

For each operation, specify:
  Operation: [POST /appointments, etc]
  Is idempotent?: [Yes/No]
  If not idempotent, how to make it idempotent?: [idempotency key?]
  Retry strategy: [how many times? backoff?]
  Failure handling: [rollback? partial success?]
```

**Expected output:** Idempotency and retry specifications

---

### Task 5: Create Error Handling & Validation Document

Ask:
```
Create comprehensive error handling & validation specification:

Combine all previous tasks into one document:

# Error Handling & Validation Specification

## 1. Field Validations
[From Task 1 - validation rules for all fields]

## 2. Error Codes
[From Task 2 - HTTP status + error codes + messages]

## 3. Edge Cases
[From Task 3 - boundary conditions + special cases]

## 4. Idempotency & Retries
[From Task 4 - idempotent operations + retry logic]

## 5. Response Format (for errors)
Standard error response format:
\`\`\`json
{
  "error": {
    "code": "SLOT_NOT_FOUND",
    "message": "User-friendly message",
    "details": {
      "requested_slot_id": "123",
      "hint": "Slot may have been booked by another user"
    }
  }
}
\`\`\`

## 6. Validation Examples
Show examples of:
- Valid POST /appointments request
- Invalid request (missing field)
- Invalid request (bad format)
- Error response for each

Include the exact JSON/request format for developers.

Format as structured spec document.
```

**Expected output:** Complete error handling & validation spec

---

## 📊 Save: `outputs/exercise-2-error-handling-validation.md`

Include:
- Field validation rules (all 4 data models)
- Error codes + HTTP status + messages
- Edge cases & boundary conditions
- Idempotency + retry logic
- Error response format examples
- Invalid request examples

---

## 💡 Why Error Handling Specs Matter

✅ **Prevents surprises** - Developers aren't left guessing
✅ **Consistent UX** - Users see predictable error messages
✅ **Data integrity** - Validation prevents garbage data
✅ **Reliability** - Retries handle transient failures
✅ **Concurrency** - Idempotency prevents race condition bugs
✅ **Edge cases** - Handle those weird scenarios nobody thought of

---

## 🔄 Common Validation Mistakes to Avoid

❌ Validating in code only (no spec = inconsistent)
❌ No idempotency (duplicate bookings possible)
❌ No retry logic (failed notification = lost data)
❌ Vague error messages ("Error" vs "Slot already booked")
❌ Race conditions (two users book same slot)

---

## ✅ Success Criteria

- [ ] 15+ validation rules defined (all fields)
- [ ] 8+ error codes specified (HTTP status + messages)
- [ ] 5+ edge cases documented
- [ ] Idempotency strategy defined (for POST endpoints)
- [ ] Retry logic specified (for notifications)
- [ ] Error response format with examples
- [ ] Invalid request examples showing errors
- [ ] (Bonus) Updated `.instructions.md` with error handling & validation standards
- [ ] (Bonus) Referenced instruction file in prompts to ensure consistency

---

## 🚀 Next

→ [Exercise 3: Generate Code from Specs](./03-generate-code-from-specs.md)
