# Filter Vets by Specialty

## Story

**As a** client  
**I want to** filter vets by specialty  
**So that** I can show only relevant vets

---

## Scope

Add an optional `specialty` query parameter to the existing `GET /api/vets` endpoint (`VetsController`). The parameter filters the returned list to vets who have at least one matching specialty. When omitted, all vets are returned as before.

---

## Acceptance Criteria

```gherkin
GIVEN the client calls GET /api/vets without a specialty parameter
WHEN there are vets in the database
THEN all vets are returned with status 200 OK

GIVEN the client calls GET /api/vets?specialty=radiology
WHEN there are vets who have the specialty "radiology"
THEN only those vets are returned with status 200 OK

GIVEN the client calls GET /api/vets?specialty=radiology
WHEN no vets have the specialty "radiology"
THEN an empty array is returned with status 200 OK

GIVEN the client calls GET /api/vets?specialty=
WHEN the specialty parameter is an empty string
THEN all vets are returned with status 200 OK (treated as if omitted)
```

---

## API Contract

### Endpoint

```
GET /api/vets
```

### Query Parameters

| Parameter   | Type   | Required | Description                                                              |
|-------------|--------|----------|--------------------------------------------------------------------------|
| `specialty` | string | No       | Case-insensitive specialty name to filter by (e.g. `radiology`, `surgery`) |

### Responses

| Status | Description                       |
|--------|-----------------------------------|
| 200 OK | List of vets (may be empty array) |

### Example Requests

```
GET /api/vets
GET /api/vets?specialty=radiology
GET /api/vets?specialty=Surgery
```

### Example Response (200 OK)

```json
[
  {
    "id": 1,
    "firstName": "James",
    "lastName": "Carter",
    "specialties": [
      { "id": 1, "name": "radiology" }
    ]
  }
]
```

---

## Implementation Notes

- Filter is applied in `VetsController.GetVets(string? specialty)`.
- Matching is **case-insensitive** on `Specialty.Name`.
- An empty or whitespace-only `specialty` value is treated as if the parameter was not provided.
- No new endpoint is introduced; the existing `GET /api/vets` is extended.

---

## Out of Scope

- Filtering by multiple specialties in a single request
- Pagination of the filtered results
- Authentication / authorisation changes
