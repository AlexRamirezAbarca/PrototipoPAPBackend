using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Services.Mantenedores;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.Mantenedores.EjesPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetivoMetaPNController : ControllerBase
    {
        private readonly ObjetivoMetaPNService _service;

        public ObjetivoMetaPNController(ObjetivoMetaPNService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ObjetivoMetaRequestDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok("Eliminado correctamente.") : NotFound("No encontrado.");
        }

        [HttpGet("por-objetivo/{objPnId}")]
        public async Task<IActionResult> GetByObjetivoId(int objPnId)
        {
            var data = await _service.GetByObjetivoIdAsync(objPnId);
            return Ok(data);
        }
    }
}
