using LibrarieModele;
namespace NivelStocareDate
{
    public class AdministrareMedicamente : IStocareData
    {
        private List<medicament> meds;

        public AdministrareMedicamente()
        {
            meds = new List<medicament>();
        }

        public void AddMed(medicament med)
        {
            med.id = GetNextIdMed();
            meds.Add(med);
        }

        public List<medicament> GetMeds()
        {
            return meds;
        }

        public medicament? GetMed(int idMed)
        {
            foreach (medicament med in meds)
            {
                if (med.id == idMed)
                {
                    return med;
                }
            }

            return null;
        }

        public medicament? GetMed(string _den)
        {
            return meds?.FirstOrDefault(med =>
                med.den.Equals(_den, StringComparison.OrdinalIgnoreCase)
            );
        }
        public List<medicament> GetMedsCat(medicament.CategoriiMedicament cat)
        {
            var medsel = from med in meds
                         where (med.categorii & cat) == cat
                         select med;
            return medsel.ToList();
        }

        public bool UpdateMedicament(medicament s)
        {
            throw new Exception("Optiunea UpdateMedicament nu este implementata");
        }

        public int GetNextIdMed()
        {
            if (meds.Count == 0)
            {
                return 1;
            }

            return meds.Last().id + 1;
        }
    }
}
