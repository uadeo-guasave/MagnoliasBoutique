using MagnoliaDB;
using MagnoliaWebAPI10.DTOs;

namespace MagnoliaWebAPI10.Mappers;

public static class MappingExtensions
{
    // Categoria Mappers
    public static CategoriaDTO ToDTO(this Categoria entity)
    {
        return new CategoriaDTO
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            EstaDisponible = entity.EstaDisponible
        };
    }

    public static Categoria ToEntity(this CategoriaCreateDTO dto)
    {
        return new Categoria
        {
            Nombre = dto.Nombre,
            EstaDisponible = dto.EstaDisponible
        };
    }

    public static void UpdateEntity(this CategoriaUpdateDTO dto, Categoria entity)
    {
        entity.Nombre = dto.Nombre;
        entity.EstaDisponible = dto.EstaDisponible;
    }

    // Prenda Mappers
    public static DTOs.PrendaDTO ToDTO(this Prenda entity)
    {
        return new DTOs.PrendaDTO
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            CodigoDeReferencia = entity.CodigoDeReferencia,
            Descripcion = entity.Descripcion,
            Talla = entity.Talla,
            Color = entity.Color,
            Material = entity.Material,
            PrecioDeRenta = entity.PrecioDeRenta,
            CategoriaId = entity.CategoriaId,
            ImagenUrl = entity.ImagenUrl
        };
    }

    public static Prenda ToEntity(this PrendaCreateDTO dto)
    {
        return new Prenda
        {
            Nombre = dto.Nombre,
            CodigoDeReferencia = dto.CodigoDeReferencia,
            Descripcion = dto.Descripcion,
            Talla = dto.Talla,
            Color = dto.Color,
            Material = dto.Material,
            PrecioDeRenta = dto.PrecioDeRenta,
            CategoriaId = dto.CategoriaId,
            ImagenUrl = dto.ImagenUrl
        };
    }

    public static void UpdateEntity(this PrendaUpdateDTO dto, Prenda entity)
    {
        entity.Nombre = dto.Nombre;
        entity.CodigoDeReferencia = dto.CodigoDeReferencia;
        entity.Descripcion = dto.Descripcion;
        entity.Talla = dto.Talla;
        entity.Color = dto.Color;
        entity.Material = dto.Material;
        entity.PrecioDeRenta = dto.PrecioDeRenta;
        entity.CategoriaId = dto.CategoriaId;
        entity.ImagenUrl = dto.ImagenUrl;
    }

    // Inventario Mappers
    public static InventarioDTO ToDTO(this Inventario entity)
    {
        return new InventarioDTO
        {
            Id = entity.Id,
            PrendaId = entity.PrendaId,
            Costo = entity.Costo,
            FechaDeAlta = entity.FechaDeAlta,
            Existencia = entity.Existencia
        };
    }

    public static Inventario ToEntity(this InventarioCreateDTO dto)
    {
        return new Inventario
        {
            PrendaId = dto.PrendaId,
            Costo = dto.Costo,
            FechaDeAlta = dto.FechaDeAlta,
            Existencia = dto.Existencia
        };
    }

    public static void UpdateEntity(this InventarioUpdateDTO dto, Inventario entity)
    {
        entity.PrendaId = dto.PrendaId;
        entity.Costo = dto.Costo;
        entity.FechaDeAlta = dto.FechaDeAlta;
        entity.Existencia = dto.Existencia;
    }

    // Cliente Mappers
    public static ClienteDTO ToDTO(this Cliente entity)
    {
        return new ClienteDTO
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Telefono = entity.Telefono,
            Email = entity.Email
        };
    }

    public static Cliente ToEntity(this ClienteCreateDTO dto)
    {
        return new Cliente
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Email = dto.Email
        };
    }

    public static void UpdateEntity(this ClienteUpdateDTO dto, Cliente entity)
    {
        entity.Nombre = dto.Nombre;
        entity.Telefono = dto.Telefono;
        entity.Email = dto.Email;
    }

    // Renta Mappers
    public static RentaDTO ToDTO(this Renta entity)
    {
        return new RentaDTO
        {
            Id = entity.Id,
            ClienteId = entity.ClienteId,
            FechaRenta = entity.FechaRenta,
            FechaDevolucion = entity.FechaDevolucion,
            Total = entity.Total,
            Estado = entity.Estado.ToString()
        };
    }

    public static Renta ToEntity(this RentaCreateDTO dto)
    {
        return new Renta
        {
            ClienteId = dto.ClienteId,
            FechaRenta = dto.FechaRenta,
            FechaDevolucion = dto.FechaDevolucion,
            Total = dto.Total,
            Estado = dto.Estado
        };
    }

    public static void UpdateEntity(this RentaUpdateDTO dto, Renta entity)
    {
        entity.ClienteId = dto.ClienteId;
        entity.FechaRenta = dto.FechaRenta;
        entity.FechaDevolucion = dto.FechaDevolucion;
        entity.Total = dto.Total;
        entity.Estado = dto.Estado;
    }

    // DetalleRenta Mappers
    public static DetalleRentaDTO ToDTO(this DetalleRenta entity)
    {
        return new DetalleRentaDTO
        {
            Id = entity.Id,
            RentaId = entity.RentaId,
            PrendaId = entity.PrendaId,
            PrecioRenta = entity.PrecioRenta
        };
    }

    public static DetalleRenta ToEntity(this DetalleRentaCreateDTO dto)
    {
        return new DetalleRenta
        {
            RentaId = dto.RentaId,
            PrendaId = dto.PrendaId,
            PrecioRenta = dto.PrecioRenta
        };
    }

    public static void UpdateEntity(this DetalleRentaUpdateDTO dto, DetalleRenta entity)
    {
        entity.RentaId = dto.RentaId;
        entity.PrendaId = dto.PrendaId;
        entity.PrecioRenta = dto.PrecioRenta;
    }
}
