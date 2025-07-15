using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosInstitucionalesController : BaseController<ProductosInstitucionales, ProductosInstitucionalesResponse, CatalogoRequest>
    {
        public ProductosInstitucionalesController(DBContext context)
            : base(context, "ProductosInstitucionale", "ProductoInstId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.ProductosInstitucionales.Any(e => e.ProductoInstId == id);
        }
    }
}
