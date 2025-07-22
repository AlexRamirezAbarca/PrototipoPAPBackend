namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class ObjetivoPoliticaPN
    {
        public int objetivo_politica_pn_id { get; set; }

        public int obj_pn_id { get; set; }
        public int politica_pn_id { get; set; }

        public string estado { get; set; } = "A";
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        public DateTime? fecha_modificacion { get; set; }

        public virtual ObjetivosPlanNacionalDesarrollo? Objetivo { get; set; }
        public virtual PoliticasPlanNacionalDesarrollo? Politica { get; set; }
    }
}
