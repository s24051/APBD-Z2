using kontenery_cli.Enumy;
using kontenery_cli.Wyjatki;

namespace kontenery_cli.Modele;

public class ContainerRefrigerated : Container
{
    private EProduct product;
    public ContainerRefrigerated(double height, double depth, double massMax, double tare, EProduct product)
        : base(height, depth, massMax, tare)
    {
        type = EContainerType.REFRIGERATED;
        this.product = product;
    }

    public double temperature()
    {
        switch (product)
        {
            case EProduct.Bananas: return 13.3;
            case EProduct.Chocolate: return 18;
            case EProduct.Fish: return 2;
            case EProduct.Meat: return -15;
            case EProduct.IceCream: return -18;
            case EProduct.FrozenPizza: return -30;
            case EProduct.Cheese: return 7.2;
            case EProduct.Sausages: return 5;
            case EProduct.Butter: return 20.5;
            case EProduct.Eggs: return 19;
        }
        throw new Exception("Invalid product type");
    }
    
    public override Container Load(double load)
    {
        if ((load + mass) > massMax)
        {
            string warning = Serial() + ": exceeded " + (massMax) + " kg!";
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
        return base.ToString() + ", product: " + product + ", temperature " + temperature() + " [C]";
    }
}