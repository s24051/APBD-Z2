﻿using kontenery_cli.Enumy;
using kontenery_cli.Modele;
using kontenery_cli.Wyjatki;

namespace kontenery_cli;

// zarządzanie 
public class Manager
{
    private Dictionary<string, Ship> ships = new Dictionary<string, Ship>();
    private Dictionary<string, Container> containers = new Dictionary<string, Container>();

    public Ship AddShip(string name, double maxSpeed, int maxContainers, double maxWeight)
    {
        if (ships.ContainsKey(name))
            throw new Exception("A ship with this name already exists!");
        ships[name] = new Ship(name, maxSpeed, maxContainers, maxWeight);
        return ships[name];
    }

    public Container AddLiquidContainer(double height, double depth, double massMax, double tare, bool isHazardous)
    {
        Container container = new ContainerLiquid(height, depth, massMax, tare, isHazardous);
        containers.Add(container.Serial(), container);
        return container;
    }
    public Container AddGasContainer(double height, double depth, double massMax, double tare, double maxPsi)
    {
        Container container = new ContainerGas(height, depth, massMax, tare, maxPsi);
        containers.Add(container.Serial(), container);
        return container;
    }
    public Container AddRefrigeratedContainer(double height, double depth, double massMax, double tare, EProduct product)
    {
        Container container = new ContainerRefrigerated(height, depth, massMax, tare, product);
        containers.Add(container.Serial(), container);
        return container;
    }
    
    public void ReplaceContainerOnShip(string shipString, string containerSerial, string newContainerSerial)
    {
        if (!ships.ContainsKey(shipString))
            throw new Exception("Nonexistent ship: " + shipString);
        Ship ship = ships[shipString];
        
        if (!ship.containers.ContainsKey(containerSerial))
            throw new Exception(containerSerial + " not on ship " + ship.name);
        
        Container container = ship.containers[containerSerial];
        Container newContainer = containers[newContainerSerial];

        if (!ship.TestWeight(newContainer.TotalWeight() - container.TotalWeight()))
        {
            Console.WriteLine(ship);
            Console.WriteLine(container);
            Console.WriteLine(newContainer);
            throw new OverfillException("Ship capacity exceeded!");
        }
        
        ship.Unload(container);
        containers[container.Serial()] = container;
        ship.AddContainer(newContainer);
    }

    public void MoveContainerOntoShip(string source, string serial, string target)
    {
        if (!ships.ContainsKey(source))
            throw new Exception("Nonexistent ship: " + source);
        Ship src = ships[source];
        
        if (!src.containers.ContainsKey(serial))
            throw new Exception(serial + " not on ship " + source);
        Container container = src.containers[serial];
        
        if (!ships.ContainsKey(target))
            throw new Exception("Nonexistent ship: " + target);
        Ship dst = ships[target];

        if (!dst.TestWeight(container.TotalWeight()))
        {
            Console.WriteLine(dst);
            Console.WriteLine(container);
            throw new OverfillException("Ship capacity exceeded!");
        }

        src.Unload(container);
        dst.AddContainer(container);
    }
    
    public List<Container> GetContainers()
    {
        return containers.Values.ToList();
    }

    public List<Ship> GetShips()
    {
        return ships.Values.ToList();
    }
}