namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class ProgramaPresupuestarioProductoResponseDto
    {
        public int Id { get; set; }

        public int ProgramaPreId { get; set; }
        public string ProgramaPreNombre { get; set; }

        public int ProductoInstId { get; set; }
        public string ProductoInstNombre { get; set; }

        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
