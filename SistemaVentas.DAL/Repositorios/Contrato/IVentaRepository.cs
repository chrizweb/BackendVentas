using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.Models;

namespace SistemaVentas.DAL.Repositorios.Contrato {
  public interface IVentaRepository : IGenericRepository<Venta>{

    Task<Venta> registrarVenta(Venta model);
  }
}
