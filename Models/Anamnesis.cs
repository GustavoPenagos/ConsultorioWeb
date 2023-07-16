using System.ComponentModel.DataAnnotations;


namespace OdontologiaWeb.Models
{
    public class Anamnesis
    {

        public string Id_Usuario { get; set; }
        public string Motivo_Consulta { get; set; }
        public string Emf_Actual { get; set; }
    }
}