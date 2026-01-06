using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class WithdrawView : IViewable
{
    public void Handle(BankController bankController)
    {
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

            Console.WriteLine(!isSuccessful
                ? "Entered amount must be positive, and less than or equal to account balance!"
                : $"Successfully withdrew {inputtedAmount:F2} to Id: {inputtedId}");
        }
    }
}