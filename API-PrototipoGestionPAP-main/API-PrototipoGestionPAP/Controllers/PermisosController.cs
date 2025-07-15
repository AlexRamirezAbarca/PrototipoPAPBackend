using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PrototipoGestionPAP.Models;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : BaseController<Permisos, Permisos, Permisos>
    {
        public PermisosController(DBContext context) : base(context, "Permiso", "PermisoId")
        {
        }

        protected override bool EntityExists(int id)
        {
            return _context.Permisos.Any(e => e.PermisoId == id);
        }
    }
}
