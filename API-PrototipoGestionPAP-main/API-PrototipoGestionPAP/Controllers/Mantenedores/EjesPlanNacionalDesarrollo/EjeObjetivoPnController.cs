using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.Mantenedores.EjesPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjeObjetivoPnController : ControllerBase
    {
        private readonly IEjeObjetivoPnService _service;

        public EjeObjetivoPnController(IEjeObjetivoPnService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { Code = 404, Message = "Relación no encontrada." });

                return Ok(new { Code = 200, Message = "Relación obtenida correctamente.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Message = "Error interno del servidor.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _service.GetAllPaginatedAsync(page, pageSize);
                return Ok(new { Code = 200, Message = "Relaciones obtenidas correctamente.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Message = "Error interno del servidor.", Details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEjeObjetivoPnRequest request)
        {
            try
            {
                var result = await _service.CreateAsync(request);
                return Ok(new { Code = 201, Message = "Relación creada correctamente.", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Message = "Error interno al crear la relación.", Details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEjeObjetivoPnRequest request)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, request);
                if (!updated)
                    return NotFound(new { Code = 404, Message = "Relación no encontrada o desactivada." });

                return Ok(new { Code = 200, Message = "Relación actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Message = "Error interno al actualizar.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { Code = 404, Message = "Relación no encontrada." });

                return Ok(new { Code = 200, Message = "Relación eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Message = "Error interno al eliminar.", Details = ex.Message });
            }
        }
    }
}
