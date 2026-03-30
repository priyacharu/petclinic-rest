# Exercise 1: Create a Custom tester instruction file

## Objective
Create a repository-level custom instruction file for automated testing guidance.

## Why this matters
Custom instruction files help standardize AI test generation across teams, making tests more predictable, robust, and aligned with project standards.

## What you will build
- A new custom instruction markdown file stored at: `.github/instructions/testing.instructions.md`
- An exercise walkthrough that encodes the **Testing Guidelines** below

## Exercise steps

1. Create or verify the folder path:
   - `.github/instructions/`

2. Create the file:
   - `.github/instructions/testing.instructions.md`

3. Copy the following policy into that file (or use it as a template):

```markdown
# Testing Guidelines

## Testing Philosophy

- Tests are documentation of expected behavior
- Tests should be readable and maintainable
- Tests should be fast and deterministic
- Tests should provide value, not just coverage numbers

## Test Writing Guidelines

### Test Structure
- Always use AAA (Arrange-Act-Assert) or Given-When-Then pattern
- One test method per scenario
- Clear, descriptive test method names: `test[Method]_[Scenario]_[ExpectedResult]`
- Extract common setup to `@BeforeEach` when appropriate

### Test Coverage
- Aim for 70%+ overall coverage
- 100% coverage for critical business logic
- Cover both success and error scenarios
- Include edge cases and boundary conditions
- Test validation logic thoroughly

### Test Quality
- Tests should be independent (no test dependencies)
- Tests should be deterministic (no random data, no timing dependencies)
- Tests should be fast (mock slow operations)
- Tests should be readable (clear names, good structure)
- Tests should be maintainable (DRY principle, use builders)

## Testing Patterns

### Unit Tests
- Use `xUnit` or `NUnit` with `Moq` (C#)
- Mock external dependencies with `Mock<T>` or `NSubstitute`
- Test business logic, not framework code
- Use `ServiceProvider` / `ITestOutputHelper` for dependency injection and test output handling

### Integration Tests
- Use `WebApplicationFactory<T>` / `Microsoft.AspNetCore.Mvc.Testing` (NET 6/7/8)
- Use `TestServer` / `HttpClient` for REST endpoint testing in ASP.NET Core
- Use `[TestMethod]` with transaction rollback patterns or `Respawn` for database isolation
- Use `TestServer` + `AddAuthentication` / `WebApplicationFactory` with fake principal for authentication testing

### Test Data
- Use test data builders for complex objects
- Extract common test data to `@BeforeEach`
- Use meaningful test data (not just "test", "test123")
- Consider using Testcontainers for integration tests

## Test Review Checklist

When reviewing tests, check:
- [ ] Tests follow AAA or Given-When-Then pattern
- [ ] Test names are descriptive
- [ ] Tests cover success and error scenarios
- [ ] Edge cases are covered
- [ ] Tests are independent and deterministic
- [ ] Tests are fast (mocked slow operations)
- [ ] Test data is appropriate
- [ ] Assertions verify correct behavior
- [ ] Mocks are used correctly
- [ ] Tests follow project standards

## Common Issues to Watch For

- **Flaky tests**: Tests that sometimes pass, sometimes fail
- **Slow tests**: Tests that take too long to run
- **Over-mocking**: Mocking things that shouldn't be mocked
- **Test interdependencies**: Tests that depend on other tests
- **Hard-coded values**: Magic numbers or strings without explanation
- **Missing edge cases**: Not testing null, empty, boundary values
- **Poor test names**: Vague names that don't describe the scenario

## Best Practices

- Write tests before or alongside code (TDD/BDD)
- Review tests as carefully as production code
- Refactor tests when they become hard to maintain
- Keep tests simple and focused
- Use meaningful assertions
- Document complex test scenarios
```

4. Verify Copilot references the repo-level testing instructions

   - Open the Copilot chat or assistant integrated in your editor.
   - Ask the question: "what are the testing guidelines for this project"
   - Confirm the assistant's response includes key headings or content from the file you created, for example: "Testing Philosophy", "Test Writing Guidelines", "Test Quality", or example test snippets.
   - If the assistant does not reference the repo-level instructions, ensure the file `.github/instructions/testing.instructions.md` exists and contains the guidance, then retry the query.

## Validation
- Confirm file exists and content is correct