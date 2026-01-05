namespace TerminalBankingApp.Views;

public class WithdrawView
{
    public static void Withdraw()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isValid = false;

        var inputtedAccount = "";
        var inputtedAmount = (decimal?)null;
        
        while (!isValid)
        {
            inputtedAccount = MainMenuView.ParseAccount();
            
            if (inputtedAccount == "exit")
            {
                return;
            }
            
            inputtedAmount = MainMenuView.ParseAmount();
            if (inputtedAmount == null)
            {
                return;
            }

            isValid = MainMenuView.Controller.MakeWithdraw(inputtedAccount, (decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Must have valid Id, positive money amount, inputted amount less than or equal to balance.");
            }
        }
        
        Console.WriteLine($"Successfully withdrew ${inputtedAmount:F2} to Id: {inputtedAccount}.");
        Console.WriteLine($"New Balance of ${MainMenuView.Controller.CheckBalance(inputtedAccount):F2}");
    }
}