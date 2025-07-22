using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IEjePnService
    {
        Task<EjePnResponse> GetByIdAsync(int id);
        Task<EjePnResponse> CreateAsync(CreateEjePnRequest request);
        Task<PaginatedResponse<EjePnResponse>> GetAllPaginatedAsync(int page, int pageSize, string? filter, string? filterField);
        Task<GeneralResponse<object>> UpdateAsync(int id, CreateEjePnRequest request);
        Task<GeneralResponse<object>> DeleteAsync(int id);
    }
}
