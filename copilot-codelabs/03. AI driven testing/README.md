# AI Driven Testing

This folder contains codelab exercises for integrating AI into test generation and validation workflows.

## Exercises

1. [Create a custom instruction file for Testing guidelines](01-create-custom-instruction.md)

> The exercise includes creating `.github/instructions/testing.instructions.md` with project-wide AI test-writing rules and validation checkpoints.

2. [Generate a custom prompt with `/create-prompt` to produce unit tests](02-create-unit-test-prompt.md)

> The exercise includes building and tuning a reusable prompt for test generation, plus an experiment log.

## Bonus Exercises

3. [Update custom instructions to include integration tests with Hurl](03-update-custom-instruction.md)

> Extends `.github/instructions/testing.instructions.md` with an **Integration Testing with Hurl** section covering file conventions, Hurl entry structure, example tests for the PetClinic REST API, and CI usage patterns.

4. [Generate a custom prompt with `/create-prompt` to produce Hurl integration tests](04-create-integration-test-prompt.md)

> The exercise includes building a reusable `/generate-integration-test` prompt that produces `.hurl` test files covering happy-path, not-found, invalid-input, and create-then-retrieve scenarios.
