# Plan: Integrar MagnoliaWebAPI10 con capa de datos MagnoliaDB

## TL;DR

Crear una WebAPI completamente funcional que exponga todos los CRUD de las 6 entidades (Categorias, Prendas, Inventarios, Clientes, Rentas, DetallesRenta) mediante endpoints REST. Configurar inyección de dependencias para registrar DbContext con soporte para ConnectionString personalizado y factory por defecto.

## Arquitectura propuesta

```
MagnoliaWebAPI10/
├── Program.cs                          (DI + configuración)
├── appsettings.json                    (conexión por defecto)
├── appsettings.Development.json        (override desarrollo)
├── Controllers/
│   ├── CategoriasController.cs         (CRUD endpoints)
│   ├── PrendasController.cs
│   ├── InventariosController.cs
│   ├── ClientesController.cs
│   ├── RentasController.cs
│   └── DetallesRentaController.cs
├── DTOs/
│   ├── CategoriaDTO.cs
│   ├── PrendaDTO.cs
│   ├── InventarioDTO.cs
│   ├── ClienteDTO.cs
│   ├── RentaDTO.cs
│   └── DetalleRentaDTO.cs
└── Mappers/
    └── MappingExtensions.cs            (Entity ↔ DTO)
```

## Steps

1. **Configurar DbContext en DI** — Registrar `SqliteDbContext` en `Program.cs` con opciones para leer `ConnectionString` de `appsettings.json` con factory como fallback

2. **Registrar servicios de negocio** — Registrar los 6 services (CategoriaService, PrendaService, InventarioService, ClienteService, RentaService, DetalleRentaService) como scoped

3. **Crear DTOs** — DTOs para cada entidad con propiedades esenciales y validaciones básicas (DataAnnotations)

4. **Crear mappers** — Extension methods para conversión Entity ↔ DTO bidireccional

5. **Implementar 6 Controllers** — Un controlador por entidad con endpoints REST estándar:
   - `GET /api/[entidad]` — obtener todos
   - `GET /api/[entidad]/{id}` — obtener por ID
   - `POST /api/[entidad]` — crear
   - `PUT /api/[entidad]/{id}` — actualizar
   - `DELETE /api/[entidad]/{id}` — eliminar
   - Endpoints específicos según métodos de cada servicio

6. **Configurar appsettings.json** — Agregar `ConnectionStrings` con valor por defecto (factory)

7. **Verificar documentación API** — Swagger/Scalar auto-generan docs para todos los endpoints

## Further Considerations

1. **Paginación** — ¿offset/limit en GetAll? Recomiendo no ahora, agregar después si datos crecen.

2. **Manejo de errores** — ¿GlobalExceptionHandler o Try-Catch en controllers? Recomiendo GlobalExceptionHandler middleware.

3. **Validación DTOs** — ¿DataAnnotations o FluentValidation? Recomiendo DataAnnotations (más simple).

4. **CORS** — ¿Todo (* ) o dominios específicos? Recomiendo específico si conoces frontend URLs.

5. **Versionado API** — ¿URLs versionadas o sin versioning? Recomiendo sin versioning inicialmente.

6. **Autenticación** — ¿JWT/Auth0 desde inicio o después? Recomiendo sin autenticación inicialmente.

7. **Response envelope** — ¿Entidades directas o envueltas en {data, success, message}? Recomiendo directo.

8. **Soft Delete** — ¿Borrado lógico (IsDeleted) o físico? Recomiendo físico por ahora.
