using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class DepositView : IViewable
{
    public void Handle(BankController bankController)
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isSuccessful = false;

        while (!isSuccessful)
        {
            var inputtedAccount = Parse.Id();

            if (inputtedAccount == "exit")
            {
                return;
            }

            if (!bankController.TryGetAccount(inputtedAccount, out var selectedAccount))
            {
                Console.WriteLine(Responses.invalidId);
                continue;
            }

            var inputtedAmount = Parse.Amount();

            if (inputtedAmount == null)
            {
                return;
            }

            isSuccessful = selectedAccount.TryMakeDeposit((decimal)inputtedAmount);

            Console.WriteLine(isSuccessful
                ? $"Successfully deposited ${inputtedAmount:F2} to Id: {inputtedAccount}."
                : Responses.nonNegative);
        }
    }
    
    public string GetActionName()
    {
        return "Make a Deposit";
    }
    
}