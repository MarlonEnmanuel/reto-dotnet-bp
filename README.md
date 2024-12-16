# Prueba técnica de backend con .NET 8
Proyecto creado con .NET 8 y SQL Server para administrar clientes, cuentas bancarias y movimientos de dinero.

## Características

Se crearon dos microservicios
- `ClientsApi` para clientes
- `AccountsApi` para cuentas y movimientos
  
Se aplicó los siguientes patrones de diseño
- Repository
- UnitOfWork
- DependencyInyection
- Dtos

Se utilizó Clean Architecture, cada web api tiene la siguiente estructura
```
- Application/
  - Dtos/         -> Clases las entradas y salidas de los endpoints
  - Interfaces/   -> Contratos de la capa de aplicación
  - Services/     -> Servicios de lógica de aplicaciónm
- Domain/
  - Entities/     -> Entidades de dominio y lógica de dominio
  - Validators/   -> Validación de los campos de las entidades
- Infrastructure/
  - Controllers/  -> Endpoints de entrada al api
  - Database/     -> DbContext y configuración de entidades
  - Interfaces/   -> Contratos de la capa de infra
  - Repositories/ -> Repositorios y unidad de trabajo
```

Se implementó algunas pruebas
- `AccountsApi.Tests AccountsControllerTests` contiene pruebas de integración
- `AccountsApi.Tests AccountsServiceTests` contiene pruebas unitarias

Librería utilizadas
- Para los WebApi
  - EntityFrameworkCore
  - EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
  - FluentValidation
- Para testing
  - xUnit
  - Moq
  - FluentAssertions
  - Microsoft.AspNetCore.Mvc.Testing
  - EntityFrameworkCore.InMemory
