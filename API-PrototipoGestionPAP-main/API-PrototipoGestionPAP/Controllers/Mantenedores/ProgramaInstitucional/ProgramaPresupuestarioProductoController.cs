//using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
//using API_PrototipoGestionPAP.Services.Mantenedores;
//using Microsoft.AspNetCore.Mvc;

//namespace API_PrototipoGestionPAP.Controllers.Mantenedores.ProgramaInstitucional
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProgramaPresupuestarioProductoController : ControllerBase
//    {
//        private readonly ProgramaPresupuestarioProductoService _service;

//        public ProgramaPresupuestarioProductoController(ProgramaPresupuestarioProductoService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var result = await _service.GetAllAsync();
//            return Ok(result);
//        }

//        [HttpGet("por-programa-pre/{programaPreId}")]
//        public async Task<IActionResult> GetByProgramaPreId(int programaPreId)
//        {
//            var result = await _service.GetByProgramaPreIdAsync(programaPreId);
//            return Ok(result);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] ProgramaPresupuestarioProductoRequestDto dto)
//        {
//            await _service.CreateAsync(dto);
//            return Ok(new { message = "Relación creada exitosamente." });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            await _service.DeleteAsync(id);
//            return Ok(new { message = "Relación eliminada exitosamente." });
//        }
//    }
//}
