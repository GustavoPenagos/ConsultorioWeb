using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class EstadoTratamiento
    {
        public long IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Diente { get; set; }
        public string TrataEfectuado { get; set; }
        public string Doctor { get; set; }
        public string Firma { get; set; }
        public DateTime Atencion { get; set; } = DateTime.Now;
    }
}