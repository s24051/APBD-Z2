using kontenery_cli;
using kontenery_cli.Modele;
using kontenery_cli.Wyjatki;

class Program
{
    public static int Main()
    {
        bool run = true;
        Manager menago = new Manager();
        Tester.MockRequirements(menago);
        return 0;
        while (run)
        {
            Console.WriteLine("Lista kontenerowców:");
            menago.GetShips();
            
            Console.WriteLine("Lista Kontenerów:");
            menago.GetContainers();
            
            Console.WriteLine("Wybierz Opcję:");
            Console.WriteLine("0. Wyjście");
            Console.WriteLine("1. Dodaj kontenerowiec");
            Console.WriteLine("2. Edytuj kontenerowiec");
            Console.WriteLine("3. Dodaj Kontener");
            Console.WriteLine("4. Edytuj Kontener");
            
            string option = Console.ReadLine();
            switch (option)
            {
                case "0":
                    run = false;
                    break;
            }
        }
        Console.WriteLine("Zamykanie...");
        return 0;
    }
}