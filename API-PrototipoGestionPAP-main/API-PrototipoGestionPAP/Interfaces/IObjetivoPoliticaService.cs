using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;

namespace API_PrototipoGestionPAP.Interfaces
{
    public interface IObjetivoPoliticaService
    {
        Task<ObjetivoPoliticaResponse?> GetByIdAsync(int id);
        Task<PaginatedResponse<ObjetivoPoliticaResponse>> GetAllPaginatedAsync(int page, int pageSize);
        Task<ObjetivoPoliticaResponse> CreateAsync(CreateObjetivoPoliticaRequest request);
        Task<bool> UpdateAsync(int id, CreateObjetivoPoliticaRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
