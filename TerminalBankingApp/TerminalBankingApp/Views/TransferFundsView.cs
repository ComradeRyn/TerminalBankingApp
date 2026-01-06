using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public class TransferFundsView
{
    public static void TransferFunds()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isValid = false;

        var inputtedReceiver = "";
        var inputtedSender = "";
        var inputtedAmount = (decimal?)null;
        
        var controller = new AccountController(MainMenuView.ManagerController);
        
        while (!isValid)
        {
            Console.Write(@"(Sending Account) ");
            inputtedSender = MainMenuView.ParseAccount();
            
            if (inputtedSender == "exit")
            {
                return;
            }
            
            Console.Write(@"(Receiving Account) ");
            inputtedReceiver = MainMenuView.ParseAccount();
            
            if (inputtedReceiver == "exit")
            {
                return;
            }
            
            inputtedAmount = MainMenuView.ParseAmount();
            if (inputtedAmount == null)
            {
                return;
            }

            controller.SetAccount(inputtedSender);
            isValid = controller.MakeTransfer(inputtedReceiver, (decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Ids must be valid, with a positive money amount, less than the balance of the sender.");
            }
        }
        
        Console.WriteLine($"Successfully transferred ${inputtedAmount:F2} to from Id: {inputtedSender} to Id:{inputtedReceiver}.");
    }
}