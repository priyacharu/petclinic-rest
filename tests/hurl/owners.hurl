# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners – Happy path: returns list of owners
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners
Accept: application/json

HTTP 200
[Asserts]
header "Content-Type" contains "application/json"
jsonpath "$" isCollection

# ─────────────────────────────────────────────────────────────────────────────
# POST /api/owners – Create-then-Retrieve workflow
# ─────────────────────────────────────────────────────────────────────────────
POST {{base_url}}/api/owners
Content-Type: application/json
{
  "firstName": "Jane",
  "lastName": "Smith",
  "address": "456 Oak Avenue",
  "city": "Springfield",
  "telephone": "5555550101",
  "pets": []
}

HTTP 201
[Asserts]
header "Content-Type" contains "application/json"
header "Location" exists
jsonpath "$.id" isInteger
jsonpath "$.firstName" == "Jane"
jsonpath "$.lastName" == "Smith"
jsonpath "$.address" == "456 Oak Avenue"
jsonpath "$.city" == "Springfield"
jsonpath "$.telephone" == "5555550101"
[Captures]
created_owner_id: jsonpath "$.id"

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{id} – Retrieve the owner created above
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/{{created_owner_id}}
Accept: application/json

HTTP 200
[Asserts]
header "Content-Type" contains "application/json"
jsonpath "$.id" == {{created_owner_id}}
jsonpath "$.firstName" == "Jane"
jsonpath "$.lastName" == "Smith"

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{id} – Not found: non-existent owner returns 404
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/99999
Accept: application/json

HTTP 404

# ─────────────────────────────────────────────────────────────────────────────
# PUT /api/owners/{id} – Update the owner
# ─────────────────────────────────────────────────────────────────────────────
PUT {{base_url}}/api/owners/{{created_owner_id}}
Content-Type: application/json
{
  "firstName": "Jane",
  "lastName": "Doe",
  "address": "789 Pine Road",
  "city": "Shelbyville",
  "telephone": "5555550202",
  "pets": []
}

HTTP 204

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{id} – Verify update was persisted
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/{{created_owner_id}}
Accept: application/json

HTTP 200
[Asserts]
jsonpath "$.lastName" == "Doe"
jsonpath "$.city" == "Shelbyville"

# ─────────────────────────────────────────────────────────────────────────────
# PUT /api/owners/{id} – Not found: update non-existent owner returns 404
# ─────────────────────────────────────────────────────────────────────────────
PUT {{base_url}}/api/owners/99999
Content-Type: application/json
{
  "firstName": "Ghost",
  "lastName": "User",
  "address": "Nowhere",
  "city": "Nowhere",
  "telephone": "0000000000",
  "pets": []
}

HTTP 404

# ─────────────────────────────────────────────────────────────────────────────
# POST /api/owners/{ownerId}/pets – Add a pet to the created owner
# ─────────────────────────────────────────────────────────────────────────────
POST {{base_url}}/api/owners/{{created_owner_id}}/pets
Content-Type: application/json
{
  "name": "Whiskers",
  "birthDate": "2020-06-15T00:00:00Z",
  "ownerId": {{created_owner_id}},
  "petTypeId": 1,
  "visits": []
}

HTTP 201
[Asserts]
header "Content-Type" contains "application/json"
header "Location" exists
jsonpath "$.id" isInteger
jsonpath "$.name" == "Whiskers"
[Captures]
created_pet_id: jsonpath "$.id"

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{ownerId}/pets/{petId} – Retrieve the pet just created
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/{{created_owner_id}}/pets/{{created_pet_id}}
Accept: application/json

HTTP 200
[Asserts]
header "Content-Type" contains "application/json"
jsonpath "$.id" == {{created_pet_id}}
jsonpath "$.name" == "Whiskers"

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{ownerId}/pets/{petId} – Not found: wrong owner/pet returns 404
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/{{created_owner_id}}/pets/99999
Accept: application/json

HTTP 404

# ─────────────────────────────────────────────────────────────────────────────
# POST /api/owners/{ownerId}/pets – Not found: non-existent owner returns 404
# ─────────────────────────────────────────────────────────────────────────────
POST {{base_url}}/api/owners/99999/pets
Content-Type: application/json
{
  "name": "Ghost",
  "birthDate": "2020-01-01T00:00:00Z",
  "ownerId": 99999,
  "petTypeId": 1,
  "visits": []
}

HTTP 404

# ─────────────────────────────────────────────────────────────────────────────
# PUT /api/owners/{ownerId}/pets/{petId} – Update the pet
# ─────────────────────────────────────────────────────────────────────────────
PUT {{base_url}}/api/owners/{{created_owner_id}}/pets/{{created_pet_id}}
Content-Type: application/json
{
  "name": "Whiskers Updated",
  "birthDate": "2020-06-15T00:00:00Z",
  "ownerId": {{created_owner_id}},
  "petTypeId": 1,
  "visits": []
}

HTTP 204

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{ownerId}/pets/{petId} – Verify pet update was persisted
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/{{created_owner_id}}/pets/{{created_pet_id}}
Accept: application/json

HTTP 200
[Asserts]
jsonpath "$.name" == "Whiskers Updated"

# ─────────────────────────────────────────────────────────────────────────────
# PUT /api/owners/{ownerId}/pets/{petId} – Not found: wrong pet returns 404
# ─────────────────────────────────────────────────────────────────────────────
PUT {{base_url}}/api/owners/{{created_owner_id}}/pets/99999
Content-Type: application/json
{
  "name": "Ghost Pet",
  "birthDate": "2020-01-01T00:00:00Z",
  "ownerId": {{created_owner_id}},
  "petTypeId": 1,
  "visits": []
}

HTTP 404

# ─────────────────────────────────────────────────────────────────────────────
# DELETE /api/owners/{id} – Delete the owner created above
# ─────────────────────────────────────────────────────────────────────────────
DELETE {{base_url}}/api/owners/{{created_owner_id}}

HTTP 204

# ─────────────────────────────────────────────────────────────────────────────
# GET /api/owners/{id} – Confirm owner is gone after deletion
# ─────────────────────────────────────────────────────────────────────────────
GET {{base_url}}/api/owners/{{created_owner_id}}
Accept: application/json

HTTP 404

# ─────────────────────────────────────────────────────────────────────────────
# DELETE /api/owners/{id} – Not found: delete non-existent owner returns 404
# ─────────────────────────────────────────────────────────────────────────────
DELETE {{base_url}}/api/owners/99999

HTTP 404
