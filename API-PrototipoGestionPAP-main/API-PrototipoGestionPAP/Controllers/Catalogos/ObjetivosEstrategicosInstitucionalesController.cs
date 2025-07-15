using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetivosEstrategicosInstitucionalesController : BaseController<ObjetivosEstrategicosInstitucionales, ObjetivosEstrategicosInstitucionalesResponse, CatalogoRequest>
    {
        public ObjetivosEstrategicosInstitucionalesController(DBContext context)
            : base(context, "ObjetivosEstrategicosInstitucionale", "ObjEstrId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ObjetivosEstrategicosInstitucionales.Any(e => e.ObjEstrId == id);
        }
    }
}
