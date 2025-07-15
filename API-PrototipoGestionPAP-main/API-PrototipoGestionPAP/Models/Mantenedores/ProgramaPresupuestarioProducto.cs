namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class ProgramaPresupuestarioProducto
    {
        public int ProgPreProdId { get; set; }

        public int ProgramaPreId { get; set; }
        public int ProductoInstId { get; set; }

        public string Estado { get; set; } = "A";
        public string? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual ProgramasPresupuestarios? ProgramaPresupuestario { get; set; }
        public virtual ProductosInstitucionales? ProductoInstitucional { get; set; }
    }
}
