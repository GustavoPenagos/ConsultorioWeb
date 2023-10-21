using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class Imagenes
    {
        public int IdUsuario { get; set; }
        public string Imagen { get; set; }
        public DateTime Fecha_Carga { get; set; } = DateTime.Now;
    }
}