# PetClinic: Appointment Booking System - Requirements

## Project Vision

Enable pet owners to book veterinary appointments online, reducing phone calls and manual scheduling.

---

## User Stories

### Story 1: Owner Views Available Appointment Slots

```gherkin
GIVEN a pet owner is logged into the system
WHEN they select their pet and preferred date
THEN they see all available appointment slots for that day
AND each slot shows: time, vet name, price, duration
```

**Acceptance Criteria:**
- Load time < 2 seconds
- Show slots for next 30 days
- Display in owner's local timezone
- Show only available (not booked) slots

---

### Story 2: Owner Books an Appointment

```gherkin
GIVEN a pet owner is viewing available slots
WHEN they select a slot and confirm booking
THEN the appointment is created and confirmed
AND owner receives confirmation (email/SMS)
```

**Acceptance Criteria:**
- Booking completes in < 3 seconds
- Prevents double-booking (same vet, same time)
- Confirmation email sent within 1 minute
- Owner can see "Booking confirmed" message

---

### Story 3: Owner Receives Confirmation Notification

```gherkin
GIVEN an appointment was successfully booked
WHEN 1 minute has passed
THEN owner receives confirmation via email AND SMS
```

**Acceptance Criteria:**
- Email includes: date, time, vet name, clinic address
- SMS includes: date, time, vet name (concise)
- Retry if delivery fails (up to 3 times)
- Track if notification was delivered

---

### Story 4: Vet Views Their Daily Schedule

```gherkin
GIVEN a vet logs into the system
WHEN they view their calendar
THEN they see all booked appointments for today
AND pending appointments from this week
```

**Acceptance Criteria:**
- Show appointment details: pet name, owner name, owner phone
- Show reason for visit (if provided)
- Appointments sorted by time
- Load time < 1 second

---

## Non-Functional Requirements

| Category | Requirement | Target |
|----------|-------------|--------|
| **Performance** | Slot search response | < 2 seconds |
| | Booking completion | < 3 seconds |
| | Daily schedule load | < 1 second |
| **Reliability** | System uptime | 99.5% |
| | Notification delivery | 100% (with retries) |
| | Data accuracy | No double-bookings |
| **Scale** | Concurrent users | Up to 500 |
| | Appointments/day | 1,000 |
| | Data retention | 5 years |
| **Security** | Authentication | Login required |
| | Data encryption | SSL/TLS for transit |
| | Authorization | Owners see own appts only |

---

## Actors & Systems

**Primary Actors:**
- Pet Owner (books appointments)
- Veterinarian (views schedule)
- Clinic Staff (manages vets)

**External Systems:**
- Email service (send confirmations)
- SMS provider (Twilio or similar)

---

## Key Constraints

1. **Single clinic only** (not multi-clinic)
2. **Available on web** (not mobile app yet)
3. **Working hours only** (9 AM - 6 PM weekdays)
4. **Standard services only** (no surgery, just checkups)
5. **Manual slot creation** (clinic staff pre-creates slots)

---

## Success Metrics

- **Usage**: 50% of owners book online (vs phone)
- **Time saved**: 30 minutes/day for clinic staff
- **Customer satisfaction**: 4.5+/5 stars
- **System reliability**: 99.5% uptime
- **Performance**: 95% of bookings < 3 seconds

---

## Dependencies

- Existing PetClinic database (owners, pets, vets, clinics)
- Email provider (SendGrid, mailgun, etc.)
- SMS provider (Twilio or similar)

---

## Known Risks

1. **Double-booking**: Multiple users booking same slot simultaneously
2. **Notification failures**: Email/SMS delivery not guaranteed
3. **Timezone confusion**: Owner and clinic in different timezones
4. **Capacity planning**: Need accurate slot allocation

---

## Out of Scope (Future)

- Mobile app
- Multi-clinic support
- Recurring appointments
- Payment processing
- Surgery/complex procedures
- Telemedicine
