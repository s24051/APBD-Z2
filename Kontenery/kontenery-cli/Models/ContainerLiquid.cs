using kontenery_cli.Enumy;
using kontenery_cli.Interfejsy;
using kontenery_cli.Wyjatki;

namespace kontenery_cli.Modele;

public class ContainerLiquid : Container, IHazardNotifier
{
    private bool isHazardous = false;
    public ContainerLiquid(double height, double depth, double massMax, double tare, bool isHazardous)
        : base(height, depth, massMax, tare)
    {
        type = EContainerType.LIQUID;
        this.isHazardous = isHazardous;
    }
    
    public override Container Load(double load)
    {
        double limit = isHazardous ? 0.9 : 0.5;
        if (((load + mass) / massMax) > limit)
        {
            string warning = Serial() + ": exceeded " + (limit * 100) + "% capacity!";
            if (isHazardous)
                printHazardWarning(warning, Serial());
            throw new OverfillException(warning);
        }
        mass += load;
        return this;
    }

    public void printHazardWarning(string message, string container)
    {
        Console.WriteLine("******** WARNING **********");
        Console.WriteLine("Container: " + container);
        Console.WriteLine(message);
    }
    
    public override string ToString()
    {
        return base.ToString() + ", isHazardous: " + isHazardous;
    }
}