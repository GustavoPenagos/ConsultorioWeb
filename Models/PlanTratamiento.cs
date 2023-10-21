using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class PlanTratamiento
    {
        public long IdUsuario { get; set; }
        public string Diagnostico { get; set; }
        public string Pronostico { get; set; }
        public string Tratamiento { get; set; }
        public DateTime Atencion { get; set; } = DateTime.Now;
    }
}