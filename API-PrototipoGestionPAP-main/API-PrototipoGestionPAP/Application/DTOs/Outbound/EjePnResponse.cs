namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class EjePnResponse
    {
        public int EjePnId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
