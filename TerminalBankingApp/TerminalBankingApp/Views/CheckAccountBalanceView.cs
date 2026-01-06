using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class CheckAccountBalanceView : IViewable
{
    public static void CheckAccountBalance()
    {
        
        Console.WriteLine("Type \"exit\" to return to main menu");
        
        string? inputtedAccount = null;
        var accountBalance = -1m;
        var controller = new AccountController(MainMenuView.ManagerController); 

        while (accountBalance == -1)
        {
            inputtedAccount = Parse.Id();
            controller.TrySetAccount(inputtedAccount);
            //accountBalance = controller.TryCheckBalance();

            if (inputtedAccount == "exit")
            {
                return;
            }
        }
        
        Console.WriteLine($"Account with Id: {inputtedAccount} has a balance of ${accountBalance:F2}");
    }

    public void Handle(BankController managerController)
    {
        throw new NotImplementedException();
    }
}