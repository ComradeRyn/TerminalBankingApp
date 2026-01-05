namespace TerminalBankingApp.Views;

public class WithdrawViewer
{
    public static void WithdrawView()
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

            isValid = MainMenuViewer.Controller.MakeWithdraw(inputtedAccount, (decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Must have valid Id, positive money amount, inputted amount less than or equal to balance.");
            }
        }
        
        Console.WriteLine($"Successfully withdrew ${inputtedAmount:F2} to Id: {inputtedAccount}.");
        Console.WriteLine($"New Balance of ${MainMenuViewer.Controller.CheckBalance(inputtedAccount):F2}");
    }
}