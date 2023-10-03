using System;
using System.ComponentModel.DataAnnotations;


namespace OdontologiaWeb.Models
{
    public class Anamnesis
    {

        public long IdUsuario { get; set; }
        public string MotivoConsulta { get; set; }
        public string EmferActual { get; set; }
        public DateTime Atencion { get; set; }
    }
}