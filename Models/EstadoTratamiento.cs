using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class EstadoTratamiento
    {
        public string Id_Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Diente { get; set; }
        public string Trata_Efectuado { get; set; }
        public string Doctor { get; set; }
        public string Firma { get; set; }
    }
}