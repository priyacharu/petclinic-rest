# Exercise 2: Generate a custom test-generation prompt with `/create-prompt`

## Objective
Create a reusable custom prompt `/generate-unit-test` using the `/create-prompt` command that generates unit tests for selected functions or files.

## Why this matters
Using `/generate-unit-test` helps standardize AI test generation workflow and lets developers experiment with prompt style, coverage, and scenario depth.

## What you will build
- A new prompt definition in your local workspace
- A focus on function-level or file-level test generation, with a strategy anchored by the existing `Testing Guidelines`
- Exploration steps for how to tune the prompt

## Exercise steps

1. Read the existing policy that you already stored in `.github/instructions/testing.instructions.md`.
2. Create a custom prompt using `/create-prompt` in your CoPilot chat interface. Use the following sample as starting point and save it under `.github/prompts/generate-unit-test.prompt.md`:

```markdown
---
agent: 'agent'
description: 'Generate unit tests for selected functions or files'
---
## Task
Analyze the selected function or file and generate focused xUnit unit tests that thoroughly validate its behavior, following the project's Testing Guidelines in `.github/instructions/testing.instructions.md`.

## Test Generation Strategy
1. **Core Functionality Tests**
   - Test the main purpose and expected behavior of each public method
   - Verify return values with typical, realistic inputs
   - Test with representative data scenarios reflecting actual usage

2. **Input Validation Tests**
   - Test with null or missing required values
   - Test with empty strings, empty collections, and zero values
   - Test boundary values (min/max IDs, maximum string lengths)
   - Test with invalid types or out-of-range values where applicable

3. **Error Handling Tests**
   - Verify expected exceptions are thrown with meaningful messages
   - Test that the method returns the correct error result (e.g., `NotFound`, `BadRequest`)
   - Test graceful handling of downstream/dependency failures

4. **Side Effects & Dependency Tests**
   - Mock all external dependencies with `Moq` (`Mock<T>`)
   - Verify mocked methods are called with the expected arguments (`Verify`)
   - Test that state changes (e.g., database writes) are triggered correctly
   - Validate returned DTOs map correctly from domain models

## Output Format
- Place generated tests in `tests/unit/PetClinicRest.Tests/`
- Name the test class after the class under test: `[ClassName]Tests.cs`
- Name each test method: `[MethodName]_[Scenario]_[ExpectedResult]`
- Use `xUnit` (`[Fact]` / `[Theory]`) with `Moq` for mocking
- Use `FluentAssertions` for readable assertions where available

## xUnit Test Template
```csharp
public class [ClassName]Tests
{
    private readonly Mock<[IDependency]> _mockDependency;
    private readonly [ClassName] _sut;

    public [ClassName]Tests()
    {
        _mockDependency = new Mock<[IDependency]>();
        _sut = new [ClassName](_mockDependency.Object);
    }

    [Fact]
    public async Task [MethodName]_[Scenario]_[ExpectedResult]()
    {
        // Arrange
        var input = /* set up realistic test data */;
        _mockDependency.Setup(m => m.[Method](input)).ReturnsAsync(/* expected value */);

        // Act
        var result = await _sut.[MethodName](input);

        // Assert
        Assert.NotNull(result);
        _mockDependency.Verify(m => m.[Method](input), Times.Once);
    }
}
```

Generate tests that give confidence the class works correctly in isolation and help catch regressions early.
```

3. Experiment and iterate:
   - Add or modify prompt sections
   - Try “/generate-unit-test OwnersController.cs” and “/generate-unit-test PetClinicDbContext".
   - Confirm the generated tests follow AAA/Given-When-Then, have clear names, and include success/error/edge scenarios.

---

## Validation
- The test generation example is present and executable via `/generate-unit-test`.
- Generated test code is validated against the `Testing Guidelines` in `.github/instructions/testing.instructions.md`. 