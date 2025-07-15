namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class ObjetivoMetaResponseDto
    {
        public int ObjetivoMetaPnId { get; set; }

        public int ObjPnId { get; set; }
        public string ObjPnNombre { get; set; } = string.Empty;

        public int MetaPnId { get; set; }
        public string MetaPnNombre { get; set; } = string.Empty;

        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; }
    }
}
