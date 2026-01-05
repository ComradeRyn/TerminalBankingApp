namespace TerminalBankingApp.Views;

public class DepositViewer
{
    public static void DepositView()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isValid = false;

        var inputtedAccount = "";
        var inputtedAmount = (decimal?)null;
        
        while (!isValid)
        {
            inputtedAccount = MainMenuViewer.ParseAccount();
            
            if (inputtedAccount == "exit")
            {
                return;
            }
            
            inputtedAmount = MainMenuViewer.ParseAmount();
            if (inputtedAmount == null)
            {
                return;
            }

            isValid = MainMenuViewer.Controller.MakeDeposit(inputtedAccount, (decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Must have valid Id and positive money amount");
            }
            
        }
        
        Console.WriteLine($"Successfully deposited ${inputtedAmount:F2} to Id: {inputtedAccount}.");
        Console.WriteLine($"New Balance of ${MainMenuViewer.Controller.CheckBalance(inputtedAccount):F2}");
    }
}