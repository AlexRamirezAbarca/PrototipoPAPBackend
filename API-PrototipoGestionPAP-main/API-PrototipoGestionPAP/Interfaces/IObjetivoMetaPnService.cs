using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IObjetivoMetaPnService
    {
        Task<ObjetivoMetaPnResponse?> GetByIdAsync(int id);
        Task<PaginatedResponse<ObjetivoMetaPnResponse>> GetAllPaginatedAsync(int page, int pageSize);
        Task<ObjetivoMetaPnResponse> CreateAsync(CreateObjetivoMetaPnRequest request);
        Task<bool> UpdateAsync(int id, CreateObjetivoMetaPnRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
