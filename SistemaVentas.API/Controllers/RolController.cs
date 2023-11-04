using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class RolController : ControllerBase {
    private readonly IRolService rolService;

    public RolController(IRolService rolService) {
      this.rolService = rolService;
    }

    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista() {
      var response = new Response<List<RolDTO>>();

      try {
        response.status = true;
        response.value = await rolService.Lista();

      } catch (Exception ex) {
        response.status = false;
        response.msg = ex.Message;
        throw;
      }
      return Ok(response);
    }
  }
}
