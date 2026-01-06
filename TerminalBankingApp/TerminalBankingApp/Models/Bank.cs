namespace TerminalBankingApp.Models;

public class Bank
{
    public Dictionary<string, IAccountController> Accounts;

    public Bank()
    {
        Accounts = new Dictionary<string, IAccountController>();
    }
    
}