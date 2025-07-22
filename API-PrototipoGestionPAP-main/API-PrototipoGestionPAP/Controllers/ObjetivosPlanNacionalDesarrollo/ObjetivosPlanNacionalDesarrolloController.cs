using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.ObjetivosPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/objetivos")]
    public class ObjetivosPlanNacionalDesarrolloController : ControllerBase
    {
        private readonly IObjetivoPnService _objetivoPnService;

        public ObjetivosPlanNacionalDesarrolloController(IObjetivoPnService objetivoPnService)
        {
            _objetivoPnService = objetivoPnService;
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllPaginated(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? filter = null,
            [FromQuery] string? filterField = null)
        {
            try
            {
                var result = await _objetivoPnService.GetAllPaginatedAsync(page, pageSize, filter, filterField);

                return Ok(new GeneralResponse<PaginatedResponse<ObjetivoPnResponse>>
                {
                    Code = 200,
                    Message = "Objetivos obtenidos correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al obtener los objetivos: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _objetivoPnService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Objetivo no encontrado.",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<ObjetivoPnResponse>
                {
                    Code = 200,
                    Message = "Objetivo obtenido correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al obtener el objetivo: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateObjetivoPnRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Nombre))
                {
                    return BadRequest(new GeneralResponse<object>
                    {
                        Code = 400,
                        Message = "El cuerpo de la solicitud es inválido.",
                        Data = null
                    });
                }

                var result = await _objetivoPnService.CreateAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = result.ObjPnId }, new GeneralResponse<ObjetivoPnResponse>
                {
                    Code = 201,
                    Message = "Objetivo creado correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al crear el objetivo: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateObjetivoPnRequest request)
        {
            try
            {
                var success = await _objetivoPnService.UpdateAsync(id, request);
                if (!success)
                {
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Objetivo no encontrado para actualizar.",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Objetivo actualizado correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al actualizar el objetivo: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _objetivoPnService.DeleteAsync(id);
                if (!success)
                {
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Objetivo no encontrado para eliminar.",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Objetivo eliminado correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al eliminar el objetivo: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}