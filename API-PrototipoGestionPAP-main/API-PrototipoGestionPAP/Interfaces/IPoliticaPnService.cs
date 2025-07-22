using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IPoliticaPnService
    {
        Task<PoliticaPnResponse> GetByIdAsync(int id);
        Task<PoliticaPnResponse> CreateAsync(CreatePoliticaPnRequest request);
        Task<PaginatedResponse<PoliticaPnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField);
        Task<bool> UpdateAsync(int id, CreatePoliticaPnRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
