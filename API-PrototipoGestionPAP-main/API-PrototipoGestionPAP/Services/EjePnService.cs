using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_PrototipoGestionPAP.Services
{
    public class EjePnService : IEjePnService
    {
        private readonly DBContext _context;

        public EjePnService(DBContext context)
        {
            _context = context;
        }

        public async Task<EjePnResponse> GetByIdAsync(int id)
        {
            var eje = await _context.EjesPlanNacionalDesarrollo.FindAsync(id);
            if (eje == null || eje.Estado == "N")
                return null;

            return new EjePnResponse
            {
                EjePnId = eje.EjePnId,
                Nombre = eje.Nombre,
                Descripcion = eje.Descripcion,
                Estado = eje.Estado,
                FechaCreacion = eje.FechaCreacion
            };
        }

        public async Task<EjePnResponse> CreateAsync(CreateEjePnRequest request)
        {
            var eje = new EjesPlanNacionalDesarrollo
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = "A",
                FechaCreacion = DateTime.UtcNow
            };

            _context.EjesPlanNacionalDesarrollo.Add(eje);
            await _context.SaveChangesAsync();

            return new EjePnResponse
            {
                EjePnId = eje.EjePnId,
                Nombre = eje.Nombre,
                Descripcion = eje.Descripcion,
                Estado = eje.Estado,
                FechaCreacion = eje.FechaCreacion
            };
        }

        public async Task<PaginatedResponse<EjePnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField)
        {
            var query = _context.EjesPlanNacionalDesarrollo
                .Where(x => x.Estado != "N")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                var parameter = Expression.Parameter(typeof(EjesPlanNacionalDesarrollo), "x");
                var property = Expression.PropertyOrField(parameter, filterField);

                if (property.Type != typeof(string))
                    throw new Exception("Solo se puede filtrar por propiedades tipo string");

                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                var value = Expression.Constant(filter);
                var contains = Expression.Call(property, containsMethod, value);
                var lambda = Expression.Lambda<Func<EjesPlanNacionalDesarrollo, bool>>(contains, parameter);

                query = query.Where(lambda);
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (page > totalPages && totalPages > 0)
                page = totalPages;

            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new EjePnResponse
                {
                    EjePnId = x.EjePnId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();

            return new PaginatedResponse<EjePnResponse>
            {
                Data = result,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };
        }

        public async Task<GeneralResponse<object>> UpdateAsync(int id, CreateEjePnRequest request)
        {
            try
            {
                var eje = await _context.EjesPlanNacionalDesarrollo.FindAsync(id);
                if (eje == null || eje.Estado == "N")
                {
                    return new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Eje no encontrado.",
                        Data = null
                    };
                }

                eje.Nombre = request.Nombre;
                eje.Descripcion = request.Descripcion;
                eje.FechaModificacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Eje actualizado correctamente.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Ocurrió un error al actualizar el eje.",
                    Data = ex.Message
                };
            }
        }

        public async Task<GeneralResponse<object>> DeleteAsync(int id)
        {
            try
            {
                var eje = await _context.EjesPlanNacionalDesarrollo.FindAsync(id);
                if (eje == null)
                {
                    return new GeneralResponse<object>
                    {
                        Code = 404,
                        Message = "Eje no encontrado.",
                        Data = null
                    };
                }

                _context.EjesPlanNacionalDesarrollo.Remove(eje);
                await _context.SaveChangesAsync();

                return new GeneralResponse<object>
                {
                    Code = 200,
                    Message = "Eje eliminado correctamente.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<object>
                {
                    Code = 500,
                    Message = "Ocurrió un error al eliminar el eje.",
                    Data = ex.Message
                };
            }
        }

    }
}
