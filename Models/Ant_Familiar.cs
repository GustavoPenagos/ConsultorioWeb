using System;
using System.ComponentModel.DataAnnotations;


namespace OdontologiaWeb.Models
{
    public class Ant_Familiar
    {
        public long IdUsuario { get; set; }
        public int Cancer { get; set; }
        public int Sinusitis { get; set; }
        public int OrganosSentidos { get; set; }
        public int Diabetes { get; set; }
        public int Infecciones { get; set; }
        public int Hepatitis { get; set; }
        public int Epilepsia { get; set; }
        public int TransGastricos { get; set; }
        public int Cardiopatias { get; set; }
        public int FiebReumatica { get; set; }
        public int TrataMedico { get; set; }
        public int EnferRespiratoria { get; set; }
        public int Hipertension { get; set; }
        public int AltCoagulatorias { get; set; }
        public int TransNeumologico { get; set; }
        public int TenArterial { get; set; }
        public string Otros { get; set; }
        public int Embarazo { get; set; }
        public int Meses { get; set; }
        public int Lactancia { get; set; }
        public int FreCepillado { get; set; }
        public int CedaDental { get; set; }
        public string Observaciones { get; set; }
        public DateTime Atencion { get; set; }
    }
}