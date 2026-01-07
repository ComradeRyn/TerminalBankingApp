using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class DepositView : IViewable
{
    public void Handle(BankController bankController)
    {
        var inputtedAccount = Parse.Id();
        if (!bankController.TryGetAccount(inputtedAccount, out var selectedAccount))
        {
            Console.WriteLine(Responses.InvalidId);

            return;
        }

        var inputtedAmount = Parse.Amount();
        if (!selectedAccount!.TryMakeDeposit(inputtedAmount))
        {
            Console.WriteLine(Responses.NonNegative);

            return;
        }
        
        Success(inputtedAmount);
    }
    
    public string GetActionName()
        => "Make a Deposit";

    private void Success(decimal inputtedAmount)
        => Console.WriteLine($"Successfully deposited ${inputtedAmount:F2}.");
}