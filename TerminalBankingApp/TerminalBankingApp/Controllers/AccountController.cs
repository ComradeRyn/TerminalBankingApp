using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountController(Account? account) : IAccountController
{
    public Account? Account { private get; set; } = account;

    public bool TryMakeDeposit(decimal amount)
    {
        if (Account == null || amount <= 0) return false;
        return TryUpdateBalance(amount);
    }

    public bool TryMakeWithdraw(decimal amount)
    {
        if (Account == null || amount <= 0 || amount > Account.Balance) return false;
        return TryUpdateBalance(amount * -1);
    }

    public bool TryMakeTransfer(IAccountController receiving, decimal amount)
    {
        if (!TryMakeWithdraw(amount)) return false;
        if (receiving.TryMakeDeposit(amount))
        {
            return true;
        }

        TryMakeDeposit(amount);

        return false;
    }
    
    public bool TryCheckBalance(out decimal balance)
    {
        if (Account == null)
        {
            balance = -1;
            return false;
        }

        balance = Account.Balance;
        return true;
    }
    
    private bool TryUpdateBalance(decimal amount)
    {
        if (Account == null) return false;
        Account.Balance += amount;

        return true;
    }
}