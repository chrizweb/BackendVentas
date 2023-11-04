using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase {

		private readonly IUsuarioService usuarioService;

		public UsuarioController(IUsuarioService usuarioService) {
			this.usuarioService = usuarioService;
		}

		[HttpGet]
		[Route("Lista")]
		public async Task<IActionResult> Lista() {
			var response = new Response<List<UsuarioDTO>>();

			try {
				response.status = true;
				response.value = await usuarioService.Lista();

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpPost]
		[Route("IniciarSesion")]
		public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login) {
			var response = new Response<SesionDTO>();

			try {
				response.status = true;
				response.value = await usuarioService.ValidarCredenciales(
					login.Correo,
					login.Clave
				);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpPost]
		[Route("Guardar")]
		public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario) {
			var response = new Response<UsuarioDTO>();

			try {
				response.status = true;
				response.value = await usuarioService.Crear(usuario);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario) {
			var response = new Response<bool>();

			try {
				response.status = true;
				response.value = await usuarioService.Editar(usuario);

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
				response.value = await usuarioService.Eliminar(id);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

	}
}
