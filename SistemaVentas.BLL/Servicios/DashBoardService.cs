using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.Models;

namespace SistemaVentas.BLL.Servicios {
  public class DashBoardService : IDashBoardService {

    private readonly IVentaRepository venta_repository;
    private readonly IGenericRepository<Producto> producto_repository;
    private readonly IMapper mapper;

    public DashBoardService(IVentaRepository venta_repository, IGenericRepository<Producto> producto_repository, IMapper mapper) {
      this.venta_repository = venta_repository;
      this.producto_repository = producto_repository;
      this.mapper = mapper;
    }

    private IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarDias) {

      DateTime? ultima_fecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

      ultima_fecha = ultima_fecha.Value.AddDays(restarDias);

      return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultima_fecha.Value.Date);
    }

    private async Task<int> TotalVentasUltimaSemana() {

      int total = 0;
      IQueryable<Venta> venta_query = await venta_repository.Consult();

      if (venta_query.Count() > 0) {
        /*obtener total de ventas registradas hace 7 dias*/
        var tabla_venta = RetornarVentas(venta_query, -7);
        total = tabla_venta.Count();
      }
      return total;
    }

    private async Task<string> TotalIngresosUltimaSemana() {

      decimal resultado = 0;
      IQueryable<Venta> venta_query = await venta_repository.Consult();

      /*verificar si existen ventas*/
      if (venta_query.Count() > 0) {

        var tabla_venta = RetornarVentas(venta_query, -7);
        /*Actualizando variable resultado, debe hacer la suma de todos los ingresos*/
        resultado = tabla_venta.Select(v => v.Total).Sum(v => v.Value);
      }
      return Convert.ToString(resultado, new CultureInfo("es-ES"));
    }

    private async Task<int> TotalProductos() {

      IQueryable<Producto> producto_query = await producto_repository.Consult();

      int total = producto_query.Count();
      return total;
    }
    /*Accede con string y retorna con entero */
    private async Task<Dictionary<string, int>> VentasUltimaSemana() {

      Dictionary<string, int> resultado = new Dictionary<string, int>();

      IQueryable<Venta> venta_query = await venta_repository.Consult();

      if (venta_query.Count() > 0) {
        var tabla_venta = RetornarVentas(venta_query, -7);
        resultado = tabla_venta
          .GroupBy(v => v.FechaRegistro.Value.Date)
          /*Key es la agrupacion de las fechas*/
          .OrderBy(g => g.Key)
          /*Obtenemos un total por cada agrupacion de fecha que se haga*/
          .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
          /*Todo lo anterior se convertira en un diccionario*/
          .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
      }
      return resultado;
    }

    public async Task<DashBoardDTO> Resumen() {
      DashBoardDTO view_dashboard = new DashBoardDTO();

      try {
        view_dashboard.TotalVentas = await TotalVentasUltimaSemana();
        view_dashboard.TotalIngresos = await TotalIngresosUltimaSemana();
        view_dashboard.TotalProductos = await TotalProductos();

        List<VentaSemanaDTO> list_ventaSemanal = new List<VentaSemanaDTO>();
        /*string: Como lo vamos a llamarlo, int: Como lo vamos a retornar*/
        /*Iterar los elementos del diccionario*/
        foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())  {
          list_ventaSemanal.Add(new VentaSemanaDTO() {
            Fecha = item.Key,
            Total = item.Value
          });
          view_dashboard.VentaUltimaSemana = list_ventaSemanal;
        }

      } catch (Exception) {

        throw;
      }
      return view_dashboard;
    }
  }
}
