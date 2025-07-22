namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class EjeObjetivoPnResponse
    {
        public int EjeObjetivoPnId { get; set; }
        public int EjePnId { get; set; }
        public int ObjPnId { get; set; }
        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; }
    }
}
