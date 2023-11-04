using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class DashBoardController : ControllerBase {

		public readonly IDashBoardService dashboardService;
		public DashBoardController(IDashBoardService dashboardService) {
			this.dashboardService = dashboardService;
		}

		[HttpGet]
		[Route("Resumen")]
		public async Task<IActionResult> Resumen() {
			var response = new Response<DashBoardDTO>();

			try {
				response.status = true;
				response.value = await dashboardService.Resumen();

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}
	}
}
