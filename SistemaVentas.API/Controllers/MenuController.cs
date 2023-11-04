using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class MenuController : ControllerBase {

		private readonly IMenuService menuService;
		public MenuController(IMenuService menuService) {
			this.menuService = menuService;
		}

		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista(int id_usuario) {
			var response = new Response<List<MenuDTO>>();

			try {
				response.status = true;
				response.value = await menuService.Lista(id_usuario);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}
	}
}
