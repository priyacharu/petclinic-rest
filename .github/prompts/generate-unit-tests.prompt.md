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
