---
agent: 'agent'
description: 'Generate Hurl integration tests for a selected controller or API endpoint'
---
## Task
Analyze the selected controller or API endpoint file and generate a `.hurl` integration test file that validates the HTTP contract of every endpoint.

## Test Generation Strategy
1. **Happy Path Tests**
   - Issue a request for each HTTP method and route defined in the controller
   - Assert the expected 2xx status code (200, 201, 204 as appropriate)
   - Assert `Content-Type: application/json` header where a body is returned
   - Use `jsonpath` assertions to verify key fields in the response body

2. **Not Found / Missing Resource Tests**
   - Issue a GET/PUT/DELETE for a resource ID that does not exist (e.g., `99999`)
   - Assert HTTP 404

3. **Invalid Input Tests**
   - Issue a POST or PUT with a missing or malformed body
   - Assert HTTP 400

4. **Create-then-Retrieve Workflow**
   - POST a new resource and capture the returned `id`
   - GET the resource by that `id` and verify the returned data matches what was sent

## Output Format
- Output a single `.hurl` file named after the controller (e.g., `owners.hurl`)
- Place it under `tests/hurl/`
- Prefix each entry with a `# [Scenario description]` comment
- Use `{{base_url}}` as a Hurl variable instead of hard-coding the host/port

## Hurl Entry Template
```
# [Scenario description]
[HTTP METHOD] {{base_url}}/api/[resource]
Accept: application/json

HTTP [status]
[Asserts]
header "Content-Type" contains "application/json"
jsonpath "$.[field]" [matcher]