namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class ObjetivoMetaPN
    {
        public int objetivo_meta_pn_id { get; set; }

        public int obj_pn_id { get; set; }
        public int meta_pn_id { get; set; }

        public string estado { get; set; } = "A";
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        public DateTime? fecha_modificacion { get; set; }

        // Relaciones de navegación opcionales
        public virtual ObjetivosPlanNacionalDesarrollo? Objetivo { get; set; }
        public virtual MetasPlanNacionalDesarrollo? Meta { get; set; }
    }
}
