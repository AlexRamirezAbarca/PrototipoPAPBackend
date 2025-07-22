using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IObjetivoPnService
    {
        Task<ObjetivoPnResponse> GetByIdAsync(int id);
        Task<ObjetivoPnResponse> CreateAsync(CreateObjetivoPnRequest request);
        Task<PaginatedResponse<ObjetivoPnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField);
        Task<bool> UpdateAsync(int id, CreateObjetivoPnRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
