using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services
{
    public class MetaPnService : IMetaPnService
    {
        private readonly DBContext _context;

        public MetaPnService(DBContext context)
        {
            _context = context;
        }

        public async Task<MetaPnResponse?> GetByIdAsync(int id)
        {
            var meta = await _context.MetasPlanNacionalDesarrollo
                .FirstOrDefaultAsync(x => x.MetaPnId == id && x.Estado == "A");

            if (meta == null) return null;

            return new MetaPnResponse
            {
                MetaPnId = meta.MetaPnId,
                Nombre = meta.Nombre,
                Descripcion = meta.Descripcion,
                Estado = meta.Estado,
                FechaCreacion = meta.FechaCreacion,
                FechaModificacion = meta.FechaModificacion
            };
        }

        public async Task<MetaPnResponse> CreateAsync(CreateMetaPnRequest request)
        {
            var nuevaMeta = new MetasPlanNacionalDesarrollo
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = "A",
                FechaCreacion = DateTime.UtcNow
            };

            _context.MetasPlanNacionalDesarrollo.Add(nuevaMeta);
            await _context.SaveChangesAsync();

            return new MetaPnResponse
            {
                MetaPnId = nuevaMeta.MetaPnId,
                Nombre = nuevaMeta.Nombre,
                Descripcion = nuevaMeta.Descripcion,
                Estado = nuevaMeta.Estado,
                FechaCreacion = nuevaMeta.FechaCreacion
            };
        }

        public async Task<PaginatedResponse<MetaPnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField)
        {
            var query = _context.MetasPlanNacionalDesarrollo
                .Where(x => x.Estado == "A")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                if (filterField.ToLower() == "nombre")
                    query = query.Where(x => x.Nombre.Contains(filter));
                else if (filterField.ToLower() == "descripcion")
                    query = query.Where(x => x.Descripcion.Contains(filter));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var results = await query
                .OrderByDescending(x => x.FechaCreacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new MetaPnResponse
                {
                    MetaPnId = x.MetaPnId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion,
                    FechaModificacion = x.FechaModificacion
                })
                .ToListAsync();

            return new PaginatedResponse<MetaPnResponse>
            {
                Data = results,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalCount
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateMetaPnRequest request)
        {
            var meta = await _context.MetasPlanNacionalDesarrollo.FindAsync(id);
            if (meta == null || meta.Estado == "N")
                return false;

            meta.Nombre = request.Nombre;
            meta.Descripcion = request.Descripcion;
            meta.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var meta = await _context.MetasPlanNacionalDesarrollo.FindAsync(id);
            if (meta == null)
                return false;

            _context.MetasPlanNacionalDesarrollo.Remove(meta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}