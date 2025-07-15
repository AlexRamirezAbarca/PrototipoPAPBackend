using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjesPlanNacionalDesarrolloController : BaseController<EjesPlanNacionalDesarrollo, EjesPlanNacionalDesarrolloResponse, CatalogoRequest>
    {
        public EjesPlanNacionalDesarrolloController(DBContext context)
            : base(context, "EjesPlanNacionalDesarrollo", "EjePnId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.EjesPlanNacionalDesarrollo.Any(e => e.EjePnId == id);
        }
    }
}
