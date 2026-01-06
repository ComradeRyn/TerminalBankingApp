using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;

namespace TerminalBankingApp.Views;

public class CheckAccountBalanceViewer
{
    public static void CheckAccountBalance()
    {
        
        Console.WriteLine("Type \"exit\" to return to main menu");
        
        string? inputtedAccount = null;
        var accountBalance = -1m;
        var controller = new AccountController(MainMenuView.ManagerController); 

        while (accountBalance == -1)
        {
            inputtedAccount = MainMenuView.ParseAccount();
            controller.SetAccount(inputtedAccount);
            accountBalance = controller.CheckBalance();

            if (inputtedAccount == "exit")
            {
                return;
            }
        }
        
        Console.WriteLine($"Account with Id: {inputtedAccount} has a balance of ${accountBalance:F2}");
    }
}