namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class EjeObjetivoPN
    {
        public int EjeObjetivoPnId { get; set; }

        public int EjePnId { get; set; }
        public int ObjPnId { get; set; }

        public string Estado { get; set; } = "A";
        public int? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }


        // Relaciones con navegación
        public virtual EjesPlanNacionalDesarrollo Eje { get; set; } = null!;
        public virtual ObjetivosPlanNacionalDesarrollo Objetivo { get; set; } = null!;
    }

}
