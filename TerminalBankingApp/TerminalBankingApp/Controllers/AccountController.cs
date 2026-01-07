using TerminalBankingApp.Models;

namespace TerminalBankingApp.Controllers;

public class AccountController(Account account) : IAccountController
{
    // Don't allow the account to be nullable, so you can remove the null checks
    public Account Account { private get; init; } = account;

    public bool TryMakeDeposit(decimal amount)
    {
        if (amount > 0)
        {
            Account.Balance += amount;
            
            return true;
        }

        return false;
    }

    public bool TryMakeWithdraw(decimal amount)
    {
        if (amount > 0 && amount <= Account.Balance)
        {
            Account.Balance -= amount;
            
            return true;
        }
        
        return false;
    }

    public bool TryMakeTransfer(IAccountController receiving, decimal amount)
    {
        if (!TryMakeWithdraw(amount))
        {
            return false;
        }
        
        if (!receiving.TryMakeDeposit(amount))
        {
            TryMakeDeposit(amount);

            return false;
        }

        return true;
    }
    
    public decimal CheckBalance()
        => Account.Balance;
}