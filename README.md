# IT30.Todo.Microservices

This repository contains the code and documentation for a microservices-based Todo application, developed as part of the 2024 iThome Ironman Challenge. The challenge involves publishing 30 consecutive articles over 30 days on various software development topics. This project focuses on the topic **"DDD? Clean Architecture? Microservices? Let's Build a Modern Microservice with .NET!"**, exploring the design and implementation of a modern microservices architecture using Domain-Driven Design (DDD) and Clean Architecture principles.

## Project Overview

The project demonstrates the development of a Todo application using a microservices architecture. It covers various aspects of microservices design, implementation, and deployment, including:

- Domain-Driven Design (DDD)
- Clean Architecture
- Event Sourcing
- API Gateway
- Authentication and Authorization
- gRPC communication
- GraphQL for data querying
- Message queues for inter-service communication

## Key Components

![https://ithelp.ithome.com.tw/upload/images/20241013/20168953UCDKozGIbp.png](https://ithelp.ithome.com.tw/upload/images/20241013/20168953UCDKozGIbp.png)

## Technologies Used

- .NET 8
- Entity Framework Core
- gRPC
- GraphQL
- RabbitMQ
- SQL Server
- Docker

## Getting Started

To run this project locally, follow these steps:

- Ensure you have Docker and Docker Compose installed.

- Clone this repository to your local machine.

- In the project root directory, run the following command to start all services:

```bash
docker compose up -d
```

- Wait for all containers to start. You can check the container status using:

```bash
docker compose ps
```

- Access the web application in your browser at `http://localhost`.

![https://ithelp.ithome.com.tw/upload/images/20241013/20168953BNX2SPyWoA.png](https://ithelp.ithome.com.tw/upload/images/20241013/20168953BNX2SPyWoA.png)

![https://ithelp.ithome.com.tw/upload/images/20241013/20168953W0RBFE4GvM.png](https://ithelp.ithome.com.tw/upload/images/20241013/20168953W0RBFE4GvM.png)

- To stop all services, run:

```bash
docker compose down
```

- If you make any changes to the .NET projects, rebuild and restart the services using:

```bash
docker compose up -d --build
```

## Documentation

For detailed explanations of the architecture, design decisions, and implementation details, please refer to the series of articles published as part of the iThome Ironman Challenge:

> [DDD? Clean Architecture? Microservices? 帶你用.NET實作打造一個現代化微服務！ 系列](https://ithelp.ithome.com.tw/users/20168953/ironman/7881)

You can refer to the article files in `doc/pages/` folder as well.

## Acknowledgements

This project was developed as part of the 2024 iThome Ironman Challenge. Special thanks to iThome for organizing the event and providing a platform for sharing knowledge and experiences in software development.
