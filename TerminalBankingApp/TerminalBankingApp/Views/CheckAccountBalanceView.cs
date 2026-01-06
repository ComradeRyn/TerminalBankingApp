using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class CheckAccountBalanceView : IViewable
{

    public void Handle(BankController bankController)
    {
        var isSuccesful = false;

        while (!isSuccesful)
        {
            var inputtedId = Parse.Id();

            if (inputtedId == "exit")
            {
                return;
            }

            bankController.TryGetAccount(inputtedId, out var account);
            isSuccesful = account.TryCheckBalance(out var balance);

            Console.WriteLine(isSuccesful ? $"Account {inputtedId} has a balance of ${balance}" : "Invalid account Id");
        }
    }
}