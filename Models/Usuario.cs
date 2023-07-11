using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdontologiaWeb.Models
{
    public class Usuario
    {
        public int Id_ClId_Usuario { get; set; }
        public int Id_Documento { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime Fecha_Nacido { get; set; }
        public int Id_Genero { get; set; }
        public int Id_Ciudad { get; set; }
        public int Id_Departamento { get; set; }
    }
}