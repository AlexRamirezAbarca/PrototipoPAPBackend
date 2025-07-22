using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.PoliticaPlanNacionalDesarrollo
{

    [ApiController]
    [Route("api/[controller]")]
    public class PoliticaPlanNacionalDesarrolloController : ControllerBase
    {
        private readonly IPoliticaPnService _service;

        public PoliticaPlanNacionalDesarrolloController(IPoliticaPnService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var politica = await _service.GetByIdAsync(id);
                if (politica == null)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Política no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<PoliticaPnResponse>
                {
                    Code = 200,
                    Message = "Política obtenida correctamente.",
                    Data = politica
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? filter = null, [FromQuery] string? filterField = null)
        {
            try
            {
                var result = await _service.GetAllPaginatedAsync(page, pageSize, filter, filterField);
                return Ok(new GeneralResponse<PaginatedResponse<PoliticaPnResponse>>
                {
                    Code = 200,
                    Message = "Políticas obtenidas correctamente.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePoliticaPnRequest request)
        {
            try
            {
                var created = await _service.CreateAsync(request);
                return Ok(new GeneralResponse<PoliticaPnResponse>
                {
                    Code = 200,
                    Message = "Política creada correctamente.",
                    Data = created
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePoliticaPnRequest request)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, request);
                if (!updated)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Política no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Política actualizada correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Política no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Política eliminada correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = $"Error interno: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}