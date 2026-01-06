namespace TerminalBankingApp.Models;

public interface IAccountController
{
    public bool TryMakeDeposit(decimal amount);

    public bool TryMakeWithdraw(decimal amount);

    public bool TryMakeTransfer(IAccountController receiving, decimal amount);

    public bool TryCheckBalance(out decimal balance);
}