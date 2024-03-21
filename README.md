# HealthCareSystem - CQRS infrastructure in .NET

## Description

This project was started just to practice a CQRS architecture. There is no business logic (just some CRUD operations). This project contains multiple technologies and architecture patterns.

## Languages and Utilities Used

- C#
- ASP.NET Core Api
- MediatR
- Ef core
- RabbitMQ.Client
- FluentValidation
- MimeKit

## Environment

- The project was developed on macOS Ventura(V13.0)
- Should work on any other distros.
- You will need a database running (I used postgress) and a RabbitMQ instance.

## Implementation Details

- The project has 2 APIs, names WebApi and Notifications.
- **WebApi** is the API responsible for the queries and commands in our application, while **Notification** has backgroundJobs for sending emails when an action is perfomed.
- The application uses Mapperly for mapping the objects between layers.
- Users have multiple roles. This is handled in [ShouldHaveRoleAttribute.cs](https://github.com/CirsteanPaul/HealthCareSystem/blob/main/WebApi/Infrastructure/ShouldHaveRoleAttribute.cs)
- All controllers has a base controller [ApiController](https://github.com/CirsteanPaul/HealthCareSystem/blob/main/WebApi/Infrastructure/ApiController.cs)
- Errors are parsed with the help of an [Result object](https://github.com/CirsteanPaul/HealthCareSystem/blob/main/Healthcare.Domain/Shared/Results/Result.cs)
- In the instracture lalyer we have an [email service](https://github.com/CirsteanPaul/HealthCareSystem/blob/main/Healthcare.Infrastructure/Emails/EmailSmtp.cs)

## Project board

Refer to the [board](https://github.com/CirsteanPaul/HealthCareSystem/projects?query=is%3Aopen).
