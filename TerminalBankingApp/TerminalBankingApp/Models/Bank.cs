namespace TerminalBankingApp.Models;

public class Bank
{
    public readonly Dictionary<string, IAccountController> Accounts;

    public Bank()
    {
        Accounts = new Dictionary<string, IAccountController>();
    }
    
}