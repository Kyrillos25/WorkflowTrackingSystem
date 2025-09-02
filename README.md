# Workflow Tracking System

A modular, microservices-based workflow management system built with .NET and Docker.

## Overview

The Workflow Tracking System is designed to manage and track workflows across different modules. It follows a clean architecture pattern with separate API projects for different domains.

## Architecture

The solution is structured into the following main components:

### API Services
- **Gateway API**: Entry point for all external requests
- **Workflow Management API**: Handles workflow definitions and instances
- **Workflow Processor API**: Processes workflow steps and automation

### Infrastructure
- **PostgreSQL Database**: Main data store
- **Docker**: Containerization and orchestration

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/Kyrillos25/WorkflowTrackingSystem.git
cd Workflow-Tracking-System
```

### Run with Docker Compose

The easiest way to run the entire system is using Docker Compose:

```bash
docker-compose up -d
```

This will start all the required services:
- Gateway API (port 80)
- Users API
- Workflow Management API
- Workflow Processor API
- PostgreSQL Database

### Access the Application

Once all services are up and running, you can access:
locate Gateway port and open browser
- **Gateway API**: http://localhost:{portnumber}/WFManagement/swagger
- **Gateway API**: http://localhost:{portnumber}/WFProcessor/swagger

## Development

### Build the Solution

```bash
dotnet build
```

### Run Tests

```bash
dotnet test
```

### Database Migrations

Database migrations are automatically applied on application startup.

## Environment Variables

Create a `.env` file in the root directory with the following variables:

```
# Database
DB_CONNECTION_STRING=Host=workflowtracking.database;Database=workflowtracking;Username=postgres;Password=postgres

# JWT Settings
JWT_SECRET=your_jwt_secret_key_here
JWT_ISSUER=WorkflowTracking
JWT_AUDIENCE=WorkflowTracking.Users
```

## Project Structure

```
├── src/
│   ├── API/                    # API projects
│   │   ├── WorkflowTracking.Gateway/
│   │   ├── WorkflowTracking.Users.API/
│   │   └── WorkflowTracking.WorkflowManagement.API/
│   │
│   ├── Common/                 # Shared components
│   │   ├── WorkflowTracking.Common.Application/
│   │   ├── WorkflowTracking.Common.Domain/
│   │   └── WorkflowTracking.Common.Infrastructure/
│   │
│   └── Modules/                # Feature modules
│       ├── Users/
│       ├── WFManagment/
│       └── WFProcessor/
│
├── docker-compose.yml          # Docker Compose configuration
└── README.md                   # This file
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
