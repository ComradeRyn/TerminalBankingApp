using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountController
{
    private Account? _account;
    private readonly AccountManagerController _accountManagerController;

    // Does it make more sense to pass in a controller and call the method on the controller her, or does it make sense to just pass in an account
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
    
    public void SetAccount(string? id)
    {
        _account = _accountManagerController.GetAccount(id);
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