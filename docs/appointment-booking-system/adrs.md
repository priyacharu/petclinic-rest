# Architecture Decision Records — Appointment Booking System

---

## ADR-001: Concurrency Control for Double-Booking Prevention

**Status:** Accepted

### Context
Story 2 requires that the booking creation step prevents two owners from simultaneously claiming the last available slot (see Impact Map §2 — Delivery Risks: "Double-booking, High likelihood, High impact"). Under up to 500 concurrent users, a naïve read-then-write pattern will produce race conditions.

### Decision
Use a **DB-level unique constraint** on `(SlotId)` in the `Booking` table combined with **EF Core optimistic concurrency** (`RowVersion` / `xmin` token). The booking endpoint reads the slot, verifies it is available, attempts to insert a `Booking` row, and catches the unique-constraint violation; on conflict the API returns `409 Conflict`.

### Rationale
- Aligns with the existing EF Core / ASP.NET Core stack — no additional infrastructure.
- The unique constraint is the last line of defence and is DB-engine-enforced regardless of application-layer bugs.
- Optimistic concurrency avoids holding locks across network round-trips, keeping latency within the < 3 s booking SLA.

### Alternatives Considered

| Alternative | Pros | Cons | Rejected because |
|---|---|---|---|
| Pessimistic lock (`SELECT FOR UPDATE`) | Guarantees no conflict | Holds DB row lock for full request duration; risk of lock contention at 500 users | Violates < 3 s SLA under load |
| Distributed lock (Redis) | Works across scale-out replicas | Adds Redis as a hard dependency; operational overhead | Over-engineered for single-clinic, single-instance deployment |
| Application-layer mutex | Simple | Not safe across multiple server instances; not durable | Fails when horizontally scaled |

### Consequences
- **Positive:** Zero double-bookings guaranteed by the database engine.
- **Trade-off:** Clients must handle `409 Conflict` and prompt the user to select another slot.
- **Risk:** EF Core optimistic concurrency requires `RowVersion` column on `AppointmentSlot`; schema migration needed.

### References
- [Impact Map §2 — Delivery Risks](./impact-map.md)
- [Requirements — Story 2 Acceptance Criteria](../requirements.md)

---

## ADR-002: Asynchronous Notification Delivery via Background Queue

**Status:** Accepted

### Context
Story 3 requires email AND SMS confirmation within 1 minute of booking, with retry up to 3×, and delivery tracking. Sending notifications synchronously inside the booking HTTP request would couple booking latency to external provider latency and violate the < 3 s SLA.

### Decision
Publish a `BookingConfirmed` domain event to an **in-process background queue** (using `System.Threading.Channels` + a hosted `BackgroundService`). The worker dequeues the event, calls the email and SMS provider SDKs, records delivery status in the `NotificationLog` table, and retries up to 3× with exponential back-off on failure. The booking HTTP response is returned to the owner immediately after the event is enqueued.

### Rationale
- Decouples booking latency from provider latency — booking always completes in < 3 s.
- `System.Threading.Channels` is built into .NET; no additional broker infrastructure for v1 (single-clinic, single-instance scope).
- Delivery status is persisted, satisfying the "track if notification was delivered" acceptance criterion.

### Alternatives Considered

| Alternative | Pros | Cons | Rejected because |
|---|---|---|---|
| Synchronous send inside HTTP handler | Simple code path | Ties booking SLA to provider latency; partial failure leaves booking confirmed but no notification sent | Violates < 3 s SLA |
| External message broker (RabbitMQ / Azure Service Bus) | Durable, survives process restarts | Significant infrastructure overhead; out of scope for single-clinic v1 | Over-engineered for current scale |
| Fire-and-forget `Task.Run` | Minimal code | No retry, no delivery tracking, no back-pressure | Does not satisfy acceptance criteria |

### Consequences
- **Positive:** Booking latency is independent of notification provider availability.
- **Trade-off:** In-process queue is not durable — if the process crashes between enqueue and send, the notification is lost. Accept for v1; migrate to a durable broker in v2 if needed.
- **Risk:** Must implement idempotent notification send to avoid duplicate sends on retry.

### References
- [Impact Map §2 — Delivery Risks: Notification delivery failure](./impact-map.md)
- [Requirements — Story 3 Acceptance Criteria](../requirements.md)

---

## ADR-003: ASP.NET Core Identity for Authentication and Authorisation

**Status:** Accepted

### Context
All four user stories require authenticated access. Owners must see only their own appointments; vets must see only their own schedule. The impact map flags auth as the highest-effort risk. A concrete technology choice must be made before the booking flow can be implemented.

### Decision
Use **ASP.NET Core Identity** with **JWT Bearer tokens**. A dedicated `AuthController` handles `/auth/login` (owner and vet), issuing a short-lived JWT. Role claims (`Owner`, `Vet`, `Staff`) are embedded in the token and validated by controller `[Authorize(Roles = ...)]` attributes.

