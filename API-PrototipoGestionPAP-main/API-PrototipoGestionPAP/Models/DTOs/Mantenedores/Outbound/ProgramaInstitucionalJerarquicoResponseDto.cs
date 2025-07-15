namespace API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound
{
    public class ProgramaInstitucionalJerarquicoResponseDto
    {
        public int ProgramaInstId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ProgramaPresupuestarioJerarquicoDto> ProgramasPresupuestarios { get; set; } = new();
    }

    public class ProgramaPresupuestarioJerarquicoDto
    {
        public int ProgramaPreId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<ProductoInstitucionalDto> Productos { get; set; } = new();
    }

    public class ProductoInstitucionalDto
    {
        public int ProductoInstId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }



}
