using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontologiaWeb.Models
{
    public class Ciudad
    {

        public int IdCiudad { get; set; }
        public string Municipio { get; set; }
        public int Estado { get; set; }
        public int IdDepartamento { get; set; }
    }
}