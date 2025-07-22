using System.ComponentModel.DataAnnotations;

namespace API_PrototipoGestionPAP.Models.Mantenedores
{
    public class EjeObjetivoPN
    {
        public int eje_objetivo_pn_id { get; set; }

        public int eje_pn_id { get; set; }
        public int obj_pn_id { get; set; }

        public string estado { get; set; } = "A";
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        public DateTime? fecha_modificacion { get; set; }

        // Relaciones de navegación opcionales
        public virtual EjesPlanNacionalDesarrollo? Eje { get; set; }
        public virtual ObjetivosPlanNacionalDesarrollo? Objetivo { get; set; }
    }

}
