using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetivosOperativosController : BaseController<ObjetivosOperativos, ObjetivosOperativosResponse, CatalogoRequest>
    {
        public ObjetivosOperativosController(DBContext context)
            : base(context, "ObjetivosOperativo", "ObjetivoOperativoId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ObjetivosOperativos.Any(e => e.ObjetivoOperativoId == id);
        }
    }
}
