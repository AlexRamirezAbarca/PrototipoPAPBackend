namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class ProgramaInstitucionalPresupuestarioResponseDto
    {
        public int Id { get; set; }

        public int ProgramaInstId { get; set; }
        public string ProgramaInstNombre { get; set; }

        public int ProgramaPreId { get; set; }
        public string ProgramaPreNombre { get; set; }

        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
