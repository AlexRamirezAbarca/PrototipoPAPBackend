namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class ObjetivoPoliticaResponse
    {
        public int ObjetivoPoliticaPnId { get; set; }
        public int ObjPnId { get; set; }
        public int PoliticaPnId { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
