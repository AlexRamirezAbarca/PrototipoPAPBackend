using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.Mantenedores.EjesPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetivoMetaPnController : ControllerBase
    {
        private readonly IObjetivoMetaPnService _service;

        public ObjetivoMetaPnController(IObjetivoMetaPnService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<ObjetivoMetaPnResponse>>> GetById(int id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);
                if (data == null)
                    return NotFound(new GeneralResponse<ObjetivoMetaPnResponse>
                    {
                        Code = 404,
                        Message = "Relación no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<ObjetivoMetaPnResponse>
                {
                    Code = 200,
                    Message = "Relación encontrada exitosamente.",
                    Data = data
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new GeneralResponse<ObjetivoMetaPnResponse>
                {
                    Code = 500,
                    Message = "Ocurrió un error al obtener la relación.",
                    Data = null
                });
            }
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<PaginatedResponse<ObjetivoMetaPnResponse>>> GetAllPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _service.GetAllPaginatedAsync(page, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<ObjetivoMetaPnResponse>>> Create([FromBody] CreateObjetivoMetaPnRequest request)
        {
            try
            {
                var created = await _service.CreateAsync(request);

                return Ok(new GeneralResponse<ObjetivoMetaPnResponse>
                {
                    Code = 200,
                    Message = "Relación creada correctamente.",
                    Data = created
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new GeneralResponse<ObjetivoMetaPnResponse>
                {
                    Code = 500,
                    Message = "Error al crear la relación.",
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<object>>> Update(int id, [FromBody] CreateObjetivoMetaPnRequest request)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, request);

                if (!updated)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Relación no encontrada o eliminada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Relación actualizada exitosamente.",
                    Data = null
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error al actualizar la relación.",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<object>>> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);

                if (!deleted)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Relación no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Relación eliminada correctamente.",
                    Data = null
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error al eliminar la relación.",
                    Data = null
                });
            }
        }
    }
}
