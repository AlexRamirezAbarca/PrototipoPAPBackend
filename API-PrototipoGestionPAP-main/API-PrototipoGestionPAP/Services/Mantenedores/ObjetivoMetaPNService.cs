using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class ObjetivoMetaPnService : IObjetivoMetaPnService
    {
        private readonly DBContext _context;

        public ObjetivoMetaPnService(DBContext context)
        {
            _context = context;
        }

        public async Task<ObjetivoMetaPnResponse?> GetByIdAsync(int id)
        {
            var relacion = await _context.ObjetivosMetasPN.FindAsync(id);
            if (relacion == null || relacion.estado == "N")
                return null;

            return new ObjetivoMetaPnResponse
            {
                objetivo_meta_pn_id = relacion.objetivo_meta_pn_id,
                obj_pn_id = relacion.obj_pn_id,
                meta_pn_id = relacion.meta_pn_id,
                estado = relacion.estado,
                fecha_creacion = relacion.fecha_creacion
            };
        }

        public async Task<PaginatedResponse<ObjetivoMetaPnResponse>> GetAllPaginatedAsync(int page, int pageSize)
        {
            var query = _context.ObjetivosMetasPN
                .Where(x => x.estado == "A")
                .AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var data = await query
                .OrderByDescending(x => x.fecha_creacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ObjetivoMetaPnResponse
                {
                    objetivo_meta_pn_id = x.objetivo_meta_pn_id,
                    obj_pn_id = x.obj_pn_id,
                    meta_pn_id = x.meta_pn_id,
                    estado = x.estado,
                    fecha_creacion = x.fecha_creacion
                })
                .ToListAsync();

            return new PaginatedResponse<ObjetivoMetaPnResponse>
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<ObjetivoMetaPnResponse> CreateAsync(CreateObjetivoMetaPnRequest request)
        {
            var nuevaRelacion = new ObjetivoMetaPN
            {
                obj_pn_id = request.obj_pn_id,
                meta_pn_id = request.meta_pn_id,
                estado = "A",
                fecha_creacion = DateTime.UtcNow
            };

            _context.ObjetivosMetasPN.Add(nuevaRelacion);
            await _context.SaveChangesAsync();

            return new ObjetivoMetaPnResponse
            {
                objetivo_meta_pn_id = nuevaRelacion.objetivo_meta_pn_id,
                obj_pn_id = nuevaRelacion.obj_pn_id,
                meta_pn_id = nuevaRelacion.meta_pn_id,
                estado = nuevaRelacion.estado,
                fecha_creacion = nuevaRelacion.fecha_creacion
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateObjetivoMetaPnRequest request)
        {
            var relacion = await _context.ObjetivosMetasPN.FindAsync(id);
            if (relacion == null || relacion.estado == "N")
                return false;

            relacion.obj_pn_id = request.obj_pn_id;
            relacion.meta_pn_id = request.meta_pn_id;
            relacion.fecha_modificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var relacion = await _context.ObjetivosMetasPN.FindAsync(id);
            if (relacion == null)
                return false;

            _context.ObjetivosMetasPN.Remove(relacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
