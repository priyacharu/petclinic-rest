---
agent: 'agent'
description: 'Generate unit and integration test cases from a requirement and optional tech specs. Use when: given a requirement, user story, or acceptance criteria and you need to derive test cases.'
---

## Inputs
- **Requirement**: The feature or behaviour to be tested (provided inline or as an attached file)
- **Tech Spec** *(optional)*: Implementation details — controller name, method signature, DTO shape, HTTP route — that constrain how the tests are written

If no tech spec is provided, infer the affected code from the requirement and the existing codebase.

---

## Step 1 – Analyse the Requirement

Before writing any test code, reason through the requirement:

1. **Identify the observable behaviour** — what should the system do when the requirement is met?
2. **Extract acceptance criteria** — list every "must", "should", and "must not" statement (explicit or implied)
3. **Identify edge cases** — empty inputs, boundary values, missing resources, concurrent writes
4. **Classify each scenario** by test type:
   - `unit` — pure logic, no I/O, mock dependencies
   - `integration` — HTTP contract, status codes, persistence (use Hurl)

Output a numbered list of scenarios before generating any code.

---

## Step 2 – Generate Unit Tests (xUnit)

For each `unit` scenario identified above, generate a `[Fact]` or `[Theory]` test following the project's Testing Guidelines in `.github/instructions/testing.instructions.md`.

### Rules
- Place tests in `tests/unit/PetClinicRest.Tests/Controllers/` (mirror production path)
- Class name: `[ControllerOrClass]Tests`
- Method name: `[MethodName]_[Scenario]_[ExpectedResult]`
- Use **AAA** structure with explicit `// Arrange`, `// Act`, `// Assert` comments
- Mock all external dependencies with `Moq` (`Mock<T>`)
- Use `FluentAssertions` for assertions
- One scenario per `[Fact]`; use `[Theory]` + `[InlineData]` only for true data-driven cases

### Template
```csharp
public class [ClassName]Tests
{
    private readonly Mock<[IDependency]> _mock[Dependency];
    private readonly [ClassName] _sut;

    public [ClassName]Tests()
    {
        _mock[Dependency] = new Mock<[IDependency]>();
        _sut = new [ClassName](_mock[Dependency].Object);
    }

    [Fact]
    public async Task [MethodName]_[Scenario]_[ExpectedResult]()
    {
        // Arrange
        [setup test data and mock behaviour]

        // Act
        var result = await _sut.[MethodName]([args]);

        // Assert
        [assertions on result and mock verifications]
    }
}
```

---

## Step 3 – Generate Integration Tests (Hurl)

For each `integration` scenario identified above, generate a Hurl entry in `tests/hurl/[resource].hurl`.

### Rules
- One `.hurl` file per resource/controller; append to existing file if it already exists
- Prefix every entry with `# [Scenario description]`
- Use `{{base_url}}` — never hard-code host or port
- Assert status code, `Content-Type` header, and key `jsonpath` fields
- Chain create → retrieve workflows using `[Captures]`

### Template
```hurl
# [Scenario description]
[HTTP METHOD] {{base_url}}/api/[resource]
Content-Type: application/json
[Optional body]

HTTP [status]
[Asserts]
header "Content-Type" contains "application/json"
jsonpath "$.[field]" [matcher]
```

---

## Step 4 – Output Summary

After generating all tests, produce a short summary table:

| # | Scenario | Type | Test name / Hurl entry |
|---|----------|------|------------------------|
| 1 | … | unit / integration | … |

Then list any gaps — requirements or edge cases not yet covered — and suggest follow-up tests.

---

## Example Invocation

```
/generate-tests-from-requirement
Requirement: "GET /api/owners should return 200 with an empty array when no owners exist, not 404"
Tech spec: OwnersController.GetOwners() – returns IEnumerable<OwnerDto>
```
