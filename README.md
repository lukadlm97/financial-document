# Financial Document

## Description
This project is a .NET 8 application utilizing a vertical slice architecture combined with onion architecture principles. It is designed to manage and manipulate data related to tenants, clients, companies, products, and product properties. The application focuses on abstracting the data handling and validation processes while maintaining flexibility in how data sources are managed.

## Architecture
The project employs a vertical sliced architecture with onion architecture principles. The core idea is to define stable domains and application logic while building layers around these to provide abstraction:

- **Domain Layer**: Contains core business rules and domain logic.
- **Application Layer**: Uses MediatR for handling features and business processes, such as the `GetDocument` class for processing logic.
- **Infrastructure Layer**: Implements services and data sources(Persistance Layer). Currently uses an in-memory database, but can be easily migrated to SQL or other data sources.

The application leverages Minimal API for simplicity, while MediatR decouples the endpoint layer from the service layer. Validation is managed using FluentValidation, and data anonymization is performed with Regex.

## Features
- **Domain and Application Logic**: Encapsulated in a vertical slice approach with MediatR for handling feature-specific logic.
- **Data Handling**: Supports in-memory database with potential for easy migration to SQL or other storage solutions.
- **Validation and Anonymization**: Utilizes FluentValidation and Regex for validating inputs and anonymizing data.
- **Testing**: Includes unit tests using NUnit and Moq.

## Technology and Concepts
- **.NET 8**: The primary framework used for development.
- **Entity Framework**: For data access and ORM.
- **MediatR**: For implementing CQRS and decoupling.
- **FluentValidation**: For validation logic
- **Moq**: For mocking dependencies.
- **Minimal API**: For lightweight API implementation.
- **Onion Architecture**: For maintaining a clear separation of concerns.

## Highlites
- **Validation & Performance**: Mediator behavior is used for validation & performance measurement.
- **Flexible Repository**: The repository pattern is built around an in-memory database, allowing for easy changes to other data sources via Dependency Injection.
- **Exception Handling**: Business rules are enforced with exceptions, though a result pattern is generally preferred for error handling.

## Additional Information
- **GitHub Actions**: CI/CD workflows are set up to build and test the code automatically. Check the Actions tab in this repository for details.

## Prerequisites
- .NET 8.0

## Test cases
- happy path: ProductA, guid, guid
- product not enabled: ProductC, guid, guid
- product not exist: ProductD, guid, guid
- tenant not whitelisted: ProductA, guid4, guid
- client not exist: ProductA, guid2, guid3
- company size is small: ProducA, guid2, guid2
