using System;
using System.Collections.Generic;
using System.Globalization;
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
  public class VentaService : IVentaService {

    private readonly IVentaRepository venta_repository;
    private readonly IGenericRepository<DetalleVenta> detventa_repository;
    private readonly IMapper mapper;

    public VentaService(IVentaRepository venta_repository, IGenericRepository<DetalleVenta> detventa_repository, IMapper mapper) {
      this.venta_repository = venta_repository;
      this.detventa_repository = detventa_repository;
      this.mapper = mapper;
    }

    public async Task<VentaDTO> Registrar(VentaDTO model) {
      try {
        var venta_generada = await venta_repository.registrarVenta(mapper.Map<Venta>(model));

        if (venta_generada.IdVenta == 0)
          throw new TaskCanceledException("No se puedo registrar venta");

        /*Esta venta generada es retornada con numero de documento #0000 y el id venta*/
        return mapper.Map<VentaDTO>(venta_generada);

      } catch (Exception) {

        throw;
      }
    }

    public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin) {
        
      IQueryable<Venta> query = await venta_repository.Consult();
      var resultado_ventas = new List<Venta>();

      try {
        /*Buscar historial de venta por fecha, convertir fechas de string a DateTime*/
        if (buscarPor == "fecha") {
          DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ES"));
          DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ES"));

          resultado_ventas = await query.Where(v =>
            v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
            v.FechaRegistro.Value.Date <= fecha_fin.Date
          ).Include(dv => dv.DetalleVenta)
          .ThenInclude(p => p.IdProductoNavigation)
          .ToListAsync();
        }
        else {
          /*Buscar historial de ventas por numero de venta*/
          resultado_ventas = await query.Where(v =>
          v.NumeroDocumento == numeroVenta
          ).Include(dv => dv.DetalleVenta)
          .ThenInclude(p => p.IdProductoNavigation)
          .ToListAsync();

        }  

      } catch (Exception) {
         
        throw;
      }
      return mapper.Map<List<VentaDTO>>(resultado_ventas);
    }

    public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin) {

      IQueryable<DetalleVenta> query = await detventa_repository.Consult();
      var resultado_detventa = new List<DetalleVenta>();

      try {
        DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ES"));
        DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ES"));

        resultado_detventa = await query
        .Include(p => p.IdProductoNavigation)
        .Include(v => v.IdVentaNavigation)
        .Where(dv =>
         dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_inicio.Date &&
         dv.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_fin.Date
         ).ToListAsync();

      } catch (Exception) {

        throw;
      }
      return mapper.Map<List<ReporteDTO>>(resultado_detventa);
    }


  }
}
