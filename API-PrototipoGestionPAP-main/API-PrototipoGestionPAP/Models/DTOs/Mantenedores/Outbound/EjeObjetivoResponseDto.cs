namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class EjeObjetivoResponseDto
    {
        public int EjeObjetivoPnId { get; set; }

        public int EjePnId { get; set; }
        public string EjeNombre { get; set; } = string.Empty;

        public int ObjPnId { get; set; }
        public string ObjetivoNombre { get; set; } = string.Empty;

        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; }
    }

}
