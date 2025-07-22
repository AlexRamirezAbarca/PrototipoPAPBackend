namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class PoliticaPnResponse
    {
        public int PoliticaPnId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
