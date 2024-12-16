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
  - Serilog
- Para testing
  - xUnit
  - Moq
  - FluentAssertions
  - Microsoft.AspNetCore.Mvc.Testing
  - EntityFrameworkCore.InMemory

## Requisitos
- Docker Desktop [https://www.docker.com/](https://www.docker.com/)
- Postman para ejecutar peticiones
- SDK de .NET 8 para ejecución de pruebas

## Instalación
Abrir la terminar y clonar el repositorio
```
git clone https://github.com/MarlonEnmanuel/reto-dotnet-bp.git
```

Crear las BD en SQL Server para cada microservicio
  ```
  ClientsDb     -> Files\BaseDatos_ClientsDb.sql
  AccountsDb    -> Files\BaseDatos_AccountsDb.sql
  ```

## Configuración
En el archivo `docker-compose.yml` modifique las cadena de conexión según corresponda.
- `ConnectionStrings__ClientsDb`
- `ConnectionStrings__AccountsDb`


## Construir imágenes de docker
Abrir la terminar en la carpeta principal del repositorio y ejecutar
```
docker-compose buid
```

## Iniciar microservicios
Abrir la terminar en la carpeta principal del repositorio y ejecutar
```
docker-compose up
```

Se iniciarán los servicios en los puertos:
- `https://localhost:5001` -> Microservicio de clientes
- `https://localhost:5002` -> Microservicios de cuentas y movimientos


Abrir la aplicación Postman e importar el archivo provisto en el repositorio
- `Files/Reto .NET Banco.postman_collection.json`
- `Files/RetoDotNet.postman_environment.json`


## Ejecutar pruebas
Abrir una terminar en la carpeta principal del repositorio y ejecutar
```
dotnet test
```