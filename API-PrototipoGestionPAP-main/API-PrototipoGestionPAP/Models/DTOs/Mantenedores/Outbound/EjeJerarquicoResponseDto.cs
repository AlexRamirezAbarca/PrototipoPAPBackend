namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class EjeJerarquicoResponseDto
    {
        public int EjePnId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ObjetivoJerarquicoDto> Objetivos { get; set; } = new();
    }

    public class ObjetivoJerarquicoDto
    {
        public int ObjPnId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<PoliticaDto> Politicas { get; set; } = new();
        public List<MetaDto> Metas { get; set; } = new();
    }

    public class PoliticaDto
    {
        public int PoliticaPnId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class MetaDto
    {
        public int MetaPnId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
