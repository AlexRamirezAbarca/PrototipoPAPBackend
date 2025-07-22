using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IEjeObjetivoPnService
    {
        Task<EjeObjetivoPnResponse?> GetByIdAsync(int id);
        Task<PaginatedResponse<EjeObjetivoPnResponse>> GetAllPaginatedAsync(int page, int pageSize);
        Task<EjeObjetivoPnResponse> CreateAsync(CreateEjeObjetivoPnRequest request);
        Task<bool> UpdateAsync(int id, CreateEjeObjetivoPnRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
