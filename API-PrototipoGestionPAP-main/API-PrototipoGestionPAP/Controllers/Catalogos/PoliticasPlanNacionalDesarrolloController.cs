using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliticasPlanNacionalDesarrolloController : BaseController<PoliticasPlanNacionalDesarrollo, PoliticasPlanNacionalDesarrolloResponse, CatalogoRequest>
    {
        public PoliticasPlanNacionalDesarrolloController(DBContext context)
            : base(context, "PoliticasPlanNacionalDesarrollo", "PoliticaPnId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.PoliticasPlanNacionalDesarrollo.Any(e => e.PoliticaPnId == id);
        }
    }
}
