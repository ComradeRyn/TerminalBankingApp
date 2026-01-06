using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class WithdrawView : IViewable
{
    public void Handle(BankController bankController)
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isSuccessful = false;

        while (!isSuccessful)
        {
            var inputtedId = Parse.Id();
            IAccountController accountController;
            
            if (inputtedId == "exit")
            {
                return;
            }
            
            if (!bankController.TryGetAccount(inputtedId, out accountController))
            {
                Console.WriteLine("Invalid account Id");
                continue;
            }

            var inputtedAmount = Parse.Amount();

            if (inputtedAmount == null)
            {
                return;
            }

            isSuccessful = accountController.TryMakeWithdraw((decimal)inputtedAmount);

            Console.WriteLine(isSuccessful
                ? $"Successfully withdrew ${inputtedAmount:F2} to Id: {inputtedId}"
                : $"{Responses.nonNegative} and {Responses.lessThanBalance}");
        }
    }
}