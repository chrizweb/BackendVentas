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
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.IOC {
  public static class Dependencia {

    /*Agregando cadena de conexion a base de datos*/
    public static void InyectarDependecias(this IServiceCollection services, IConfiguration configuration) {

      services.AddDbContext<DbventasContext>(options => {
        options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
      });

      /*Agregando dependecias de interfas y clase GenericRepository*/
      services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
      /*Agregando dependecia de interfas y clase VentaRepository*/
      services.AddScoped<IVentaRepository, VentaRepository>();

      /*Agregando dependencia de AutoMapper*/
      services.AddAutoMapper(typeof(AutoMapperProfile));

      /*Agregando dependencia de interfaces y clases implements Service*/
      services.AddScoped<IRolService, RolService>();
      services.AddScoped<IUsuarioService, UsuarioService>();
      services.AddScoped<ICategoriaService, CategoriaService>();
      services.AddScoped<IProductoService, ProductoService>();
      services.AddScoped<IDashBoardService, DashBoardService>();
      services.AddScoped<IMenuService, MenuService>();

    }
  }
}
