using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccionesController : BaseController<Acciones, AccionesResponse, CatalogoRequest>
    {
        public AccionesController(DBContext context)
            : base(context, "Accione", "AccionId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.Acciones.Any(e => e.AccionId == id);
        }
    }
}
