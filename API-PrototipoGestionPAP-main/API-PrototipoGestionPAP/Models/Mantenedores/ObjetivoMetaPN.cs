namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class ObjetivoMetaPN
    {
        public int ObjetivoMetaPnId { get; set; }
        public int ObjPnId { get; set; }
        public int MetaPnId { get; set; }

        public string Estado { get; set; } = "A";
        public string? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Relaciones de navegación
        public virtual ObjetivosPlanNacionalDesarrollo? Objetivo { get; set; }
        public virtual MetasPlanNacionalDesarrollo? Meta { get; set; }
    }
}
