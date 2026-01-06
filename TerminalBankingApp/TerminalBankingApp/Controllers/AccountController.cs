using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountController
{
    private Account? _account;
    private AccountManagerController _accountManagerController;

    public AccountController(string? id, AccountManagerController accountManagerController)
    {
        _account = accountManagerController.GetAccount(id);
        _accountManagerController = accountManagerController;
    }
    
    public AccountController(AccountManagerController accountManagerController)
    {
        _account = null;
        _accountManagerController = accountManagerController;
    }

    public bool CheckAccountNull() 
        => _account == null;
    
    // Was told about a Try syntax
    public bool SetAccount(string? id)
    {
        _account = _accountManagerController.GetAccount(id);
        return _account != null;
    }
    
    public bool MakeDeposit(decimal amount)
    {
        if (_account == null || amount <= 0) return false;
        return UpdateBalance(amount);
    }

    public bool MakeWithdraw(decimal amount)
    {
        if (_account == null || amount <= 0 || amount > _account.Balance) return false;
        return UpdateBalance(amount * -1);
    }

    public bool MakeTransfer(string receivingId, decimal amount)
    {
        var receiving = new AccountController(receivingId, _accountManagerController);
        if (!MakeWithdraw(amount)) return false;
        if (receiving.MakeDeposit(amount))
        {
            return true;
        }

        MakeDeposit(amount);

        return false;
    }
    
    
    public decimal CheckBalance()
    {
        if (_account == null)
        {
            return -1;
        }

        return _account.Balance;
    }
    
    private bool UpdateBalance(decimal amount)
    {
        if (_account == null) return false;
        _account.Balance += amount;

        return true;
    }
}