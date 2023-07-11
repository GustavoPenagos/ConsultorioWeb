using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdontologiaWeb.Models
{
    public class Anamnesis
    {
        public Usuario Id_Usuario { get; set; }
        public string Motivo_Consulta { get; set; }
        public string Emf_Actual { get; set; }
        public int Sinusitis { get; set; }
        public int Diabetes { get; set; }
        public int Hepatitis { get; set; }
        public int Cardiopatias { get; set; }
        public int Trata_Medico { get; set; }
        public int Hipertension { get; set; }
        public int Trans_Neumologico { get; set; }
        public int Organos_Sentidos { get; set; }
        public int Infecciones { get; set; }
        public int Trans_Gastricos { get; set; }
        public int Fieb_Reumatica { get; set; }
        public int Enf_Respiratoria { get; set; }
        public int Alt_Coagulatorias { get; set; }
    }
}