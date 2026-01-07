namespace TerminalBankingApp.Utils;

public static class Parse
{
    public static string? Id()
    {
        Console.Write("Enter account ID: ");
        var inputtedId = Console.ReadLine();

        return inputtedId;
    }

    public static decimal Amount()
    {
        Console.Write("Enter a money amount: ");
        var inputtedAmount = Console.ReadLine();
        
        if(!decimal.TryParse(inputtedAmount, out var amount))
        {
            return -1;
        }

        return amount;
    }
}