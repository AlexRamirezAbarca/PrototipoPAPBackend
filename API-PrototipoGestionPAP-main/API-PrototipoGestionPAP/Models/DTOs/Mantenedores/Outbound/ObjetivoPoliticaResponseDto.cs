namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class ObjetivoPoliticaResponseDto
    {
        public int ObjetivoPoliticaPnId { get; set; }
        public int ObjPnId { get; set; }
        public int PoliticaPnId { get; set; }
        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; }
    }
}
