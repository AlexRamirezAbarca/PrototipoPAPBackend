using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramasInstitucionalesController : BaseController<ProgramasInstitucionales, ProgramasInstitucionalesResponse, CatalogoRequest>
    {
        public ProgramasInstitucionalesController(DBContext context)
            : base(context, "ProgramasInstitucionale", "ProgramaInstId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ProgramasInstitucionales.Any(e => e.ProgramaInstId == id);
        }
    }
}
