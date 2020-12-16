using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_With_Authorization.Models
{
    public class Module
    {
        public int Modulnummer { get; set; }
        public int ID { get; set; }

        public string Modulname { get; set; }


        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

       
        

        public int Semester { get; set; }

        public int Workload { get; set; }

        public int Semesterwochenstunden { get; set; }
        public int Leistungspunkte { get; set; }

        public string Turnus { get; set; }
        public string Curriculare_Zuordnung { get; set; }
        public string Lernziele { get; set; }
        public string Schlüsselqualifikationen { get; set; }
        public string Inhaltliche_Beschreibung { get; set; }
        public string Unterrichtsform { get; set; }
        public string Prüfungsart { get; set; }
        public string Prüfungsform { get; set; }
        public string Verbindlichkeit { get; set; }
        public string Literaturangaben { get; set; }

        public ModuleStatus Status { get; set; }
        public string OwnerID { get; set; }

    }

    public enum ModuleStatus
    {
        Submitted,
        Approved,
        Rejected
    }

}
