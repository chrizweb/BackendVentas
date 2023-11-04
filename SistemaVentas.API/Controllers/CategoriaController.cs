using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriaController : ControllerBase {

		private readonly ICategoriaService categoriaService;
		public CategoriaController(ICategoriaService categoriaService) {
			this.categoriaService = categoriaService;
		}

		[HttpGet] 
		[Route("Lista")]
		public async Task<IActionResult> Lista() {
			var response = new Response<List<CategoriaDTO>>();

			try {
				response.status = true;
				response.value = await categoriaService.Lista();

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}
	}
}  
