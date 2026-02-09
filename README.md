# User Management Application

A full-stack user management system built with modern technologies, featuring a clean architecture backend and a responsive React frontend.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Environment Variables](#environment-variables)
- [Development](#development)
- [Deployment](#deployment)

## 🎯 Overview

This application provides a comprehensive user management system with features for creating, reading, updating, and managing user accounts. It demonstrates best practices in software architecture, including Clean Architecture, CQRS pattern, and modern React development.

## ✨ Features

### User Management
- ✅ **CRUD Operations**: Create, Read, Update users
- 🔍 **Search & Filter**: Real-time search functionality
- 📄 **Pagination**: Efficient data loading with customizable page sizes
- 🎚️ **Status Toggle**: Activate/Deactivate user accounts
- 🔔 **Toast Notifications**: Real-time feedback for all operations
- ✅ **Form Validation**: Client and server-side validation

### Technical Features
- 🏗️ **Clean Architecture**: Separation of concerns across API, Application, Domain, and Infrastructure layers
- 📨 **CQRS Pattern**: Using MediatR for command and query separation
- 🎨 **Responsive Design**: Mobile-first UI with TailwindCSS
- 🐳 **Docker Support**: Full containerization with Docker Compose
- 🔄 **Auto Migrations**: Database migrations applied automatically on startup
- 🛡️ **Error Handling**: Global exception middleware with custom exceptions

## 🛠️ Tech Stack

### Backend
- **Framework**: ASP.NET Core 10.0
- **Database**: SQL Server 2022
- **ORM**: Entity Framework Core 10.0
- **Patterns**: CQRS with MediatR
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **API Documentation**: Swagger/OpenAPI

### Frontend
- **Framework**: React 19.2
- **Language**: TypeScript 5.9
- **Build Tool**: Vite 7.2
- **Styling**: TailwindCSS 4.1
- **State Management**: TanStack Query (React Query) 5.90
- **Form Handling**: React Hook Form 7.71
- **Validation**: Zod 4.3
- **HTTP Client**: Axios 1.13
- **Notifications**: React Hot Toast 2.6
- **Icons**: Lucide React

### DevOps
- **Containerization**: Docker & Docker Compose
- **Database**: SQL Server (Docker container)
- **Reverse Proxy**: Nginx (for frontend)

## 🏛️ Architecture

### Backend Architecture (Clean Architecture)

```
src/
├── UserManagementApp.API/           # Presentation Layer
│   ├── Controllers/                 # API Controllers
│   └── Middlewares/                 # Exception handling
│
├── UserManagementApp.Application/   # Application Layer
│   ├── Features/                    # CQRS Features
│   │   └── Users/
│   │       ├── Commands/            # Write operations
│   │       ├── Queries/             # Read operations
│   │       ├── DTOs/                # Data Transfer Objects
│   │       └── Models/              # View models
│   ├── Common/
│   │   ├── Behaviors/              # MediatR Pipeline behaviors
│   │   └── Mappings/               # AutoMapper profiles
│   └── ViewModels/                 # Response models
│
├── UserManagementApp.Domain/        # Domain Layer
│   ├── Entities/                    # Domain entities
│   ├── Enums/                       # Domain enumerations
│   ├── Exceptions/                  # Domain exceptions
│   └── Repositories/                # Repository interfaces
│
└── UserManagementApp.Infrastructure/ # Infrastructure Layer
    ├── DatabaseContext/             # EF Core DbContext
    ├── Configurations/              # Entity configurations
    ├── Migrations/                  # Database migrations
    └── Repositories/                # Repository implementations
```

### Frontend Architecture

```
src/
├── api/                             # API client configuration
│   └── axiosClient.ts              # Axios instance with interceptors
│
├── features/                        # Feature-based modules
│   └── users/
│       ├── components/             # User-related components
│       ├── hooks/                  # Custom React hooks
│       ├── services/               # API service layer
│       └── types/                  # TypeScript types
│
├── components/                      # Shared/reusable components
│   └── Table/                      # Generic table component
│
└── types/                          # Global TypeScript types
```

## 🚀 Getting Started

### Prerequisites

- Docker Desktop
- Docker Compose
- (Optional) Node.js 18+ and .NET 10 SDK for local development

### Quick Start with Docker

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd user-management
   ```

2. **Start all services**
   ```bash
   docker-compose up --build
   ```

3. **Access the application**
   - Frontend: http://localhost:3000
   - API: http://localhost:8080
   - Swagger UI: http://localhost:8080/swagger

### Local Development Setup

#### Backend Setup

1. **Navigate to API project**
   ```bash
   cd src/UserManagementApp.API
   ```

2. **Update appsettings.Development.json**
   ```json
   {
     "ConnectionStrings": {
       "UserManagement": "Server=localhost,1434;Database=UserDb;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;"
     },
     "AllowedClient": {
       "ClientUri": "http://localhost:5173"
     }
   }
   ```

3. **Run the API**
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

#### Frontend Setup

1. **Navigate to client project**
   ```bash
   cd src/UserManagementApp.Client
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Create .env file**
   ```env
   VITE_API_BASE_URL=https://localhost:7117/api
   VITE_API_TIMEOUT=30000
   ```

4. **Start development server**
   ```bash
   npm run dev
   ```

## 📁 Project Structure

```
user-management/
├── docker-compose.yml               # Docker orchestration
├── UserManagementApp.slnx          # Solution file
│
└── src/
    ├── UserManagementApp.API/       # ASP.NET Core Web API
    ├── UserManagementApp.Application/# Business logic layer
    ├── UserManagementApp.Domain/    # Domain models
    ├── UserManagementApp.Infrastructure/# Data access layer
    └── UserManagementApp.Client/    # React frontend
```

## 🔌 API Endpoints

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/users` | Get paginated users list |
| GET | `/api/users/{id}` | Get user by ID |
| POST | `/api/users` | Create new user |
| PUT | `/api/users/{id}` | Update user |
| PATCH | `/api/users/{id}/status` | Toggle user active status |

### Query Parameters (GET /api/users)

- `pageNumber` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)
- `searchKey` - Search term for filtering

### Request/Response Examples

#### Create User
```json
POST /api/users
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "role": "User",
  "isActive": true
}
```

#### Response Format
```json
{
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "fullName": "John Doe",
    "email": "john@example.com",
    "role": "User",
    "isActive": true
  }
}
```

## 🔧 Environment Variables

### Backend (appsettings.json)

```json
{
  "ConnectionStrings": {
    "UserManagement": "Server=sqlserver;Database=UserDb;..."
  },
  "AllowedClient": {
    "ClientUri": "http://localhost:3000"
  }
}
```

### Frontend (.env)

```env
VITE_API_BASE_URL=http://localhost:8080/api
VITE_API_TIMEOUT=30000
```

## 💻 Development

### Backend Commands

```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run API
dotnet run --project src/UserManagementApp.API

# Create migration
dotnet ef migrations add MigrationName --project src/UserManagementApp.Infrastructure --startup-project src/UserManagementApp.API

# Update database
dotnet ef database update --project src/UserManagementApp.Infrastructure --startup-project src/UserManagementApp.API
```

### Frontend Commands

```bash
# Install dependencies
npm install

# Start dev server
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview

# Lint code
npm run lint
```

## 🐳 Docker Deployment

### Services Configuration

The `docker-compose.yml` defines three services:

1. **sqlserver**: SQL Server 2022 database
   - Port: 1434:1433
   - Health check enabled
   - Persistent volume for data

2. **api**: ASP.NET Core API
   - Port: 8080:80
   - Depends on: sqlserver
   - Auto-applies migrations

3. **client**: React frontend
   - Port: 3000:80
   - Nginx server
   - Depends on: api

### Docker Commands

```bash
# Build and start all services
docker-compose up --build

# Start in detached mode
docker-compose up -d

# Stop all services
docker-compose down

# View logs
docker-compose logs -f

# Rebuild specific service
docker-compose up --build api
```

## 📚 Key Libraries & Patterns

### Backend Patterns

- **Clean Architecture**: Separation of concerns across layers
- **CQRS**: Command Query Responsibility Segregation with MediatR
- **Repository Pattern**: Abstraction over data access
- **Validation Pipeline**: FluentValidation with MediatR behaviors
- **Exception Handling**: Global middleware for consistent error responses

### Frontend Patterns

- **Feature-First Structure**: Organized by features rather than file types
- **Custom Hooks**: Encapsulating business logic (useUsers)
- **Type Safety**: Strong typing with TypeScript and Zod
- **Optimistic Updates**: React Query for efficient data synchronization
- **Component Composition**: Reusable DataTable component

## 🎨 UI Components

- **UserTable**: Main user listing with pagination and search
- **UserForm**: Modal form for creating/editing users
- **DataTable**: Generic, reusable table component with sorting
- **StatusBadge**: Visual indicator for user active status
- **Toast Notifications**: Feedback for all CRUD operations

## 🔒 Validation

### Backend Validation
- FluentValidation for command validation
- Email format validation
- Required field validation
- Custom business rule validation

### Frontend Validation
- Zod schema validation
- React Hook Form integration
- Real-time error display
- Email format validation

## 🐛 Error Handling

### Backend
- Global exception middleware
- Custom exception types
- Consistent error response format
- Detailed error logging

### Frontend
- Axios interceptors
- Toast notifications for errors
- Network error handling
- User-friendly error messages

## 📝 License

This project is for educational and demonstration purposes.

## 👥 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Contact

For questions or feedback, please open an issue in the repository.

---

Built with ❤️ using ASP.NET Core and React
