using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramasNacionalesController : BaseController<ProgramasNacionales, ProgramasNacionalesResponse, CatalogoRequest>
    {
        public ProgramasNacionalesController(DBContext context)
            : base(context, "ProgramasNacionale", "ProgramaNacId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ProgramasNacionales.Any(e => e.ProgramaNacId == id);
        }
    }
}
