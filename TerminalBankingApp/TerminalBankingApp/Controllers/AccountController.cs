using System.IO.Enumeration;

namespace TerminalBankingApp.Controllers;

public class AccountController
{
    private List<Account> _accounts;

    public AccountController()
    {
        _accounts = new List<Account>();
    }
    
    public bool MakeDeposit(string id, decimal amount)
    {
        var retrievedAccount = GetAccount(id);
        
        if (retrievedAccount == null || amount <= 0) return false;
        return UpdateBalance(id, amount);
    }

    public bool MakeWithdraw(string id, decimal amount)
    {
        var retrievedAccount = GetAccount(id);
        
        if (retrievedAccount == null || amount <= 0 || amount > retrievedAccount.Balance) return false;
        return UpdateBalance(id,amount * -1);
    }

    public bool MakeTransfer(string sendingId, 
        string receivingId, 
        decimal amount)
    {
        if (MakeWithdraw(sendingId, amount))
        {
            if (MakeDeposit(receivingId, amount))
            {
                return true;
            }
            else
            {
                MakeDeposit(sendingId, amount);
            }
        }
        
        return false;
    }
    
    public Account? CreateAccount(string name)
    {
        if (!ValidateName(name))
        {
            return null;
        }
        
        var newAccount = new Account(name);
        _accounts.Add(newAccount);

        return newAccount;
    }

    public decimal CheckBalance(string id)
    {
        var selectedAccount = GetAccount(id);
        if (selectedAccount == null)
        {
            return -1;
        }

        return selectedAccount.Balance;
    }
    
    private bool UpdateBalance(string id, decimal amount)
    {
        var retrievedAccount = GetAccount(id);
        if (retrievedAccount == null) return false;
        retrievedAccount.Balance += amount;

        return true;
    }
    
    private Account? GetAccount(string id) 
        => _accounts.FirstOrDefault(account => account.Id.ToString() == id);
    
    private bool ValidateName(string accountName)
    {
        var nameTokens = accountName.Split(" ");
        return nameTokens.All(name => name.All(char.IsLetter) && name != "");
    }
}