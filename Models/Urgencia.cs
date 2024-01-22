using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioWeb.Models
{
    public class Urgencia
    {
        public DateTime Fecha_URG { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string IdDocumento { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FechaNacido { get; set; }
        public int Edad { get; set; }
        public int IdCiudad { get; set; }
        public int IdGenero { get; set;}
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NombreAcudiente { get;set; }
        public string Motivo {  get; set; }
        public string Diagnostico { get; set; }
        public string Enf_Actual { get; set;}
        public string Ant_General { get; set; }
        public string Medicamento { get; set;}
        public string Trata_ejectado { get; set; }
        public string Rem_Especialista { get; set; }
        public string Autorizacion { get; set;}

    }
}