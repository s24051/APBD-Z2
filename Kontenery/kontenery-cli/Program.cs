using kontenery_cli;
using kontenery_cli.Enumy;
using kontenery_cli.Modele;
using kontenery_cli.Wyjatki;

class Program
{
    public static int Main()
    {
        bool run = true;
        Manager menago = new Manager();
        Tester.MockRequirements(menago);
        while (run)
        {
            Console.WriteLine("Lista kontenerowców:");
            foreach (var ship in menago.GetShips())
                Console.WriteLine(ship);
            
            Console.WriteLine("Lista Wolnych Kontenerów:");
            foreach (var container in menago.GetContainers())
                Console.WriteLine(container);
            
            Console.WriteLine("Wybierz Opcję:");
            Console.WriteLine("0. Wyjście");
            Console.WriteLine("1. Dodaj kontenerowiec");
            Console.WriteLine("2. Edytuj kontenerowiec");
            Console.WriteLine("3. Dodaj Kontener");
            Console.WriteLine("4. Edytuj Kontener");
            
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    AddShip(menago);
                    break;
                case "2":
                    ShipMenu(menago);
                    break;
                case "3":
                    AddContainer(menago);
                    break;
                case "4":
                    ContainerMenu(menago);
                    break;
                default:
                    run = false;
                    break;
            }
        }
        Console.WriteLine("Zamykanie...");
        return 0;
    }

    public static void AddContainer(Manager manager)
    {
        Console.WriteLine("Podaj wysokość kontenera");
        string height = Console.ReadLine();
        Console.WriteLine("Podaj długość kontenera");
        string depth = Console.ReadLine();
        Console.WriteLine("Podaj maksymalną ładowność kontenera w kg");
        string massMax = Console.ReadLine();
        Console.WriteLine("Podaj wagę pustego kontenera");
        string tare = Console.ReadLine();
        Console.WriteLine("Podaj rodzaj kontenera");
        Console.WriteLine("1. Płyn");
        Console.WriteLine("2. Lodówka");
        Console.WriteLine("3. Gaz");
        string option = Console.ReadLine();

        try
        {
            switch (option)
            {
                case "1":
                    Console.WriteLine("Czy płyn jest niebezpieczny");
                    Console.WriteLine("1. Tak");
                    Console.WriteLine("2. Nie");
                    string isHazardous = Console.ReadLine();

                    manager.AddLiquidContainer(Double.Parse(height), Double.Parse(depth),
                        Double.Parse(massMax), Double.Parse(tare), isHazardous == "1");
                    break;
                case "2":
                    Console.WriteLine("Jaki produkt będzie przewożony");
                    Console.WriteLine("0. Banany");
                    Console.WriteLine("1. Czekolada");
                    Console.WriteLine("2. Ryby");
                    Console.WriteLine("3. Mięso");
                    Console.WriteLine("4. Lody");
                    Console.WriteLine("5. Mrożona Pizza");
                    Console.WriteLine("6. Ser");
                    Console.WriteLine("7. Kiełbasa");
                    Console.WriteLine("8. Masło");
                    Console.WriteLine("9. Jaja");

                    EProduct product = (EProduct)Int32.Parse(Console.ReadLine());
                    manager.AddRefrigeratedContainer(Double.Parse(height), Double.Parse(depth),
                        Double.Parse(massMax), Double.Parse(tare), product);
                    break;
                case "3":
                    Console.WriteLine("Ile PSI maksymalnie będzie wewnątrz kontenera");
                    string psiMax = Console.ReadLine();
                    manager.AddGasContainer(Double.Parse(height), Double.Parse(depth),
                        Double.Parse(massMax), Double.Parse(tare), Double.Parse(psiMax));
                    break;
                default:
                    Console.WriteLine("Niepoprawna opcja");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("Aby powrócić wciśnij enter");
        Console.ReadLine();
    }
    public static void AddShip(Manager manager)
    {
        Console.WriteLine("Podaj nazwę statku");
        string name = Console.ReadLine();
        Console.WriteLine("Podaj prędkość maksymalną");
        string maxSpeed = Console.ReadLine();
        Console.WriteLine("Podaj maksymalną ilość kontenerów");
        string maxContainers = Console.ReadLine();
        Console.WriteLine("Podaj maksymalną ładowność w tonach");
        string maxWeight = Console.ReadLine();

        try
        {
            manager.AddShip(name, Double.Parse(maxSpeed), Int32.Parse(maxContainers), Double.Parse(maxWeight));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("Aby powrócić wciśnij enter");
        Console.ReadLine();
    }

    public static void ShipMenu(Manager manager)
    {
        Console.WriteLine("Podaj nazwę statku");
        string shipName = Console.ReadLine();
        try
        {
            Ship ship = manager.GetShip(shipName);
            Console.WriteLine(ship);
            Console.WriteLine("==Opcje statku==");
            Console.WriteLine("1. Ładowanie kontenera");
            Console.WriteLine("2. Ładowanie wielu kontenerów");
            Console.WriteLine("3. Wyładowanie kontenera");
            Console.WriteLine("4. Zastąpienie kontenera");
            Console.WriteLine("5. Przeniesienie kontenera na inny statek");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Podaj numer seryjny kontenera");
                    manager.AddContainerToShip(ship, Console.ReadLine());
                    break;
                case "2":
                    Console.WriteLine("Podaj numery seryjne oddzielone spacją");
                    string seriale = Console.ReadLine();
                    string[] listaSeriali = seriale.Split(' ');

                    // Sprawdź czy są wszystkie
                    List<Container> containers = new List<Container>();
                    foreach (string serial in listaSeriali)
                    {
                        containers.Add(manager.GetContainer(serial));
                    }

                    manager.AddContainersToShip(ship,  containers);
                    
                    // umieść na statku
                    
                    break;
                case "3":
                    Console.WriteLine("Podaj numer seryjny kontenera");
                    ship.Unload(Console.ReadLine());
                    break;
                case "4":
                    Console.WriteLine("Podaj numer seryjny kontenera na statku zamiany");
                    String c1 = Console.ReadLine();
                    Console.WriteLine("Podaj numer seryjny wolnego kontenera");
                    String c2 = Console.ReadLine();
                    manager.ReplaceContainerOnShip(ship.name, c1, c2);
                    break;
                case "5":
                    Console.WriteLine("Podaj numer seryjny kontenera");
                    String container = Console.ReadLine();
                    Console.WriteLine("Podaj nazwę statku, na który mam przenieść kontener");
                    String otherShip = Console.ReadLine();
                    manager.MoveContainerOntoShip(ship.name, container, otherShip);
                    break;
                default:
                    Console.WriteLine("Niepoprawna opcja");
                    break;           
            }
        } catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("Aby powrócić wciśnij enter");
        Console.ReadLine();
    }
    public static void ContainerMenu(Manager manager)
    {
        Console.WriteLine("Podaj identyfikator kontenera");
        string serial = Console.ReadLine();
        try
        {
            Container container = manager.GetContainer(serial);
            Console.WriteLine("Opcje kontenera:");
            Console.WriteLine("1. Ładowanie");
            Console.WriteLine("2. Rozładowanie");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Ile kg ładunku chcesz załadować");
                    string load = Console.ReadLine();
                    container.Load(Double.Parse(load));
                    break;
                case "2":
                    double unloaded = container.Unload();
                    Console.WriteLine("Rozładowano " + unloaded + " [kg]");
                    break;
                default:
                    Console.WriteLine("Niepoprawna opcja");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("Aby powrócić wciśnij enter");
        Console.ReadLine();
    }
    
}