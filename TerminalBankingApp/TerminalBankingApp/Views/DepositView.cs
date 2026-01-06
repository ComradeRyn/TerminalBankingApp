using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;

namespace TerminalBankingApp.Views;

public class DepositView
{
    public static void Deposit()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");

        var controller = new AccountController(MainMenuView.ManagerController);
        
        var isValid = false;

        var inputtedAccount = "";
        var inputtedAmount = (decimal?)null;
        
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
            isValid = controller.MakeDeposit((decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Must have valid Id and positive money amount");
            }
            
        }
        
        Console.WriteLine($"Successfully deposited ${inputtedAmount:F2} to Id: {inputtedAccount}.");
        Console.WriteLine($"New Balance of ${controller.CheckBalance():F2}");
    }
}