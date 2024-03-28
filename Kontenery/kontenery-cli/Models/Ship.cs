using System.Collections;
using kontenery_cli.Wyjatki;

namespace kontenery_cli.Modele;

public class Ship
{
    public string name { get; protected set; }
    public double maxSpeed { get; protected set; }
    public int maxContainers { get; protected set; }
    public double maxWeight { get; protected set; }
    public Dictionary<string, Container> containers { get; protected set; }
    private double currentWeight;

    public Ship(string name, double maxSpeed, int maxContainers, double maxWeight)
    {
        this.name = name;
        this.maxSpeed = maxSpeed;
        this.maxContainers = maxContainers;
        this.maxWeight = maxWeight;
        containers = new Dictionary<string, Container>();
    }

    public bool TestWeight(double weight_in_kg)
    {
        return (currentWeight + (weight_in_kg / 1000) <= maxWeight);
    }

    public void AddContainer(Container container)
    {
        if (!TestWeight(container.TotalWeight()))
            throw new OverfillException("Ship capacity excceeded!");
        
        containers[container.Serial()] = container;
        currentWeight += container.TotalWeight() / 1000;
    }

    public void AddContainers(List<Container> _containers)
    {
        double weight = 0;
        foreach (var container in _containers)
        {
            weight += container.TotalWeight();
        }

        if (!TestWeight(weight))
            throw new OverfillException("Ship capacity exceeded!");
        
        foreach (var container in _containers)
        {
            containers[container.Serial()] = container;
        }
        currentWeight += weight;
    }
    
    public Container Unload(Container container)
    {
        if (!containers.ContainsValue(container))
        {
            Console.WriteLine(container.Serial() + " is not on this ship!");
            return null;
        }

        containers.Remove(container.Serial());
        return container;
    }
    
    public Container Unload(string serial)
    {
        if (!containers.ContainsKey(serial))
        {
            Console.WriteLine(serial + " is not on this ship!");
            return null;
        }
        
        return Unload(containers[serial]);
    }
    
    public override string ToString()
    {
        string message = "Ship " + name;
        message += ", maxSpeed: " + maxSpeed + "[kn]";
        message += ", maxWeight: " + maxWeight + "[t]";
        message += ", maxContainers: " + maxContainers;
        message += ", containers:";
        foreach (var container in containers)
        {
            message += "\n\t" + container + " ";
        }
        return message;
    }
}