using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IMetaPnService
    {
        Task<MetaPnResponse?> GetByIdAsync(int id);
        Task<PaginatedResponse<MetaPnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField);
        Task<MetaPnResponse> CreateAsync(CreateMetaPnRequest request);
        Task<bool> UpdateAsync(int id, CreateMetaPnRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
