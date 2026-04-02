using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministareFisierTxt : IStocareData
    {
        private string NUME_FISIER;
        private const char SEPARATOR = ' ';

        public AdministareFisierTxt(string numeFisier)
        {
            NUME_FISIER = numeFisier;
            Stream sw = File.Open(NUME_FISIER, FileMode.OpenOrCreate);
            sw.Close();
        }
        public void AddMed(medicament med)
        {
            med.id = GetNextId();
            using (StreamWriter sw = new StreamWriter(NUME_FISIER, true))
            {
                sw.WriteLine(med.ConvertToFileFormat());

            }

        }

        public List<medicament> GetMeds()
        {
            List<medicament> meds = new List<medicament>();
            try
            {
                if (File.Exists(NUME_FISIER))
                {
                    using (StreamReader sr = new StreamReader(NUME_FISIER))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;
                            medicament med = new medicament(line);
                            meds.Add(med);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citirea din fisier: {ex.Message}");
            }
            return meds;
        }
        public medicament? GetMed(int idMed)
        {
            List<medicament> meds = GetMeds();
            return meds.FirstOrDefault(med => med.id == idMed);
        }
        public medicament? GetMed(string _den)
        {
            List<medicament> meds = GetMeds();
            return meds.FirstOrDefault(med => med.den.Equals(_den, StringComparison.OrdinalIgnoreCase));
        }
        public List<medicament> GetMedsCat(medicament.CategoriiMedicament cat)
        {
            List<medicament> meds = GetMeds();
            return meds.Where(med => (med.categorii & cat) == cat).ToList();
        }
        public bool UpdateMedicament(medicament s)
        {
            List<medicament> meds = GetMeds();
            bool found = false;
            using (StreamWriter sw = new StreamWriter(NUME_FISIER, false))
            {
                foreach (medicament med in meds)
                {
                    medicament updatedMed = med;
                    if (med.id == s.id)
                    {
                        updatedMed = s;
                    }
                    sw.WriteLine(updatedMed.ConvertToFileFormat());
                }
                found = true;
            }
            return found;
        }
        private int GetNextId()
        {
            List<medicament> meds = GetMeds();
            if (meds.Count == 0)
            {
                return 1;
            }
            else
            {
                return meds.Last().id + 1;
            }


        }
    }
}