### Rationale
- Native to the ASP.NET Core / EF Core stack already in use — no new runtime dependencies.
- JWT is stateless; scales to multiple replicas in future without session-store infrastructure.
- Role-based claims satisfy the "Owners see own appts only" and vet schedule access controls directly.

### Alternatives Considered

| Alternative | Pros | Cons | Rejected because |
|---|---|---|---|
| External IdP (Auth0, Azure AD B2C) | Managed service, MFA out of the box, social login | External dependency, cost, integration complexity for v1 | Over-engineered for single-clinic v1 with known users |
| Cookie-based sessions | Familiar, works without JS | Requires session store for scale-out; CSRF risk | Stateful; complicates future API consumers (mobile) |

### Consequences
- **Positive:** Full control over user schema; no external cost.
- **Trade-off:** Team owns password hashing, token refresh, and account lockout logic.
- **Risk:** JWT secret rotation must be handled; short token lifetime (15 min) with refresh tokens is required.

### References
- [Impact Map §3 — Assumptions (Auth)](./impact-map.md)
- [Requirements — Non-Functional: Security](../requirements.md)

---

## ADR-004: UTC Storage with Client-Side Timezone Conversion

**Status:** Accepted

### Context
Story 1 requires displaying slot times in the owner's local timezone. The clinic is a single location but owners may access the system from different timezones. The impact map flags timezone mismatch as a medium-risk delivery concern.

### Decision
Store all `AppointmentSlot` start/end times as **UTC `DateTimeOffset`** in the database. The API returns UTC timestamps (ISO 8601 with `Z` suffix). The client (web front-end) converts to the user's local timezone using browser-native `Intl.DateTimeFormat` or the `date-fns-tz` library.

### Rationale
- UTC storage eliminates ambiguity from DST transitions and clock changes.
- Pushing conversion to the client avoids maintaining a timezone-per-owner field server-side for v1.
- ISO 8601 is unambiguous and supported by all modern browsers.

### Alternatives Considered

| Alternative | Pros | Cons | Rejected because |
|---|---|---|---|
| Store in clinic's local time | Simpler DB values | DST transitions corrupt stored times; not portable | Correctness risk |
| Store timezone per owner, convert server-side | Consistent API output | Requires owner profile extension + NodaTime IANA DB | Added complexity; browser conversion sufficient for web-only v1 |

### Consequences
- **Positive:** No server-side timezone library dependency for v1.
- **Trade-off:** Email/SMS confirmations must also render times correctly — notification worker must convert UTC to a configured clinic timezone for message content.
- **Risk:** If a non-browser client (future mobile) is added, server-side conversion logic will be needed.

### References
- [Impact Map §2 — Delivery Risks: Timezone mismatch](./impact-map.md)
- [Requirements — Story 1 AC: Display in owner's local timezone](../requirements.md)

---

## ADR-005: New AppointmentSlot Entity (Not Reusing Visit)

**Status:** Accepted

### Context
The existing `Visit` entity records past veterinary visits. The impact map explicitly states: "The existing Visit entity is NOT the same as a bookable AppointmentSlot; a new entity is required." Reusing `Visit` would require nullable fields, status flags, and semantic overloading that would compromise the integrity of the existing history model.

### Decision
Introduce a new **`AppointmentSlot`** entity with fields: `Id`, `VetId`, `StartUtc`, `EndUtc`, `IsAvailable`, `RowVersion`. Introduce a **`Booking`** entity with fields: `Id`, `SlotId` (unique FK), `OwnerId`, `PetId`, `ReasonForVisit`, `Status`, `CreatedAtUtc`. After a completed visit, a clinic staff member links the booking to a `Visit` record if needed.

### Rationale
- Clean separation of concerns: slots are future availability; visits are historical records.
- `AppointmentSlot.IsAvailable` + unique constraint on `Booking.SlotId` directly supports ADR-001.
- No destructive changes to the existing `Visit` table — backward compatible.

### Alternatives Considered

| Alternative | Pros | Cons | Rejected because |
|---|---|---|---|
| Reuse `Visit` with a `Status` enum | No schema additions | Semantic overload; nullable fields; breaks existing queries and reports | Corrupts the historical visit record |
| Single `Appointment` entity (no separate Slot) | Simpler schema | Staff cannot pre-create slots without an owner already assigned | Violates the constraint "Manual slot creation by clinic staff" |

### Consequences
- **Positive:** Domain model is clean; each entity has a single, clear responsibility.
- **Trade-off:** Two schema migrations required; EF Core navigation properties between Slot, Booking, Vet, Owner, and Pet must be configured carefully.
- **Risk:** The link between a completed `Booking` and a historical `Visit` must be handled as a separate workflow (out of scope for v1).

### References
- [Impact Map §3 — Assumptions: Visit vs AppointmentSlot](./impact-map.md)
- [Requirements — Constraints: Manual slot creation](../requirements.md)
