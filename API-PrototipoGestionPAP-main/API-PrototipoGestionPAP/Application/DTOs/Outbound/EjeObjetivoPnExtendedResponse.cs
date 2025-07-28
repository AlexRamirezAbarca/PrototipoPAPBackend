namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class EjeObjetivoPnExtendedResponse
    {
        public int EjeObjetivoPnId { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public EjeSimpleDto Eje { get; set; }
        public ObjetivoExtendidoDto Objetivo { get; set; }
    }

    public class EjeSimpleDto
    {
        public int EjePnId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class ObjetivoExtendidoDto
    {
        public int ObjPnId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public List<MetaSimpleDto> Metas { get; set; }
        public List<PoliticaSimpleDto> Politicas { get; set; }
    }

    public class MetaSimpleDto
    {
        public int MetaPnId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class PoliticaSimpleDto
    {
        public int PoliticaPnId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

}
