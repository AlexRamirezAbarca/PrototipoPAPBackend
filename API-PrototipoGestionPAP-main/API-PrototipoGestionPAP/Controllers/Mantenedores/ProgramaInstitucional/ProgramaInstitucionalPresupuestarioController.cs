using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Services.Mantenedores;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.Mantenedores.ProgramaInstitucional
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaInstitucionalPresupuestarioController : ControllerBase
    {
        private readonly ProgramaInstitucionalPresupuestarioService _service;

        public ProgramaInstitucionalPresupuestarioController(ProgramaInstitucionalPresupuestarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProgramaInstitucionalPresupuestarioRequestDto dto)
        {
            await _service.CrearAsync(dto);
            return Ok(new { message = "Relación creada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.EliminarAsync(id);
            return Ok(new { message = "Relación eliminada lógicamente" });
        }

        [HttpGet("por-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("por-programa-inst/{programaInstId}")]
        public async Task<IActionResult> GetByProgramaInstId(int programaInstId)
        {
            var result = await _service.GetDetalleJerarquicoPorProgramaInstitucionalIdAsync(programaInstId);
            return Ok(result);
        }

    }
}
