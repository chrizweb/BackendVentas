using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ProductoController : ControllerBase {

		private readonly IProductoService productoService;

		public ProductoController(IProductoService productoService) {
			this.productoService = productoService;
		}

		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista() {
			var response = new Response<List<ProductoDTO>>();

			try {
				response.status = true;
				response.value = await productoService.Lista();

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpPost]
		[Route("Guardar")]
		public async Task<IActionResult> Guardar([FromBody] ProductoDTO producto) {
			var response = new Response<ProductoDTO>();

			try {
				response.status = true;
				response.value = await productoService.Crear(producto);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar([FromBody] ProductoDTO producto) {
			var response = new Response<bool>();

			try {
				response.status = true;
				response.value = await productoService.Editar(producto);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpDelete]
		[Route("Eliminar/{id:int}")]
		public async Task<IActionResult> Eliminar(int id) {
			var response = new Response<bool>();

			try {
				response.status = true;
				response.value = await productoService.Eliminar(id);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}
	}
}
