using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.EjesPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/ejes")]
    public class EjesPlanNacionalDesarrolloController : ControllerBase
    {
        private readonly IEjePnService _ejePnService;

        public EjesPlanNacionalDesarrolloController(IEjePnService ejePnService)
        {
            _ejePnService = ejePnService;
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? filter = null, [FromQuery] string? filterField = null)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new GeneralResponse<object>
                {
                    Code = 400,
                    Message = "Page y PageSize deben ser mayores a 0.",
                    Data = null
                });
            }

            try
            {
                var result = await _ejePnService.GetAllPaginatedAsync(page, pageSize, filter, filterField);

                return Ok(new GeneralResponse<PaginatedResponse<EjePnResponse>>
                {
                    Code = 200,
                    Message = "Ejes paginados obtenidos correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralResponse<object>
                {
                    Code = 400,
                    Message = $"Error al filtrar datos: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _ejePnService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Eje no encontrado.",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<EjePnResponse>
                {
                    Code = 200,
                    Message = "Eje obtenido correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al obtener el eje: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEjePnRequest request)
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

                var result = await _ejePnService.CreateAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = result.EjePnId }, new GeneralResponse<EjePnResponse>
                {
                    Code = 201,
                    Message = "Eje creado correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al crear el eje: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEjePnRequest request)
        {
            try
            {
                var success = await _ejePnService.UpdateAsync(id, request);
                if (success.Data != null)
                {
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Eje no encontrado para actualizar.",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Eje actualizado correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al actualizar el eje: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _ejePnService.DeleteAsync(id);
                if (success.Data != null)
                {
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Eje no encontrado para eliminar.",
                        Data = null
                    });
                }

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Eje eliminado correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno al eliminar el eje: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}