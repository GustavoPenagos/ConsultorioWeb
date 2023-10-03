using System;

namespace OdontologiaWeb.Models
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public int IdDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacido { get; set; }
        public int EstadoCivil { get; set; }
        public string Ocupacion { get; set; }
        public string Aseguradora { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdGenero { get; set; }
        public int IdCiudad { get; set; }
        public int IdDepartamento { get; set; }
        public string Oficina { get; set; }
        public string NombreAcudiente { get; set; }
        public string Referido { get; set; }
        public string Observaciones { get; set; }
        
        
    }
}