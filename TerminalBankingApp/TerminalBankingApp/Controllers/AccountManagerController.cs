using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountManagerController
{
    private AccountManager _accountManager;
    
    public AccountManagerController()
    {
        _accountManager = new AccountManager();
    }
    
    public string? CreateAccount(string name)
    {
        if (!ValidateName(name))
        {
            return null;
        }
        
        var newAccount = new Account(name);
        _accountManager.Accounts.Add(newAccount.Id.ToString(), newAccount);

        return newAccount.Id.ToString();
    }

    public Account? GetAccount(string? id)
    {
        if (id == null)
        {
            return null;
        }
        
        Account? value;
        return _accountManager.Accounts.TryGetValue(id, out value) ? value : null;
    }
    
    private bool ValidateName(string accountName)
    {
        var nameTokens = accountName.Split(" ");
        return nameTokens.All(name => name.All(char.IsLetter) && name != "");
    }
}