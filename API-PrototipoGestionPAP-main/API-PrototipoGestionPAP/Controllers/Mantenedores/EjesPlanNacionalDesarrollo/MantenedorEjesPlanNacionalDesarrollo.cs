//using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
//using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound;
//using API_PrototipoGestionPAP.Services.Mantenedores;
//using Microsoft.AspNetCore.Mvc;

//namespace API_PrototipoGestionPAP.Controllers.Mantenedores.NewFolder
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MantenedorEjesPlanNacionalDesarrollo : ControllerBase
//    {
//        private readonly EjeObjetivoPNService _service;

//        public MantenedorEjesPlanNacionalDesarrollo(EjeObjetivoPNService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var data = await _service.GetAllAsync();
//            return Ok(data);
//        }

//        [HttpGet("por-eje/{ejeId}")]
//        public async Task<IActionResult> GetByEjeId(int ejeId)
//        {
//            var data = await _service.GetByEjeIdAsync(ejeId);
//            return Ok(data);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] EjeObjetivoRequestDto dto)
//        {
//            var result = await _service.CreateAsync(dto);
//            return Ok(new { message = result });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _service.DeleteAsync(id);
//            if (!success)
//                return NotFound("No encontrado o ya eliminado.");
//            return Ok("Eliminado correctamente.");
//        }

//        [HttpGet("jerarquico/{ejeId}")]
//        public async Task<ActionResult<EjeJerarquicoResponseDto>> GetJerarquico(int ejeId)
//        {
//            var result = await _service.GetEjeJerarquicoAsync(ejeId);

//            if (result == null)
//                return NotFound($"No se encontró un eje con ID {ejeId}.");

//            return Ok(result);
//        }

//    }
//}
