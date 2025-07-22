namespace API_PrototipoGestionPAP.Application.DTOs.Outbound
{
    public class ObjetivoMetaPnResponse
    {
        public int objetivo_meta_pn_id { get; set; }
        public int obj_pn_id { get; set; }
        public int meta_pn_id { get; set; }
        public string estado { get; set; } = "A";
        public DateTime fecha_creacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
    }
}
