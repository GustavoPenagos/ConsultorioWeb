using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class Contabilidad
    {
        public int Id { get; set; }
        public long IdUsuario { get; set; }
        public double Valor { get; set; }
        public double Abono { get; set; }
        public double Saldo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}