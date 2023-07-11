using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdontologiaWeb.Models
{
    public class Ciudad
    {
        public int Id_Ciudad { get; set; }
        public string Name { get; set; }
        public int Estado { get; set; }
        public List<Departamento> Id_Departamento { get; set; }
    }
}