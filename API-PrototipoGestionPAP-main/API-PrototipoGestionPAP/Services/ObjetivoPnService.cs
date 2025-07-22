using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_PrototipoGestionPAP.Services
{
    public class ObjetivoPnService : IObjetivoPnService
    {
        private readonly DBContext _context;

        public ObjetivoPnService(DBContext context)
        {
            _context = context;
        }

        public async Task<ObjetivoPnResponse> GetByIdAsync(int id)
        {
            var obj = await _context.ObjetivosPlanNacionalDesarrollo.FindAsync(id);
            if (obj == null || obj.Estado == "N") return null;

            return new ObjetivoPnResponse
            {
                ObjPnId = obj.ObjPnId,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
                Estado = obj.Estado,
                FechaCreacion = obj.FechaCreacion
            };
        }

        public async Task<ObjetivoPnResponse> CreateAsync(CreateObjetivoPnRequest request)
        {
            var obj = new ObjetivosPlanNacionalDesarrollo
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = "A",
                FechaCreacion = DateTime.UtcNow
            };

            _context.ObjetivosPlanNacionalDesarrollo.Add(obj);
            await _context.SaveChangesAsync();

            return new ObjetivoPnResponse
            {
                ObjPnId = obj.ObjPnId,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
                Estado = obj.Estado,
                FechaCreacion = obj.FechaCreacion
            };
        }

        public async Task<PaginatedResponse<ObjetivoPnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField)
        {
            var query = _context.ObjetivosPlanNacionalDesarrollo
                .Where(x => x.Estado != "N")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                var parameter = Expression.Parameter(typeof(ObjetivosPlanNacionalDesarrollo), "x");
                var property = Expression.PropertyOrField(parameter, filterField);

                if (property.Type != typeof(string))
                    throw new Exception("Solo se puede filtrar por propiedades tipo string");

                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                var value = Expression.Constant(filter);
                var contains = Expression.Call(property, containsMethod, value);
                var lambda = Expression.Lambda<Func<ObjetivosPlanNacionalDesarrollo, bool>>(contains, parameter);

                query = query.Where(lambda);
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page > totalPages && totalPages > 0)
                page = totalPages;

            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ObjetivoPnResponse
                {
                    ObjPnId = x.ObjPnId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();

            return new PaginatedResponse<ObjetivoPnResponse>
            {
                Data = result,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateObjetivoPnRequest request)
        {
            var obj = await _context.ObjetivosPlanNacionalDesarrollo.FindAsync(id);
            if (obj == null || obj.Estado == "N") return false;

            obj.Nombre = request.Nombre;
            obj.Descripcion = request.Descripcion;
            obj.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var objetivo = await _context.ObjetivosPlanNacionalDesarrollo.FindAsync(id);
                if (objetivo == null)
                    return false;

                _context.ObjetivosPlanNacionalDesarrollo.Remove(objetivo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }


    }
}
