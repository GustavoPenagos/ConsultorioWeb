using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdontologiaWeb.Models
{
    public class Ant_Familiar
    {
        public Usuario Id_Usuario { get; set; }
        public int Cancer { get; set; }
        public int Diabetes { get; set; }
        public int Ten_Arterial { get; set; }
        public string Otros { get; set; }
        public int Embarazo { get; set; }
        public int Meses { get; set; }
        public int Lactancia { get; set; }
        public int Fre_Cepillado { get; set; }
        public int Ceda_Dental { get; set; }
        public string Observaciones { get; set; }
    }
}