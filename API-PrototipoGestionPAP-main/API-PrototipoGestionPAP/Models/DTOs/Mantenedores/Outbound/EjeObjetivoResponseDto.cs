namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class EjeObjetivoResponseDto
    {
        public int EjeObjetivoPnId { get; set; }
        public int EjePnId { get; set; }
        public int ObjPnId { get; set; }
        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; }
    }
}
