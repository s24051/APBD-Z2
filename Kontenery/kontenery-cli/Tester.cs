using kontenery_cli.Enumy;
using kontenery_cli.Modele;
using kontenery_cli.Wyjatki;

namespace kontenery_cli;

public class Tester
{
    public static void MockRequirements(Manager manager)
    {
        Console.WriteLine("********* DANE TESTOWE ************");
        
        //Stworzenie kontenera danego typu
        Console.WriteLine("***Stworzenie kontenera danego typu***");
        Container cg = manager.AddGasContainer(50, 50, 1500, 100, 30);
        Container cl = manager.AddLiquidContainer(100, 200, 1500, 100, true);
        Container cr = manager.AddRefrigeratedContainer(300, 300, 1500, 100, EProduct.Bananas);
        Console.WriteLine(cg);
        Console.WriteLine(cl);
        Console.WriteLine(cr);
        Console.WriteLine();
        //Załadowanie ładunku do danego kontenera
        Console.WriteLine("***Załadowanie ładunku do danego kontenera***");
        cg.Load(750);
        try
        {
            cg.Load(1000);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e);
        }
        cg.Load(250);
        cr.Load(1400);
        Console.WriteLine(cg);
        Console.WriteLine(cl);
        Console.WriteLine(cr);
        Console.WriteLine();
        //Załadowanie kontenera na statek
        Console.WriteLine("***Załadowanie kontenera na statek***");
        Ship shipA = manager.AddShip("HMS.A", 30, 50, 5000);
        Ship shipB = manager.AddShip("RHS.B", 25, 30, 1000);
        shipA.AddContainer(cg);
        shipA.AddContainer(cr);
        shipB.AddContainer(cl);
        Console.WriteLine(shipA);
        Console.WriteLine(shipB);
        Console.WriteLine();
        //Załadowanie listy kontenerów na statek
        shipA.AddContainers(
        [
            new ContainerRefrigerated(20, 10, 100, 10, EProduct.Butter).Load(75),
            new ContainerRefrigerated(30, 20, 200, 10, EProduct.Butter).Load(150)
        ]);
        Console.WriteLine("***Załadowanie listy kontenerów na statek***");
        Console.WriteLine(shipA);
        Console.WriteLine();
        //    Usunięcie kontenera ze statku
        Console.WriteLine("***Usunięcie kontenera ze statku***");
        shipA.Unload(cg);
        Console.WriteLine(shipA);
        Console.WriteLine();
        
        //    Rozładowanie kontenera
        Console.WriteLine("***Rozładowanie kontenera***");
        cg.Unload(); // 75kg zostaje bo 5%!!!
        Console.WriteLine(cg);
        Console.WriteLine();
        
        //    Zastąpienie kontenera na statku o danym numerze innym kontenerem
        Console.WriteLine("***Zastąpienie kontenera na statku o danym numerze innym kontenerem***");
        Container newCl = manager.AddLiquidContainer(250, 250, 1500, 100, true).Load(570);
        Console.WriteLine("Przed zastapieniem");
        Console.WriteLine(shipA);
        Console.WriteLine(newCl);
        Console.WriteLine();

        manager.ReplaceContainerOnShip(shipA.name, "KON-R-2", newCl.Serial());
        Console.WriteLine("Po zastąpieniu KON-R-2 i " + newCl.Serial());
        Console.WriteLine(shipA);
        Console.WriteLine();

        // Możliwość przeniesienie kontenera między dwoma statkami
        Console.WriteLine("***Możliwość przeniesienie kontenera między dwoma statkami***");
        Console.WriteLine("Statki:");
        Console.WriteLine(shipA);
        Console.WriteLine(shipB);
        Console.WriteLine();

        Console.WriteLine("***Przemieszczenie KON-R-3 z " + shipA.name + " na " + shipB.name + "***");
        manager.MoveContainerOntoShip(shipA.name, "KON-R-3", shipB.name);

        Console.WriteLine(shipA);
        Console.WriteLine(shipB);
        Console.WriteLine("*************************************");

    }
}