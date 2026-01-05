namespace TerminalBankingApp.Models;

public class AccountManager
{
    public Dictionary<string, Account> Accounts;

    public AccountManager()
    {
        Accounts = new Dictionary<string, Account>();
    }
    
}