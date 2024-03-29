using kontenery_cli.Enumy;
using kontenery_cli.Interfejsy;
using kontenery_cli.Wyjatki;

namespace kontenery_cli.Modele;

public class ContainerGas : Container, IHazardNotifier
{
    private double MIN_LOAD = 0.05;
    private double maxPsi;
    public ContainerGas(double height, double depth, double massMax, double tare, double maxPsi)
        : base(height, depth, massMax, tare)
    {
        type = EContainerType.GAS;
        this.maxPsi = maxPsi;
        Load(massMax * MIN_LOAD);
    }

    public override double Unload()
    {
        double min = massMax * MIN_LOAD;
        if (mass < min)
        {
            printHazardWarning("A minimum of 5% is required to be inside!", Serial());
            return 0;
        }
           
        double tmp = mass - min;
        mass = min;
        return tmp;
    }

    public override Container Load(double load)
    {
        if ((load + mass) > massMax)
        {
            string warning = Serial() + ": exceeded " + (massMax) + " kg!";
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

    public double CalcPressure()
    {
        double basePsi = maxPsi * 0.1;
        double contentPsi = (mass / massMax) * (maxPsi * 0.9);
        return basePsi + contentPsi;
    }

    public override string ToString()
    {
        return base.ToString() + ", pressure: " + CalcPressure() + " [PSI]";
    }
}