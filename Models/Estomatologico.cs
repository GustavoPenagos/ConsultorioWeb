using System;

namespace OdontologiaWeb.Models
{
    public class Estomatologico
    {
        public long IdUsuario { get; set; }
        public int Labios { get; set; }
        public int Encias { get; set; }
        public int Paladar { get; set; }
        public int Carrillos { get; set; }
        public int Lengua { get; set; }
        public int Musculos { get; set; }
        public int PisoBoca { get; set; }
        public int Orofarige { get; set; }
        public int Frenillos { get; set; }
        public int Maxilares { get; set; }
        public int GlanSalivales { get; set; }
        public DateTime Atencion { get; set; } 
    }
}