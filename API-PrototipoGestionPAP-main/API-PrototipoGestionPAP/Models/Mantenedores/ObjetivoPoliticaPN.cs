namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class ObjetivoPoliticaPN
    {
        public int ObjetivoPoliticaPnId { get; set; }
        public int ObjPnId { get; set; }
        public int PoliticaPnId { get; set; }

        public string Estado { get; set; } = "A";
        public string? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Relaciones de navegación (opcional pero recomendado)
        public virtual ObjetivosPlanNacionalDesarrollo? Objetivo { get; set; }
        public virtual PoliticasPlanNacionalDesarrollo? Politica { get; set; }
    }
}
