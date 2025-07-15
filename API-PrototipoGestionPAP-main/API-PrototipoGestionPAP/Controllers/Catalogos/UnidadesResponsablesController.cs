using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesResponsablesController : BaseController<UnidadesResponsables, UnidadesResponsablesResponse, CatalogoRequest>
    {
        public UnidadesResponsablesController(DBContext context)
            : base(context, "UnidadesResponsable", "UnidadRespId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.UnidadesResponsables.Any(e => e.UnidadRespId == id);
        }
    }
}
