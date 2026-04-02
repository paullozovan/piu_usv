using System.Security.Cryptography.X509Certificates;
using LibrarieModele;
using NivelStocareDate;

namespace testpiu
{
    class Program
    {
        static AdministrareMedicamente adminMed = new AdministrareMedicamente();
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
                        AfisareMedicamente(meds);
                        break;

                    case "S":
                        med.id = meds.Count + 1;
                        meds.Add(med);
                        Console.WriteLine("Medicament salvat.");
                        break;

                    case "F":
                        GetMedicament();
                        break;

                    case "G":
                        GetMedsCat();
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
        public static void AfisareMedicament(medicament med)
        {
            Console.WriteLine(med.info());
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
    }
}