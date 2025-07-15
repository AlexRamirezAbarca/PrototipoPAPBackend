using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : BaseController<Personas, PersonasResponse, PersonasRequest>
    {
        public PersonasController(DBContext context)
            : base(context, "Persona", "PersonaId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.Personas.Any(e => e.PersonaId == id);
        }

    }
}
