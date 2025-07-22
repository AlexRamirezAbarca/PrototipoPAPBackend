using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services
{
    public class PoliticaPnService : IPoliticaPnService
    {
        private readonly DBContext _context;

        public PoliticaPnService(DBContext context)
        {
            _context = context;
        }

        public async Task<PoliticaPnResponse> GetByIdAsync(int id)
        {
            var politica = await _context.PoliticasPlanNacionalDesarrollo.FindAsync(id);
            if (politica == null || politica.Estado == "N")
                return null!;

            return new PoliticaPnResponse
            {
                PoliticaPnId = politica.PoliticaPnId,
                Nombre = politica.Nombre,
                Descripcion = politica.Descripcion,
                Estado = politica.Estado,
                FechaCreacion = politica.FechaCreacion,
                FechaModificacion = politica.FechaModificacion
            };
        }

        public async Task<PoliticaPnResponse> CreateAsync(CreatePoliticaPnRequest request)
        {
            var politica = new PoliticasPlanNacionalDesarrollo
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = "A",
                FechaCreacion = DateTime.UtcNow
            };

            _context.PoliticasPlanNacionalDesarrollo.Add(politica);
            await _context.SaveChangesAsync();

            return new PoliticaPnResponse
            {
                PoliticaPnId = politica.PoliticaPnId,
                Nombre = politica.Nombre,
                Descripcion = politica.Descripcion,
                Estado = politica.Estado,
                FechaCreacion = politica.FechaCreacion
            };
        }

        public async Task<PaginatedResponse<PoliticaPnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField)
        {
            var query = _context.PoliticasPlanNacionalDesarrollo
                .Where(x => x.Estado == "A")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                if (filterField.ToLower() == "nombre")
                    query = query.Where(x => x.Nombre.Contains(filter));
                else if (filterField.ToLower() == "descripcion")
                    query = query.Where(x => x.Descripcion.Contains(filter));
            }

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.FechaCreacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(politica => new PoliticaPnResponse
                {
                    PoliticaPnId = politica.PoliticaPnId,
                    Nombre = politica.Nombre,
                    Descripcion = politica.Descripcion,
                    Estado = politica.Estado,
                    FechaCreacion = politica.FechaCreacion,
                    FechaModificacion = politica.FechaModificacion
                })
                .ToListAsync();

            return new PaginatedResponse<PoliticaPnResponse>
            {
                TotalRecords = total,
                Data = data
            };
        }

        public async Task<bool> UpdateAsync(int id, CreatePoliticaPnRequest request)
        {
            var politica = await _context.PoliticasPlanNacionalDesarrollo.FindAsync(id);
            if (politica == null || politica.Estado == "N")
                return false;

            politica.Nombre = request.Nombre;
            politica.Descripcion = request.Descripcion;
            politica.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var politica = await _context.PoliticasPlanNacionalDesarrollo.FindAsync(id);
            if (politica == null)
                return false;

            _context.PoliticasPlanNacionalDesarrollo.Remove(politica);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
