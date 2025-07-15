using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadoresController : BaseController<Indicadores, IndicadoresResponse, IndicadoresResponse>
    {
        public IndicadoresController(DBContext context)
            : base(context, "Indicadore", "IndicadorId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.Indicadores.Any(e => e.IndicadorId == id);
        }
    }
}
