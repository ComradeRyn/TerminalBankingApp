namespace TerminalBankingApp.Views;

public class CheckAccountBalanceViewer
{
    public static void CheckAccountBalanceView()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var inputtedAccount = (string?)null;
        var accountBalance = -1m;

        while (accountBalance == -1)
        {
            inputtedAccount = MainMenuView.ParseAccount();
            accountBalance = MainMenuView.Controller.CheckBalance(inputtedAccount);

            if (inputtedAccount == "exit")
            {
                return;
            }
        }
        
        Console.WriteLine($"Account with Id: {inputtedAccount} has a balance of ${accountBalance:F2}");
    }
}