namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class ProgramaInstitucionalPresupuestario
    {
        public int ProgramaInstitucionalPresupuestarioId { get; set; }

        public int ProgramaInstId { get; set; }
        public int ProgramaPreId { get; set; }

        public string Estado { get; set; } = "A";
        public string? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Relaciones (opcional si quieres usar navegación)
        public virtual ProgramasInstitucionales? ProgramaInstitucional { get; set; }
        public virtual ProgramasPresupuestarios? ProgramaPresupuestario { get; set; }
    }
}
