using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetivosPlanNacionalDesarrolloController : BaseController<ObjetivosPlanNacionalDesarrollo, ObjetivosPlanNacionalDesarrolloResponse, CatalogoRequest>
    {
        public ObjetivosPlanNacionalDesarrolloController(DBContext context)
            : base(context, "ObjetivosPlanNacionalDesarrollo", "ObjPnId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ObjetivosPlanNacionalDesarrollo.Any(e => e.ObjPnId == id);
        }
    }
}
