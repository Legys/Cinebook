# Cinebook

### Cinebook
Cinebook is a demo application developed as a technical assignment. It showcases a clean architecture with a feature slice structure, integrating MediatR for enhanced maintainability and scalability.

## Architecture
Cinebook employs a robust and scalable architecture:

Clean Architecture: Ensures separation of concerns and independence from frameworks and UI.
Feature Slice Structure: Facilitates modular development, making the codebase more intuitive and manageable.
MediatR Integration: Leverages the Mediator pattern for decoupled, scalable, and maintainable code.

## Simplifications
In this demo version:

* Authorization: Skipped for simplicity. Typically implemented with solutions like Keycloak or IdentityServer.
* Validation: Implemented for CreateMovieRequestValidator and CinemasRequestValidator to ensure data integrity. Avoided any other validation for simplicity.
* Development Conventions
* Type Safety: Prioritized to reduce runtime errors and improve code quality.
* Declarative Style: Preferred for readability and maintainability.
* Roslynator: Used for linting and enforcing a consistent code style.
* Folder Structure: Strictly followed for future feature expansions, ensuring a well-organized codebase.