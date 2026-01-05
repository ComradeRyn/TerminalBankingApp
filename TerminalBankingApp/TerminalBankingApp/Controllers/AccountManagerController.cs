using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountManagerController
{
    private AccountManager _accountManager;
    
    public AccountManagerController()
    {
        _accountManager = new AccountManager();
    }
    
    public Account? CreateAccount(string name)
    {
        if (!ValidateName(name))
        {
            return null;
        }
        
        var newAccount = new Account(name);
        _accountManager.Accounts.Add(newAccount.Id.ToString(), newAccount);

        return newAccount;
    }

    public Account? GetAccount(string id)
    {
        Account value;
        if (_accountManager.Accounts.TryGetValue(id, out value))
        {
            return value;
        }

        else
        {
            return null;
        }
    }
    
    private bool ValidateName(string accountName)
    {
        var nameTokens = accountName.Split(" ");
        return nameTokens.All(name => name.All(char.IsLetter) && name != "");
    }
}