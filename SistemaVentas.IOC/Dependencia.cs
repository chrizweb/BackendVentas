using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVentas.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DAL.Repositorios.Contrato; 
using SistemaVentas.DAL.Repositorios;

using SistemaVentas.Utility;

namespace SistemaVentas.IOC {
  public static class Dependencia {

    /*Agregando cadena de conexion a base de datos*/
    public static void InyectarDependecias(this IServiceCollection services, IConfiguration configuration) {

      services.AddDbContext<DbventasContext>(options => {
        options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
      });

      /*Agregando dependecias de interfaces y clases genericas*/
      services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
      /*Agregando dependecia de venta*/
      services.AddScoped<IVentaRepository, VentaRepository>();

      /*Agregando dependencia de AutoMapper*/
      services.AddAutoMapper(typeof(AutoMapperProfile));

    }
  }
}
