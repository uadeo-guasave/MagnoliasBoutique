# Plan: Actualizar MagnoliaDB para control de renta de prendas

Agregar modelos de renta (Cliente, Renta, DetalleRenta) al proyecto MagnoliaDB existente, crear migraciones, establecer relaciones FK en base de datos y configurar el DbContext para uso exclusivo con inyección de dependencias en proyectos externos (MagnoliaWebAPI10).

## Steps

1. **Crear modelos de renta** — Agregar clases `Cliente.cs` (Id, Nombre, Teléfono, Email), `Renta.cs` (Id, ClienteId, FechaRenta, FechaDevolucion, Total, Estado), y `DetalleRenta.cs` (Id, RentaId, PrendaId, PrecioRenta) en `MagnoliaDB/`

2. **Integrar modelo Inventario** — Agregar `DbSet<Inventario>` a `SqliteDbContext.cs` y crear migración para tabla Inventarios con FK a Prendas

3. **Configurar relaciones en DbContext** — Implementar `OnModelCreating` en `SqliteDbContext.cs` con Fluent API para definir FKs (Cliente→Renta, Renta→DetalleRenta, Prenda→DetalleRenta, Prenda→Inventario) y constraints de cascada

4. **Crear migraciones para nuevos modelos** — Generar migraciones para Cliente, Renta y DetalleRenta con relaciones FK explícitas en base de datos

5. **Refactorizar DbContext para DI** — Eliminar constructor sin parámetros y lógica de `OnConfiguring`, dejar solo constructor con `DbContextOptions<SqliteDbContext>`, mover connection string a configuración externa para inyección desde MagnoliaWebAPI10

6. **Limpiar anti-patterns de acceso a datos** — Eliminar métodos estáticos/instancia (`ObtenerPrendas`, `ObtenerPrendaPorCodigo`) de `Prenda.cs`, mantener solo propiedades del modelo

## Further Considerations

1. **Estado de Renta** — Usar enum con conversión a string en BD para tipo seguro.
2. **Propiedades NotMapped** — Actualmente `Prendas` en `Categoria` usa `[NotMapped]`. ¿Mantener este patrón para navegación o permitir lazy loading en nuevos modelos?
3. **Inventario existente** — El modelo `Inventario` tiene `Fecha` como `DateOnly` y `Cantidad`. ¿Esto representa historial de stock o inventario actual? Recomiendo renombrar a `HistorialInventario` si es log histórico
