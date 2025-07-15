using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramasPresupuestariosController : BaseController<ProgramasPresupuestarios, ProgramasPresupuestariosResponse, CatalogoRequest>
    {
        public ProgramasPresupuestariosController(DBContext context)
            : base(context, "ProgramasPresupuestario", "ProgramaPreId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ProgramasPresupuestarios.Any(e => e.ProgramaPreId == id);
        }
    }
}
