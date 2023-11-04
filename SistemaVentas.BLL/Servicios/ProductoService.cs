using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.Models;

namespace SistemaVentas.BLL.Servicios {
  public class ProductoService : IProductoService {

    private readonly IGenericRepository<Producto> producto_repository;
    private readonly IMapper mapper;

    public ProductoService(IGenericRepository<Producto> producto_repository, IMapper mapper) {
      this.producto_repository = producto_repository;
      this.mapper = mapper;
    }
    public async Task<List<ProductoDTO>> Lista() {
      try {
        var query_producto = await producto_repository.Consult();
        var list_productos = query_producto.Include(cat => cat.IdCategoriaNavigation).ToList();

        return mapper.Map<List<ProductoDTO>>(list_productos.ToList()); 

      } catch (Exception) {

        throw;
      }
    }

    public async Task<ProductoDTO> Crear(ProductoDTO model) {
      try {
        var create_producto = await producto_repository.Create(mapper.Map<Producto>(model));

        if (create_producto.IdProducto == 0)
          throw new TaskCanceledException("No se puedo crear");

        return mapper.Map<ProductoDTO>(create_producto);

      } catch (Exception) {

        throw;
      }
    }

    public async Task<bool> Editar(ProductoDTO model) {
      try {
        var producto_req = mapper.Map<Producto>(model);
        var producto_found = await producto_repository.Get(p => p.IdProducto == producto_req.IdProducto);

        if(producto_found == null)
          throw new TaskCanceledException("El producto no existe");

        producto_found.Nombre = producto_req.Nombre;
        producto_found.IdCategoria = producto_req.IdCategoria;
        producto_found.Stock = producto_req.Stock;
        producto_found.Precio = producto_req.Precio;
        producto_found.EsActivo = producto_req.EsActivo;

        bool updated_producto = await producto_repository.Edit(producto_found);

        if (!updated_producto)
          throw new TaskCanceledException("No se puedo editar");

        return updated_producto;

      } catch (Exception) {

        throw;
      }
    }

    public async Task<bool> Eliminar(int id) {
      try {
        var producto_found = await producto_repository.Get(p => p.IdProducto == id);

        if (producto_found == null)
          throw new TaskCanceledException("El producto no existe");

        bool delete_producto = await producto_repository.Delete(producto_found);

        if (!delete_producto)
          throw new TaskCanceledException("No se puedo eliminar");

        return delete_producto;

      } catch (Exception) {

        throw;
      }
    }

  }
}
















