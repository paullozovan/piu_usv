namespace testpiu
{
    class Program
    {
        static void Main()
        {
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
    }
}