namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class ObjetivoPoliticaResponseDto
    {
        public int ObjetivoPoliticaPnId { get; set; }

        public int ObjPnId { get; set; }
        public string ObjPnNombre { get; set; } = string.Empty;

        public int PoliticaPnId { get; set; }
        public string PoliticaPnNombre { get; set; } = string.Empty;

        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; }
    }
}
