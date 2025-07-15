using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetasPlanNacionalDesarrolloController : BaseController<MetasPlanNacionalDesarrollo, MetasPlanNacionalDesarrolloResponse, CatalogoRequest>
    {
        public MetasPlanNacionalDesarrolloController(DBContext context)
            : base(context, "MetasPlanNacionalDesarrollo", "MetaPnId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.MetasPlanNacionalDesarrollo.Any(e => e.MetaPnId == id);
        }
    }
}
