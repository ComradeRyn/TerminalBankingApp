using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class CheckAccountBalanceView : IViewable
{

    public void Handle(BankController bankController)
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        
        var isSuccessful = false;

        while (!isSuccessful)
        {
            var inputtedId = Parse.Id();

            if (inputtedId == "exit")
            {
                return;
            }

            bankController.TryGetAccount(inputtedId, out var account);
            isSuccessful = account.TryCheckBalance(out var balance);

            Console.WriteLine(isSuccessful ? $"Account {inputtedId} has a balance of ${balance:F2}" : Responses.invalidId);
        }
    }

    public string GetActionName()
    {
        return "Check Account Balance";
    }
}