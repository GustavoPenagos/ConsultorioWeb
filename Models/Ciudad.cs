using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontologiaWeb.Models
{
    public class Ciudad
    {

        public int Id_Ciudad { get; set; }
        public string Municipio { get; set; }
        public int Estado { get; set; }
        public int Id_Departamento { get; set; }
    }
}