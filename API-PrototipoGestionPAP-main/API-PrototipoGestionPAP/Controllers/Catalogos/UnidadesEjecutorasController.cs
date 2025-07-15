using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesEjecutorasController : BaseController<UnidadesEjecutoras, UnidadesEjecutorasResponse, CatalogoRequest>
    {
        public UnidadesEjecutorasController(DBContext context)
            : base(context, "Unidad Ejecutora", "UnidadEjecutoraId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.UnidadesEjecutoras.Any(e => e.UnidadEjecutoraId == id);
        }
    }
}
