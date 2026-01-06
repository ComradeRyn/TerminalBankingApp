using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class BankController
{
    private readonly Bank _bank = new();

    public bool TryCreateAccount(string name, out string? id)
    {
        if (!ValidateName(name))
        {
            id = null;
            return false;
        }
        
        var newAccount = new Account(name);
        _bank.Accounts.Add(newAccount.Id.ToString(), new AccountController(newAccount));

        id = newAccount.Id.ToString();
        return true;
    }

    public bool TryGetAccount(string? id, out IAccountController value)
    {
        if (id != null && _bank.Accounts.TryGetValue(id, out value))
        {
            return true;
        }

        value = new AccountController(null);
        return false;
    }
    
    private bool ValidateName(string accountName)
    {
        var nameTokens = accountName.Split(" ");
        return nameTokens.All(name => name.All(char.IsLetter) && name != "");
    }
}