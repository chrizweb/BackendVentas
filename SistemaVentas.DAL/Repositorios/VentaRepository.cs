using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DAL.DBContext;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.Models;

namespace SistemaVentas.DAL.Repositorios {
  public class VentaRepository : GenericRepository<Venta>, IVentaRepository {

    private readonly DbventasContext db_context;

    public VentaRepository(DbventasContext db_context) : base(db_context) {
      this.db_context = db_context;
    }

    public async Task<Venta> registrarVenta(Venta model) {

      Venta venta_generada = new Venta();
      using (var transaction = db_context.Database.BeginTransaction()) {
        try {

          /* El foreach busca el producto para poder restar su stock para llevar el registro de venta*/
          foreach(DetalleVenta detalle_venta in model.DetalleVenta) {
            Producto producto_encontrado = db_context.Productos
              .Where(producto => producto.IdProducto == detalle_venta.IdProducto)
              .First();

            producto_encontrado.Stock = producto_encontrado.Stock - detalle_venta.Cantidad;
            db_context.Productos.Update(producto_encontrado);
          } 

          await db_context.SaveChangesAsync();

					/*Numero correlativo de venta*/
					NumeroDocumento correlativo = db_context.NumeroDocumentos.First();
          correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
          correlativo.FechaRegistro = DateTime.Now;

          db_context.NumeroDocumentos.Update(correlativo);
          await db_context.SaveChangesAsync();

          int CantidadDigitos = 4;
          string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
          string numero_venta = ceros + correlativo.UltimoNumero.ToString();

          numero_venta = numero_venta.Substring(numero_venta.Length - CantidadDigitos, CantidadDigitos);

          model.NumeroDocumento = numero_venta;

          await db_context.Venta.AddAsync(model);
          await db_context.SaveChangesAsync();

					/*Venta generada con numero correlativo*/
					venta_generada = model;
          transaction.Commit();

        } catch (Exception) {

          transaction.Rollback();
          throw;
        }
        return venta_generada;
      }
    }
  }
}
