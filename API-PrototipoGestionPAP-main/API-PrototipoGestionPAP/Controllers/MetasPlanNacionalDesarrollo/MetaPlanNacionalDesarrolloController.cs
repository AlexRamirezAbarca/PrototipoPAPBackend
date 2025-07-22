using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_PrototipoGestionPAP.Controllers.MetasPlanNacionalDesarrollo
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetaPlanNacionalDesarrolloController : ControllerBase
    {
        private readonly IMetaPnService _metaPnService;

        public MetaPlanNacionalDesarrolloController(IMetaPnService metaPnService)
        {
            _metaPnService = metaPnService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<object>>> GetById(int id)
        {
            try
            {
                var meta = await _metaPnService.GetByIdAsync(id);
                if (meta == null)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Meta no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Meta obtenida correctamente.",
                    Data = meta
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error interno del servidor.",
                    Data = ex.Message
                });
            }
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<GeneralResponse<object>>> GetAllPaginated(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? filter = null,
            [FromQuery] string? filterField = null)
        {
            try
            {
                var paginated = await _metaPnService.GetAllPaginatedAsync(page, pageSize, filter, filterField);
                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Metas obtenidas correctamente.",
                    Data = paginated
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error interno del servidor.",
                    Data = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<object>>> Create([FromBody] CreateMetaPnRequest request)
        {
            try
            {
                var created = await _metaPnService.CreateAsync(request);
                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Meta creada correctamente.",
                    Data = created
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error interno del servidor.",
                    Data = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<object>>> Update(int id, [FromBody] CreateMetaPnRequest request)
        {
            try
            {
                var updated = await _metaPnService.UpdateAsync(id, request);
                if (!updated)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Meta no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Meta actualizada correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error interno del servidor.",
                    Data = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<object>>> Delete(int id)
        {
            try
            {
                var deleted = await _metaPnService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Meta no encontrada.",
                        Data = null
                    });

                return Ok(new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Meta eliminada correctamente.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Error interno del servidor.",
                    Data = ex.Message
                });
            }
        }
    }
}