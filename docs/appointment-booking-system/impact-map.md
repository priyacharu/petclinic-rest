# Impact Map: Appointment Booking System

---

## 1. Requirements and Acceptance Criteria

| # | User Story | Functional Requirement | Acceptance Criteria | Gaps / Ambiguities |
|---|---|---|---|---|
| S1 | Owner views available slots | Display bookable appointment slots for a selected pet and date | Load < 2 s; show slots for next 30 days; display in owner's local timezone; show only available (not booked) slots | Timezone source not specified (browser locale? owner profile field?). "Available" definition not clarified — does a cancellation immediately free the slot? Who resolves conflicts if two owners view the same last slot simultaneously? |
| S2 | Owner books an appointment | Allow owner to select a slot and confirm a booking | Booking completes < 3 s; prevents double-booking (same vet, same time); confirmation email sent ≤ 1 min; owner sees "Booking confirmed" | Concurrency strategy for the last available slot is undefined (optimistic lock, pessimistic lock, queue?). Cancellation / rescheduling flow is not described. What happens if email send fails at booking time? |
| S3 | Confirmation notification | Send email AND SMS after successful booking | Email: date, time, vet name, clinic address; SMS: date, time, vet name (concise); retry up to 3×; track delivery status | Retry backoff interval not specified. What constitutes "delivered" — SMTP acceptance or open tracking? Owner contact details (email, phone) assumed to exist on the Owner record but not confirmed in the schema. |
| S4 | Vet views daily schedule | Present today's booked appointments and this week's pending appointments | Show pet name, owner name, owner phone, reason for visit (if provided); sorted by time; load < 1 s | "Pending" vs "booked" distinction not defined. "Reason for visit" field does not exist in the current Visit/Appointment model — requires schema addition. Authentication role for vet not described. |

**Common gaps across all stories:**
- No cancellation or rescheduling requirement defined.
- Authentication mechanism (JWT, cookie, OpenID Connect) not specified, only "login required".
- "Slot" as an entity does not exist in the current PetClinic schema (only `Visit`); a new `AppointmentSlot` concept must be designed.
- No definition of what "pending" means for appointments.

---

## 2. Affected Modules, Dependencies, and Risks

### Application / Domain Modules

| Module | Responsibility | Status |
|---|---|---|
| **Slot Management** | Create, list, and reserve appointment slots (clinic staff creates slots; system locks on booking) | New — does not exist |
| **Booking** | Owner selects slot → creates confirmed appointment; enforces no double-booking | New |
| **Notification** | Sends email + SMS on booking confirmation; retry logic; delivery tracking | New |
| **Vet Schedule** | Aggregates booked + pending appointments for a vet, filtered to day/week | New view layer; reads existing Vet/Visit data |
| **Owner / Identity** | Owner authentication, profile (email, phone for notifications) | Partial — Owners exist; auth and contact fields may need extension |
| **Vet / Identity** | Vet authentication, role-based access to schedule view | Partial — Vets exist; auth layer new |

### Technical Dependencies

| Dependency | Purpose | Notes |
|---|---|---|
| Existing PetClinic DB | Owners, Pets, Vets, Visits (base data) | Needs schema additions: `AppointmentSlot`, `Booking`, delivery tracking fields |
| Email provider (SendGrid / Mailgun) | Booking confirmation email | Provider not yet chosen; SDK integration required |
| SMS provider (Twilio or similar) | Booking confirmation SMS | Provider not yet chosen; phone number must exist on Owner |
| Auth / Identity service | Login for owners and vets | Not yet implemented; provider (ASP.NET Identity, OAuth?) TBD |
| Timezone database (IANA / `NodaTime`) | Convert slot times to owner's local timezone | Library selection TBD |
| Concurrency control (DB or cache) | Prevent double-booking under concurrent load | Strategy (row-level lock, optimistic concurrency, distributed lock) TBD |

### Delivery Risks

| Risk | Likelihood | Impact | Mitigation |
|---|---|---|---|
| **Double-booking** (concurrent last-slot race) | High | High | Use DB-level unique constraint + optimistic concurrency or SELECT FOR UPDATE |
| **Notification delivery failure** | Medium | Medium | Implement retry queue (up to 3×); persist delivery status; alert on failure |
| **Timezone mismatch** (owner ≠ clinic timezone) | Medium | Medium | Store all times in UTC; convert on display using owner's stored or detected locale |
| **Schema migration complexity** (adding Slot/Booking entities) | Medium | Medium | Plan incremental migrations; keep Visit table backward-compatible |
| **Auth integration effort** | High | High | Spike on auth provider choice before committing to booking flow |
| **Performance under load** (500 concurrent users) | Low–Medium | High | Add DB indexes on slot date + vet ID; cache slot availability; load test early |
| **Retry storm** (notification failures spike) | Low | Medium | Use exponential back-off; cap total retry window; use a background job queue |

---

## 3. Assumptions and Scope Boundaries

### Explicit Assumptions

1. The existing `Owner` entity will be extended with `Email` and `Phone` fields if not already present.
2. All appointment times are stored in UTC and converted to the owner's timezone at the presentation layer.
3. Clinic staff (not the system) pre-creates `AppointmentSlot` records; the booking system only allows owners to claim them.
4. A "slot" belongs to exactly one vet and has a fixed duration (no variable-length appointments in scope).
5. Vet and owner authentication will use ASP.NET Identity (or an equivalent) — exact provider to be decided in the design phase.
6. "Pending" appointments = slots booked but not yet today; "today's appointments" = slots whose date matches the vet's local date.
7. The existing `Visit` entity is NOT the same as a bookable `AppointmentSlot`; a new entity is required.
8. Owner sees only their own appointments; vets see only their own schedule.

### In Scope

- Appointment slot listing (for a selected pet + date range)
- Booking creation with double-booking prevention
- Email + SMS confirmation with retry (up to 3×) and delivery tracking
- Vet daily/weekly schedule view
- Single-clinic operation
- Web interface only
- Working hours only (9 AM – 6 PM, weekdays)
- Standard checkup services only
- Manual slot creation by clinic staff

### Out of Scope

- Mobile application
- Multi-clinic support
- Appointment cancellation or rescheduling
- Recurring appointments
- Payment processing
- Surgery or complex procedures
- Telemedicine / remote consultations
- Multi-role clinic staff management
- Automated / AI-driven slot generation
