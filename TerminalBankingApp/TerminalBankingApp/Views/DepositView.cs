using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class DepositView : IViewable
{
    public void Handle(BankController bankController)
    {
        var isSuccessful = false;

        while (!isSuccessful)
        {
            var inputtedAccount = Parse.Id();
            IAccountController selectedAccount;

            if (inputtedAccount == "exit")
            {
                return;
            }

            if (!bankController.TryGetAccount(inputtedAccount, out selectedAccount))
            {
                Console.WriteLine("Invalid account Id");
                continue;
            }

            var inputtedAmount = Parse.Amount();

            if (inputtedAmount == null)
            {
                return;
            }

            if (inputtedAmount == -1)
            {
                Console.WriteLine("Must enter positive money amount");
                continue;
            }

            isSuccessful = selectedAccount.TryMakeDeposit((decimal)inputtedAmount);

            if (isSuccessful)
            {
                Console.WriteLine($"Successfully deposited ${inputtedAmount:F2} to Id: {inputtedAccount}.");
            }
        }
    }
    
}