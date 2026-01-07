using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class WithdrawView : IViewable
{
    public void Handle(BankController bankController)
    {
        var inputtedId = Parse.Id();
        if (!bankController.TryGetAccount(inputtedId, out var accountController))
        {
            Console.WriteLine(Responses.InvalidId);

            return;
        }

        var inputtedAmount = Parse.Amount();
        if (inputtedAmount < 0)
        {
            Console.WriteLine(Responses.NonNegative);

            return;
        }

        if (!accountController!.TryMakeWithdraw(inputtedAmount))
        {
            Console.WriteLine(Responses.LessThanBalance);

            return;
        }
        
        Success(inputtedAmount);
    }

    public string GetActionName()
        => "Make a Withdraw";

    private void Success(decimal inputtedAmount)
        => Console.WriteLine($"Successfully withdrew ${inputtedAmount:F2}");
}
