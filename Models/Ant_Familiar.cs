using System;
using System.ComponentModel.DataAnnotations;


namespace OdontologiaWeb.Models
{
    public class Ant_Familiar
    {
        public long Id_Usuario { get; set; }
        public int Cancer { get; set; }
        public int Sinusitis { get; set; }
        public int Organos_Sentidos { get; set; }
        public int Diabetes { get; set; }
        public int Infecciones { get; set; }
        public int Hepatitis { get; set; }
        public int Trans_Gastricos { get; set; }
        public int Cardiopatias { get; set; }
        public int Fieb_Reumatica { get; set; }
        public int Trata_Medico { get; set; }
        public int Enf_Respiratoria { get; set; }
        public int Hipertension { get; set; }
        public int Alt_Coagulatorias { get; set; }
        public int Trans_Neumologico { get; set; }
        public int Ten_Arterial { get; set; }
        public string Otros { get; set; }
        public int Embarazo { get; set; }
        public int Meses { get; set; }
        public int Lactancia { get; set; }
        public int Fre_Cepillado { get; set; }
        public int Ceda_Dental { get; set; }
        public string Observaciones { get; set; }
        public DateTime Atencion { get; set; }
    }
}