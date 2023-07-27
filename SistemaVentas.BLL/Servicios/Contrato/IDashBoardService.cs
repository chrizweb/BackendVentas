using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DTOs;

namespace SistemaVentas.BLL.Servicios.Contrato {
  public interface IDashBoardService {

    Task<DashBoardDTO> Resumen();
  }
}
