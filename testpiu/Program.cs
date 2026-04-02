using System.Security.Cryptography.X509Certificates;
using LibrarieModele;
using NivelStocareDate;

namespace testpiu
{
    class Program
    {
        static IStocareData adminMed = StocareFactory.GetAdministratorStocare();
        public static void Main()
        {
            List<medicament> meds = adminMed.GetMeds();
            medicament med = null;
            string opt;
            do
            {
                Console.WriteLine("C. Citire informatii medicament de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul medicament introdus");
                Console.WriteLine("A. Afisare medicamente din lista");
                Console.WriteLine("S. Salvare medicament in lista");
                Console.WriteLine("F. Cauta medicament");
                Console.WriteLine("G. Cauta medicamente dupa categorii");
                Console.WriteLine("U. Actualizare medicament");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("Alegeti o optiune");
                opt = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (opt)
                {
                    case "C":
                        med = CitireMedicamentTastatura();
                        break;

                    case "I":
                        AfisareMedicament(med);
                        break;

                    case "A":
                        meds = adminMed.GetMeds();
                        AfisareMedicamente(meds);
                        break;

                    case "S":
                        if (med != null)
                        {
                            adminMed.AddMed(med);
                            Console.WriteLine("Medicament salvat.");
                        }
                        else
                        {
                            Console.WriteLine("Nu exista medicament de salvat. Cititi unul mai intai.");
                        }
                        break;

                    case "F":
                        GetMedicament();
                        break;

                    case "G":
                        GetMedsCat();
                        break;

                    case "U":
                        UpdateMedicament();
                        break;

                    case "X":
                        Console.WriteLine("Aplicatia va fi inchisa");
                        return;

                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }

            } while (opt.ToUpper() != "X");

        }
        public static medicament CitireMedicamentTastatura()
        {
            Console.WriteLine("Introduceti denumirea si pretul medicamentului:");
            string den = Console.ReadLine();
            string pretstring = Console.ReadLine();
            int.TryParse(pretstring, out int pret);
            medicament med = new medicament(0, den, pret);
            Console.WriteLine("Introduceti modul de eliberare \n" +
                "1 - Cu Prescriptie\n" +
                "2 - Fara Prescriptie:");
            bool validInput = false;
            while (!validInput)
            {
                int opt = Convert.ToInt32(Console.ReadLine());
                validInput = med.setopt(opt);
            }
            Console.WriteLine("Selectati categoriile (introduceti numerele dorite separate prin spatiu):");
            Console.WriteLine("1 - Analgezic");
            Console.WriteLine("2 - Antipyretic");
            Console.WriteLine("4 - Antiinflamator");
            Console.WriteLine("8 - Antibiotic");
            Console.WriteLine("16 - Antiviral");
            bool validCatInput = false;
            while (!validCatInput)
            {
                string catString = Console.ReadLine();
                validCatInput = med.setcat(catString);
                if (!validCatInput)
                {
                    Console.WriteLine("Introduceti categoriile corect!");
                }
            }

            return med;
        }
        public static void AfisareMedicament(medicament? med)
        {
            Console.WriteLine(med?.info());
        }
        public static void AfisareMedicamente(List<medicament> meds)
        {
            Console.WriteLine("Medicamentele sunt:");

            foreach (medicament med in meds)
            {
                AfisareMedicament(med);
            }
        }
        public static void GetMedsCat()
        {
            Console.WriteLine("Introduceti categoria/categoriile cautate:");
            string catString = Console.ReadLine();
            medicament tempmed = new medicament();
            tempmed.setcat(catString);
            List<medicament> meds = adminMed.GetMedsCat(tempmed.categorii);
            if (meds.Count == 0)
            {
                Console.WriteLine("Nu exista medicamente care sa corespunda categoriilor cautate.");
            }
            else
            {
                Console.WriteLine("Medicamentele care corespund categoriilor cautate sunt:");
                {
                    AfisareMedicamente(meds);
                }
            }

        }
        public static void GetMedicament()
        {
            medicament md = null;
            Console.WriteLine("Introduceti Denumirea, spatiu gol pentru id");
            string denum = Console.ReadLine();
            if (string.IsNullOrEmpty(denum))
            {
                Console.WriteLine("Introduceti ID");
                string idString = Console.ReadLine();
                int.TryParse(idString, out int id);
                md = adminMed.GetMed(id);
            }
            else
            {
                md = adminMed.GetMed(denum);
            }
            if (md == null)
                Console.WriteLine("Medicamentul nu a fost introdus");
            else
                AfisareMedicament(md);
        }
        public static void UpdateMedicament()
        {
            Console.WriteLine("Introduceti ID-ul medicamentului de actualizat:");
            string idString = Console.ReadLine();
            int.TryParse(idString, out int id);
            medicament? med = adminMed.GetMed(id);
            if (med == null)
            {
                Console.WriteLine("Medicamentul cu ID-ul specificat nu a fost gasit.");
                return;
            }
            Console.WriteLine("Introduceti noua denumire (lasa gol pentru a pastra denumirea curenta):");
            string newDen = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDen))
            {
                med.den = newDen;
            }
            Console.WriteLine("Introduceti noul pret (lasa gol pentru a pastra pretul curent):");
            string newPretString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPretString) && int.TryParse(newPretString, out int newPret))
            {
                med.pret = newPret;
            }
            Console.WriteLine("Introduceti noul mod de eliberare (1 - Cu Prescriptie, 2 - Fara Prescriptie, lasa gol pentru a pastra modul curent):");
            string newOptString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newOptString) && int.TryParse(newOptString, out int newOpt))
            {
                if (med.setopt(newOpt))
                {
                }
                else
                {
                    Console.WriteLine("Mod de eliberare invalid. Modul curent a fost pastrat.");
                }
            }
            Console.WriteLine("Introduceti noile categorii (introduceti numerele dorite separate prin spatiu, lasa gol pentru a pastra categoriile curente):");
            string newCatString = Console.ReadLine();
            if (!string.IsNullOrEmpty(newCatString))
            {
                if (med.setcat(newCatString))
                {
                }
                else
                {
                    Console.WriteLine("Categorii invalide. Categoriile curente au fost pastrate.");
                }
            }
            bool success = adminMed.UpdateMedicament(med);
            if (success)
                Console.WriteLine("Medicament actualizat cu succes.");
            else
                Console.WriteLine("Eroare la actualizarea medicamentului.");
        }
    }
}