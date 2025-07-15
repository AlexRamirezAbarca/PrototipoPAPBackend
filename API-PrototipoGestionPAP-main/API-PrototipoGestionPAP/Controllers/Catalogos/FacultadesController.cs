using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultadesController : BaseController<Facultades, FacultadesResponse, CatalogoRequest>
    {
        public FacultadesController(DBContext context)
            : base(context, "Facultade", "FacultadId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.Facultades.Any(e => e.FacultadId == id);
        }
    }
}
