namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class MetaPnResponse
    {
        public int MetaPnId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
