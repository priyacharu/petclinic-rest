# PetClinic REST API - .NET Version

A REST API for managing a veterinary clinic, built with ASP.NET Core and Entity Framework Core. This is a .NET implementation based on the [Spring PetClinic REST](https://github.com/spring-petclinic/spring-petclinic-rest) project.

## Features

- **Owners Management**: Create, read, update, and delete pet owners
- **Pets Management**: Manage pets with associated owners and pet types
- **Veterinarians**: Manage veterinarians and their specialties
- **Visits**: Schedule and track veterinary visits
- **Pet Types**: Manage pet type categories (Dog, Cat, etc.)
- **Specialties**: Manage veterinarian specialties (Surgery, Radiology, etc.)
- **Auto-generated API Documentation**: Swagger/OpenAPI UI included
- **Sample Data**: Database is pre-populated with sample data on first run

## Prerequisites

- .NET 8.0 SDK or later
- SQLite (included with .NET)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-repo/petclinic-rest.git
cd petclinic-rest
```

### 2. Build the Project

```bash
dotnet build
```

### 3. Run the Application

```bash
dotnet run
```

The application will start on `https://localhost:5001` (or `http://localhost:5000` for development).

### 4. Access the API

- **Swagger UI**: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)
- **API Base URL**: `https://localhost:5001/api`

## API Endpoints

### Owners

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/owners` | Get all owners |
| GET | `/api/owners/{id}` | Get owner by ID |
| POST | `/api/owners` | Create a new owner |
| PUT | `/api/owners/{id}` | Update an owner |
| DELETE | `/api/owners/{id}` | Delete an owner |
| GET | `/api/owners/{ownerId}/pets/{petId}` | Get a pet by owner and pet ID |
| POST | `/api/owners/{ownerId}/pets` | Add a pet to an owner |
| PUT | `/api/owners/{ownerId}/pets/{petId}` | Update an owner's pet |

### Pets

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/pets` | Get all pets |
| GET | `/api/pets/{id}` | Get pet by ID |
| POST | `/api/pets` | Create a new pet |
| PUT | `/api/pets/{id}` | Update a pet |
| DELETE | `/api/pets/{id}` | Delete a pet |

### Veterinarians

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/vets` | Get all veterinarians |
| GET | `/api/vets/{id}` | Get veterinarian by ID |
| POST | `/api/vets` | Create a new veterinarian |
| PUT | `/api/vets/{id}` | Update a veterinarian |
| DELETE | `/api/vets/{id}` | Delete a veterinarian |

### Specialties

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/specialties` | Get all specialties |
| GET | `/api/specialties/{id}` | Get specialty by ID |
| POST | `/api/specialties` | Create a new specialty |
| PUT | `/api/specialties/{id}` | Update a specialty |
| DELETE | `/api/specialties/{id}` | Delete a specialty |

### Pet Types

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/pettypes` | Get all pet types |
| GET | `/api/pettypes/{id}` | Get pet type by ID |
| POST | `/api/pettypes` | Create a new pet type |
| PUT | `/api/pettypes/{id}` | Update a pet type |
| DELETE | `/api/pettypes/{id}` | Delete a pet type |

### Visits

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/visits` | Get all visits |
| GET | `/api/visits/{id}` | Get visit by ID |
| POST | `/api/visits` | Create a new visit |
| POST | `/api/visits/{petId}/visits` | Create a visit for a specific pet |
| PUT | `/api/visits/{id}` | Update a visit |
| DELETE | `/api/visits/{id}` | Delete a visit |

## Project Structure

```
PetClinicRest/
├── Controllers/          # API Controllers
│   ├── OwnersController.cs
│   ├── PetsController.cs
│   ├── VetsController.cs
│   ├── SpecialtiesController.cs
│   ├── PetTypesController.cs
│   └── VisitsController.cs
├── Models/              # Entity Models
│   ├── Owner.cs
│   ├── Pet.cs
│   ├── PetType.cs
│   ├── Vet.cs
│   ├── Specialty.cs
│   ├── VetSpecialty.cs
│   └── Visit.cs
├── DTOs/                # Data Transfer Objects
│   ├── OwnerDto.cs
│   ├── PetDto.cs
│   ├── VetDto.cs
│   ├── SpecialtyDto.cs
│   ├── PetTypeDto.cs
│   └── VisitDto.cs
├── Data/                # Database Context & Initialization
│   ├── PetClinicDbContext.cs
│   └── DbInitializer.cs
├── Mappings/            # AutoMapper Configuration
│   └── MappingProfile.cs
├── Program.cs           # Application Entry Point
├── appsettings.json     # Configuration
└── PetClinicRest.csproj # Project File
```

## Database

The application uses SQLite by default for simplicity. The database is automatically created and pre-populated with sample data on the first run.

### Switching to Another Database

To use SQL Server or another database provider:

1. Install the appropriate NuGet package:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   ```

2. Update `Program.cs`:
   ```csharp
   builder.Services.AddDbContext<PetClinicDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
   ```

3. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=petclinic;User Id=sa;Password=your-password;"
   }
   ```

## Sample Data

The database is pre-populated with sample data including:
- 5 Pet Types (Dog, Cat, Hamster, Rabbit, Bird)
- 3 Specialties (Radiology, Surgery, Dentistry)
- 5 Veterinarians
- 10 Pet Owners
- 10 Pets
- 3 Sample Visits

## Development

### Running in Development Mode

```bash
dotnet watch run
```

This will automatically rebuild and restart the application when code changes are detected.

### Building for Production

```bash
dotnet publish -c Release
```

## Technologies Used

- **ASP.NET Core 8.0**: Web framework
- **Entity Framework Core 8.0**: ORM
- **SQLite**: Default database
- **AutoMapper**: Object mapping
- **Swagger/Swashbuckle**: API documentation
- **C# 12**: Programming language

## References

- [Spring PetClinic REST](https://github.com/spring-petclinic/spring-petclinic-rest)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core)
- [AutoMapper Documentation](https://automapper.org)

## License

This project is licensed under the Apache License 2.0 - see the LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
