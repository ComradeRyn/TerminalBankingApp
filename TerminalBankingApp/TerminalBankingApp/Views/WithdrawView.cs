using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;

namespace TerminalBankingApp.Views;

public class WithdrawView
{
    public static void Withdraw()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isValid = false;

        var inputtedAccount = "";
        var inputtedAmount = (decimal?)null;

        var controller = new AccountController(MainMenuView.ManagerController);
        
        while (!isValid)
        {
            inputtedAccount = Parse.Id();
            
            if (inputtedAccount == "exit")
            {
                return;
            }
            
            inputtedAmount = Parse.Amount();
            if (inputtedAmount == null)
            {
                return;
            }

            controller.SetAccount(inputtedAccount);
            isValid = controller.MakeWithdraw((decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Must have valid Id, positive money amount, inputted amount less than or equal to balance.");
            }
        }
        
        Console.WriteLine($"Successfully withdrew ${inputtedAmount:F2} to Id: {inputtedAccount}.");
        Console.WriteLine($"New Balance of ${controller.CheckBalance():F2}");
    }
}