using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.Mantenedores.EjesPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetivoPoliticaController : ControllerBase
    {
        private readonly IObjetivoPoliticaService _service;

        public ObjetivoPoliticaController(IObjetivoPoliticaService service)
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
                    return NotFound();

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error al obtener la relación objetivo-política.");
            }
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _service.GetAllPaginatedAsync(page, pageSize);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error al obtener los datos paginados.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateObjetivoPoliticaRequest request)
        {
            try
            {
                var result = await _service.CreateAsync(request);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error al crear la relación objetivo-política.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateObjetivoPoliticaRequest request)
        {
            try
            {
                var success = await _service.UpdateAsync(id, request);
                if (!success)
                    return NotFound();

                return Ok("Relación actualizada correctamente.");
            }
            catch
            {
                return StatusCode(500, "Error al actualizar la relación objetivo-política.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return Ok("Relación eliminada correctamente.");
            }
            catch
            {
                return StatusCode(500, "Error al eliminar la relación objetivo-política.");
            }
        }
    }
}
