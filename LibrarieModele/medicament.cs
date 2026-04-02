using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class medicament
    {
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


    }
}
