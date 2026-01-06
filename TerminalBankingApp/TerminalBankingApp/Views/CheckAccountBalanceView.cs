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
            
            IAccountController account;
            decimal balance;

            bankController.TryGetAccount(inputtedId, out account);
            isSuccesful = account.TryCheckBalance(out balance);

            if (!isSuccesful)
            {
                Console.WriteLine("Invalid account Id");
            }
        }
    }
}