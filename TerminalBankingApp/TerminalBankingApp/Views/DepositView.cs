namespace TerminalBankingApp.Views;

public class DepositView
{
    public static void Deposit()
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

            isValid = MainMenuView.Controller.MakeDeposit(inputtedAccount, (decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Must have valid Id and positive money amount");
            }
            
        }
        
        Console.WriteLine($"Successfully deposited ${inputtedAmount:F2} to Id: {inputtedAccount}.");
        Console.WriteLine($"New Balance of ${MainMenuView.Controller.CheckBalance(inputtedAccount):F2}");
    }
}