using System;
namespace kontenery_cli.Wyjatki;

public class OverfillException: Exception
{
    public OverfillException(string message) : base(message)
    {
    }
}