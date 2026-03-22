using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testpiu
{
    public class medicament
    {
        public int id { get; set; }
        public string den { get; set; }
        public int pret { get; set; }

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
            return $"Id: {id}, Denumire: {den}, Pret: {pret} Lei";
        }


    }
}
