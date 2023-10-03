using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class Citas
    {
        public long IdUsuario { get; set; }
        public DateTime FechaCita { get; set; }
        public string HoraCita { get; set; }
    }
}