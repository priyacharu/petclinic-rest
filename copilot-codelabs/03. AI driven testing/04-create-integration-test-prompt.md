# Exercise 4: Generate a custom integration-test prompt with `/create-prompt`

## Objective
Create a reusable custom prompt `/generate-integration-test` using the `/create-prompt` command that generates Hurl-based integration tests for selected controllers or API endpoints.

## Why this matters
Using `/generate-integration-test` standardizes how the team creates HTTP-level tests, ensuring consistent coverage of status codes, response shapes, and error scenarios across every endpoint — while keeping tests readable and CI-friendly.

## What you will build
- A new prompt definition in your local workspace
- A focus on endpoint-level Hurl test generation, anchored by the **Integration Testing with Hurl** section you added to the `Testing Guidelines` in Exercise 3
- Exploration steps for how to tune the prompt for different controllers

## Prerequisites
- Exercise 1 completed: `.github/instructions/testing.instructions.md` exists with the base Testing Guidelines
- Exercise 3 completed: the **Integration Testing with Hurl** section has been appended to that file
- Hurl installed locally (`brew install hurl` on macOS)

## Exercise steps

1. Read the updated policy in `.github/instructions/testing.instructions.md`, focusing on the **Integration Testing with Hurl** section.

2. Create a custom prompt using `/create-prompt` in your Copilot chat interface. Use the following sample as a starting point and save it under `.github/prompts/generate-integration-test.prompt.md`:

```markdown
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
```

Generate tests that verify the HTTP contract and give confidence that the API behaves correctly end-to-end.
```

3. Experiment and iterate:
   - Add or modify prompt sections to suit the endpoint under test.
   - Try `/generate-integration-test OwnersController.cs` and `/generate-integration-test VetsController.cs`.
   - Confirm the generated `.hurl` file:
     - Has a comment above every entry describing the scenario
     - Asserts status codes on every entry
     - Uses `{{base_url}}` variable substitution
     - Covers success, not-found, and invalid-input scenarios

4. Run the generated tests to validate them:
   ```bash
   hurl --variable base_url=http://localhost:5000 tests/hurl/owners.hurl
   ```
   Confirm all entries pass (green output) against a locally running instance of the application (`dotnet run`).

---

## Validation
- `.github/prompts/generate-integration-test.prompt.md` exists and is accessible via `/generate-integration-test` in Copilot chat.
- Generated `.hurl` files follow the conventions defined in `.github/instructions/testing.instructions.md`.
- At least one generated `.hurl` file runs successfully against the local application.
