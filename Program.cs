using System.Security.Cryptography.X509Certificates;

namespace testpiu
{
    class Program
    {
        public static List<medicament> meds = new List<medicament>();
        static void Main()
        {
            medicament med = null;
            string opt;
            do
            {
                Console.WriteLine("C. Citire informatii medicament de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul medicament introdus");
                Console.WriteLine("A. Afisare medicamente din lista");
                Console.WriteLine("S. Salvare medicament in lista");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("F. Cauta medicament");
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
                        AfisareStudenti(meds);
                        break;

                    case "S":
                        med.id = meds.Count + 1;
                        meds.Add(med);
                        Console.WriteLine("Medicament salvat.");
                        break;

                    case "F":
                        Console.WriteLine("Introduceti Denumirea");
                        string denum = Console.ReadLine();
                        medicament md = GetMedicament(denum);
                        if (md == null)
                            Console.WriteLine("Medicamentul nu a fost introdus");
                        else
                            AfisareMedicament(md);
                        break;

                    case "X":
                        Console.WriteLine("Aplicatia va fi inchisa");
                        return;

                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }

            } while (opt.ToUpper() != "X");

            Console.ReadKey();
            Console.WriteLine("Introduceti id, denumire si pretul medicamentului:");
            string idstring = Console.ReadLine();
            int.TryParse(idstring, out int id);
            string den = Console.ReadLine();
            string pretstring = Console.ReadLine();
            int.TryParse(pretstring, out int pret);
            var med1 = new medicament(id, den, pret);
            Console.WriteLine(med1.info());
            Console.ReadKey();
        }
        public static medicament CitireMedicamentTastatura()
        {
            Console.WriteLine("Introduceti denumirea si pretul medicamentului:");
            string den = Console.ReadLine();
            string pretstring = Console.ReadLine();
            int.TryParse(pretstring, out int pret);
            medicament med = new medicament(0, den, pret);
            return med;
        }
        public static void AfisareMedicament(medicament med)
        {
            Console.WriteLine(med.info());
        }
        public static void AfisareStudenti(List<medicament> meds)
        {
            Console.WriteLine("Medicamentele sunt:");

            foreach (medicament med in meds)
            {
                AfisareMedicament(med);
            }
        }
        public static medicament GetMedicament(string denum)
        {
            foreach (medicament med in meds)
            {
                if (denum == med.den)
                {
                    return med;
                }
            }
            return null;
        }
    }
}