using System;
using System.Collections.Generic;

namespace Industrie6
{
    public class Coordinat
    {
        // Grundkoordinaten
        public int Breite { get; set; }
        public int Hoehe { get; set; }
        public int Tiefe { get; set; }
        public int Trans { get; set; }

        // GEO-Segmente
        public List<string> HY { get; set; }
        public List<string> PE { get; set; }
        public List<string> PER { get; set; }

        // TMP-Resultierende
        public List<string> TMPa { get; set; }
        public List<string> TMPb { get; set; }
        public List<string> TMPg { get; set; }

        // Gegen-Cache
        public Dictionary<string, List<string>> GegenCache { get; set; }

        // City-System
        public string Mode { get; set; }

        public Coordinat(int v)
        {
            // Leitbahn
            Breite = v * 3;
            Hoehe = v * 9;
            Tiefe = v * 27;
            Trans = v * 81;

            // GEO-Segmente
            HY = new List<string> { "◉", "3", "9", "◎", "81", "3↺" };
            PE = new List<string> { "◉", "9", "◎", "81", "◆", "△", "▣" };
            PER = new List<string> { "3", "9", "81", "◆", "756", "△", "27", "▣", "3↺" };

            // TMP-Resultierende
            TMPa = new List<string> { "TMPa◉", "TMPa◎", "TMPa◆" };
            TMPb = new List<string> { "TMPb◉", "TMPb◎", "TMPb◆" };
            TMPg = new List<string> { "TMPg◉", "TMPg◎", "TMPg◆" };

            // Gegen-Cache (invertiert)
            GegenCache = new Dictionary<string, List<string>>
            {
                { "HY", new List<string>(HY) },
                { "PE", new List<string>(PE) },
                { "PER", new List<string>(PER) }
            };

            GegenCache["HY"].Reverse();
            GegenCache["PE"].Reverse();
            GegenCache["PER"].Reverse();

            Mode = "NEUCITY";
        }

        public override string ToString()
        {
            return $"Coordinat System (Mode={Mode})\n" +
                   $"Breite={Breite}, Hoehe={Hoehe}, Tiefe={Tiefe}, Trans={Trans}\n" +
                   $"HY=[{string.Join(",", HY)}]\n" +
                   $"PE=[{string.Join(",", PE)}]\n" +
                   $"PER=[{string.Join(",", PER)}]\n" +
                   $"TMPa=[{string.Join(",", TMPa)}]\n" +
                   $"TMPb=[{string.Join(",", TMPb)}]\n" +
                   $"TMPg=[{string.Join(",", TMPg)}]\n";
        }
    }
}
