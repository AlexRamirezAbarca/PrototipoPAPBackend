namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class ObjetivoPnResponse
    {
        public int ObjPnId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
