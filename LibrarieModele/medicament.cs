using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class medicament
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const char SEPARATOR_SECUNDAR_FISIER = ' ';
        private const bool SUCCES = true;
        private const int ID = 0;
        private const int DENUMIRE = 1;
        private const int PRET = 2;
        private const int MOD = 3;
        private const int CATEGORII = 4;
        [Flags]
        public enum CategoriiMedicament
        {
            Analgezic = 1,
            Antipyretic = 2,
            Antiinflamator = 4,
            Antibiotic = 8,
            Antiviral = 16
        }
        public enum ModEliberare
        {
            CuPrescriptie = 1,
            FaraPrescriptie = 2
        };
        public ModEliberare me;
        private const char SEPARATOR = ' ';
        public int id { get; set; }
        public string den { get; set; }
        public int pret { get; set; }
        public int opt { get; set; }
        public string mod { get; set; }
        public CategoriiMedicament categorii { get; set; }

        public bool setopt(int opti)
        {
            if (opti == 1 || opti == 2)
            {
                opt = opti;
                ModEliberare me = (ModEliberare)opt;
                mod = me.ToString();
                return true;
            }
            else
            {
                Console.WriteLine("Optiune inexistenta, alegeti 1 sau 2!");
                return false;
            }
        }
        public bool setcat(string opti)
        {
            string[] optiuni = opti.Split(SEPARATOR);
            foreach (string opt in optiuni)
            {
                if (Enum.TryParse(opt, true, out CategoriiMedicament categorie))
                {
                    if (!Enum.IsDefined(typeof(CategoriiMedicament), categorie))
                    {
                        return false;
                    }
                    categorii |= categorie;
                }
                else
                {
                    Console.WriteLine($"Numar invalid: {opt}");
                    return false;
                }
            }
            return true;
        }
        public medicament()
        {
            id = 0;
            den = string.Empty;
            pret = 0;
        }

        public medicament(int id, string den, int pret)
        {
            this.id = id;
            this.den = den;
            this.pret = pret;
        }
        public medicament(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);
            id = int.Parse(date[ID]);
            den = date[DENUMIRE];
            pret = int.Parse(date[PRET]);
            opt = int.Parse(date[MOD]);
            mod = ((ModEliberare)opt).ToString();
            categorii = (CategoriiMedicament)Enum.Parse(typeof(CategoriiMedicament), date[CATEGORII]);
        }

        public string info()
        {
            string categoriiText = (categorii == 0) ? "Niciuna" : categorii.ToString();
            return $@"--------------------------------------
ID: {id}
Denumire: {den}
Pret: {pret} Lei
Mod Eliberare: {mod}
Categorii: {categoriiText}
--------------------------------------";
        }

        public string ConvertToFileFormat()
        {
            string categoriiText = categorii.ToString();
            string modText = ((ModEliberare)opt).ToString();
            string linieFisier = string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}",
                id, 
                SEPARATOR_PRINCIPAL_FISIER,
                den, 
                pret, 
                opt, 
                categoriiText);
            return linieFisier;
        }
    }
}
