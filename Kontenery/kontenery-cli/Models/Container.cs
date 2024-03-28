using System.Linq.Expressions;
using kontenery_cli.Enumy;

namespace kontenery_cli.Modele;

public abstract class Container
{
    private static int counter = 0;
    private int id;
    public EContainerType type { get; protected set; }
    
    public double massMax { get; }
    public double tare { get; set; }
    public double height { get; set; }
    public double depth { get; set; }
    
    public double mass { get; protected set;  }
    
    public Container(
        double height, 
        double depth,
        double massMax,
        double tare)
    {
        id = counter++;
        
        this.height = height;
        this.depth = depth;
        this.massMax = massMax;
        this.tare = tare;
    }

    public string Sign()
    {
        switch (type)
        {
            case EContainerType.GAS:
                return "G";
            case EContainerType.LIQUID:
                return "L";
            case EContainerType.REFRIGERATED:
                return "R";
            default:
                throw new Exception("Invalid container type!");
        }
    }

    public double TotalWeight()
    {
        return tare + mass;
    }

    public string Serial()
    {
        return "KON-" + Sign() + "-" + id;
    }

    public virtual double Unload()
    {
        double tmp = mass;
        mass = 0;
        return tmp;
    }
    public abstract Container Load(double masa);
    public override string ToString()
    {
        return "Container " + Serial() + ": " +
               "max load: " + massMax + "[kg]" +
               ", mass: " + mass + "[kg]" +
               ", tare: " + tare + "[kg]" +
               ", height: " + height + "[cm]" +
               ", depth: " + depth + "[cm]";
    }
}