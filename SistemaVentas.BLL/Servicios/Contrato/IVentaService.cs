using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DTOs;

namespace SistemaVentas.BLL.Servicios.Contrato {
  public interface IVentaService {

    Task<VentaDTO> Registrar(VentaDTO model);
    Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
    Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin);

  }
}
