using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTOs;
using SistemaVentas.API.Utilidad;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class VentaController : ControllerBase {

		private readonly IVentaService ventaService;

		public VentaController(IVentaService ventaService) {
			this.ventaService = ventaService;
		}

		[HttpPost]
		[Route("Registrar")]
		public async Task<IActionResult> Registrar([FromBody] VentaDTO venta) {
			var response = new Response<VentaDTO>();

			try {
				response.status = true;
				response.value = await ventaService.Registrar(venta);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}

		[HttpGet]
		[Route("Historial")]
		public async Task<IActionResult> Historial(string buscarPor, string numeroVenta, string?fechaInicio, string?fechaFin) {
			var response = new Response<List<VentaDTO>>();

			numeroVenta = numeroVenta is null ? "" : numeroVenta;
			fechaInicio = fechaInicio is null ? "" : fechaInicio;
			fechaFin = fechaFin is null ? "" : fechaFin;

			try {
				response.status = true;
				response.value = await ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw; 
			}
			return Ok(response);
		}

		[HttpGet]
		[Route("Reporte")]
		public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin) {
			var response = new Response<List<ReporteDTO>>();

			try {
				response.status = true;
				response.value = await ventaService.Reporte(fechaInicio, fechaFin);

			} catch (Exception ex) {
				response.status = false;
				response.msg = ex.Message;
				throw;
			}
			return Ok(response);
		}
	}
}
