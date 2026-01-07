using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class CheckAccountBalanceView : IViewable
{

    public void Handle(BankController bankController)
    {
        var inputtedId = Parse.Id();
        if (bankController.TryGetAccount(inputtedId, out var account))
        {
            Console.WriteLine($"Account has a balance of ${account!.CheckBalance():F2}");
            
            return;
        }
        
        Console.WriteLine(Responses.InvalidId);
    }

    public string GetActionName() => "Check Account Balance";
}