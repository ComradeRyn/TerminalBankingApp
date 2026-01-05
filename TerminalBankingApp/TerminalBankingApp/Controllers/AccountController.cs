using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountController
{
    private Account? _account;

    public AccountController(Account account)
    {
        _account = account;
    }

    public AccountController(string id, AccountManagerController accountManagerController)
    {
        _account = accountManagerController.GetAccount(id);
    }
    
    public bool MakeDeposit(string id, decimal amount)
    {
        var retrievedAccount = _account;
        
        if (retrievedAccount == null || amount <= 0) return false;
        return UpdateBalance(id, amount);
    }

    public bool MakeWithdraw(string id, decimal amount)
    {
        var retrievedAccount = _account;
        
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
    
    
    public decimal CheckBalance(string id)
    {
        var selectedAccount = _account;
        if (selectedAccount == null)
        {
            return -1;
        }

        return selectedAccount.Balance;
    }
    
    private bool UpdateBalance(string id, decimal amount)
    {
        var retrievedAccount = _account;
        if (retrievedAccount == null) return false;
        retrievedAccount.Balance += amount;

        return true;
    }
}