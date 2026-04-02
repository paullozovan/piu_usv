using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        public void AddMed(medicament med);
        public List<medicament> GetMeds();
        public medicament? GetMed(int idMed);
        public medicament? GetMed(string _den);
        public List<medicament> GetMedsCat(medicament.CategoriiMedicament cat);
        public bool UpdateMedicament(medicament s);
    }
}
