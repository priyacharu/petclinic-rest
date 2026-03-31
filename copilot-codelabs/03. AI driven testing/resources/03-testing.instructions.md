# Testing Guidelines

## 1. Testing Philosophy (Applies to All Tests)

- Tests are **documentation of expected behavior**
- Tests should be **readable, maintainable, and deterministic**
- Tests should be **fast** and **reliable**
- Tests should provide **business value**, not just coverage metrics

---

## 2. Test Design Principles (Applies to All Tests)

### 2.1 Test Structure
- Use **AAA (Arrange–Act–Assert)** or **Given–When–Then**
- One test method per **scenario**
- Test names must clearly describe intent  
  **Format:** `test[Method]_[Scenario]_[ExpectedResult]`
- Extract shared setup to `@BeforeEach` **only when it improves clarity**

### 2.2 Test Quality
- Tests must be **independent**
- Tests must be **deterministic**
  - No random data
  - No reliance on system time or execution order
- Tests must be **fast**
  - Mock slow or external operations
- Tests must be **readable**
- Tests must be **maintainable** (DRY, builders, helpers)

---

## 3. Test Coverage Guidelines

### 3.1 Coverage Targets
- Aim for **70%+ overall coverage**
- **100% coverage** for critical business logic

### 3.2 Coverage Expectations
- Success and failure paths
- Validation logic
- Edge cases and boundary conditions
- Null, empty, and invalid inputs

---

## 4. Unit Testing Guidelines

### 4.1 Purpose of Unit Tests
Unit tests validate:
- Business logic
- Domain rules
- Decision-making code

They do **not** validate:
- HTTP contracts
- Database/network integration
- Framework behavior

---

### 4.2 Tooling & Frameworks
- Test frameworks: `xUnit` or `NUnit`
- Mocking: `Moq` or `NSubstitute`
- Dependency injection via `ServiceProvider` where required
- Use `ITestOutputHelper` for diagnostics and debugging

---

### 4.3 File & Directory Conventions (Unit Tests)

```
Controllers/
  OwnersController.cs

tests/
  unit/
    PetClinicRest.Tests
    PetClinicRest.Tests.csproj
      Controllers/
        OwnersControllerTests.cs
```

Guidelines:
- One test class per production class
- Test class name: `[ClassUnderTest]Tests`
- Group tests per method using nested classes or regions (optional)
- Avoid catch-all test classes

---

### 4.4 Unit Test Structure

```csharp
[Fact]
public void testCreateOwner_WithValidInput_ReturnsOwnerId()
{
    // Arrange
    var service = CreateService();
    var request = OwnerRequestBuilder.Valid();

    // Act
    var result = service.CreateOwner(request);

    // Assert
    result.Should().BeGreaterThan(0);
}
```

Rules:
- **Arrange:** setup objects, mocks, test data
- **Act:** invoke a single action
- **Assert:** verify observable behavior
- Prefer outcome-based assertions over implementation details

---

### 4.5 Dependencies & Mocking
- Mock all **external dependencies**
  - Repositories
  - HTTP clients
  - Message buses
  - File systems
- Do **not** mock:
  - Value objects
  - DTOs
  - Code under test
- Avoid over-mocking

---

### 4.6 Test Data (Unit Tests)
- Use **test data builders** for complex objects
- Use meaningful values that express intent
- Keep data local unless reuse is intentional

---

### 4.7 Running Unit Tests

Run all unit tests:
```bash
dotnet test tests/unit/PetClinicRest.Tests/PetClinicRest.Tests.csproj
```

Run with detailed output:
```bash
dotnet test tests/unit/PetClinicRest.Tests/PetClinicRest.Tests.csproj --logger "console;verbosity=detailed"
```

Run a specific test:
```bash
dotnet test tests/unit/PetClinicRest.Tests/PetClinicRest.Tests.csproj --filter "FullyQualifiedName~OwnerControllerTests"
```

---

## 5. Integration Testing Guidelines

Integration tests validate:
- HTTP contracts
- Serialization/deserialization
- Status codes
- Persistence and application wiring

They intentionally avoid deep business logic validation.

---

## 6. Integration Testing with Hurl

### 6.1 File & Directory Conventions (Integration Tests)

```
tests/
  hurl/
    owners.hurl
    pets.hurl
    vets.hurl
```

Guidelines:
- One `.hurl` file per resource/controller
- Group related scenarios in a single file
- Each scenario must be documented with a comment

---

### 6.2 Integration Test Structure (Hurl)

```hurl
# [Scenario description]
[HTTP METHOD] [URL]
[Optional headers]
[Optional body]

HTTP [expected status]
[Assertions]
```

---

### 6.3 Running Integration Tests

Run a single file:
```bash
hurl tests/hurl/owners.hurl
```

Run all integration tests:
```bash
hurl tests/hurl/*.hurl
```

Run with variables:
```bash
hurl --verbose --variable base_url=http://localhost:5000 tests/hurl/owners.hurl
```

---

### 6.4 Integration Test Coverage Expectations
- At least one happy-path test per endpoint
- Common error cases (`400`, `404`)
- Create → Retrieve workflows
- Do not duplicate unit test assertions

---

## 7. Review Checklists

### 7.1 Unit Test Review Checklist
- [ ] File structure mirrors production code
- [ ] Tests follow AAA or Given–When–Then
- [ ] Test names describe behavior
- [ ] Success and failure paths covered
- [ ] Mocks are minimal and appropriate
- [ ] Assertions validate behavior

### 7.2 Integration Test Review Checklist
- [ ] One resource per `.hurl` file
- [ ] Scenarios documented
- [ ] Status codes asserted
- [ ] `jsonpath` used where applicable
- [ ] Base URL uses variables
- [ ] Tests run against a local instance

---

## 8. Common Issues to Watch For

- Flaky tests
- Slow execution
- Over-mocking
- Test interdependencies
- Hard-coded values
- Missing edge cases
- Poorly named tests

---

## 9. Best Practices

- Write tests alongside production code
- Treat test code as production code
- Refactor tests proactively
- Keep tests focused and intention-revealing
- Document non-obvious scenarios
