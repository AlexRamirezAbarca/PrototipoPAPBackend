using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class EjeObjetivoPnService : IEjeObjetivoPnService
    {
        private readonly DBContext _context;

        public EjeObjetivoPnService(DBContext context)
        {
            _context = context;
        }

        public async Task<EjeObjetivoPnResponse?> GetByIdAsync(int id)
        {
            var relacion = await _context.EjesObjetivosPN.FindAsync(id);
            if (relacion == null || relacion.estado == "N")
                return null;

            return new EjeObjetivoPnResponse
            {
                EjeObjetivoPnId = relacion.eje_objetivo_pn_id,
                EjePnId = relacion.eje_pn_id,
                ObjPnId = relacion.obj_pn_id,
                Estado = relacion.estado,
                FechaCreacion = relacion.fecha_creacion
            };
        }

        public async Task<PaginatedResponse<EjeObjetivoPnExtendedResponse>> GetAllPaginatedAsync(int page, int pageSize)
        {
            var query = _context.EjesObjetivosPN
                .Include(eo => eo.Eje)
                .Include(eo => eo.Objetivo)
                .Where(eo => eo.estado == "A")
                .AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var relaciones = await query
                .OrderByDescending(x => x.fecha_creacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = new List<EjeObjetivoPnExtendedResponse>();

            foreach (var relacion in relaciones)
            {
                var metas = await _context.ObjetivosMetasPN
                    .Where(om => om.obj_pn_id == relacion.obj_pn_id && om.estado == "A")
                    .Include(om => om.Meta)
                    .Select(m => new MetaSimpleDto
                    {
                        MetaPnId = m.Meta.MetaPnId,
                        Nombre = m.Meta.Nombre,
                        Descripcion = m.Meta.Descripcion
                    })
                    .ToListAsync();

                var politicas = await _context.ObjetivosPoliticasPN
                    .Where(op => op.obj_pn_id == relacion.obj_pn_id && op.estado == "A")
                    .Include(op => op.Politica)
                    .Select(p => new PoliticaSimpleDto
                    {
                        PoliticaPnId = p.Politica.PoliticaPnId,
                        Nombre = p.Politica.Nombre,
                        Descripcion = p.Politica.Descripcion
                    })
                    .ToListAsync();

                data.Add(new EjeObjetivoPnExtendedResponse
                {
                    EjeObjetivoPnId = relacion.eje_objetivo_pn_id,
                    Estado = relacion.estado,
                    FechaCreacion = relacion.fecha_creacion,
                    Eje = new EjeSimpleDto
                    {
                        EjePnId = relacion.Eje.EjePnId,
                        Nombre = relacion.Eje.Nombre,
                        Descripcion = relacion.Eje.Descripcion
                    },
                    Objetivo = new ObjetivoExtendidoDto
                    {
                        ObjPnId = relacion.Objetivo.ObjPnId,
                        Nombre = relacion.Objetivo.Nombre,
                        Descripcion = relacion.Objetivo.Descripcion,
                        Metas = metas,
                        Politicas = politicas
                    }
                });
            }

            return new PaginatedResponse<EjeObjetivoPnExtendedResponse>
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<EjeObjetivoPnResponse> CreateAsync(CreateEjeObjetivoPnRequest request)
        {
            var nuevaRelacion = new EjeObjetivoPN
            {
                eje_pn_id = request.EjePnId,
                obj_pn_id = request.ObjPnId,
                estado = "A",
                fecha_creacion = DateTime.UtcNow
            };

            _context.EjesObjetivosPN.Add(nuevaRelacion);
            await _context.SaveChangesAsync();

            return new EjeObjetivoPnResponse
            {
                EjeObjetivoPnId = nuevaRelacion.eje_objetivo_pn_id,
                EjePnId = nuevaRelacion.eje_pn_id,
                ObjPnId = nuevaRelacion.obj_pn_id,
                Estado = nuevaRelacion.estado,
                FechaCreacion = nuevaRelacion.fecha_creacion
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateEjeObjetivoPnRequest request)
        {
            var relacion = await _context.EjesObjetivosPN.FindAsync(id);
            if (relacion == null || relacion.estado == "N")
                return false;

            relacion.eje_pn_id = request.EjePnId;
            relacion.obj_pn_id = request.ObjPnId;
            relacion.fecha_modificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var relacion = await _context.EjesObjetivosPN.FindAsync(id);
            if (relacion == null)
                return false;

            _context.EjesObjetivosPN.Remove(relacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
