namespace TerminalBankingApp.Views;

public class TransferFundsViewer
{
    public static void TransferFundsView()
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isValid = false;

        var inputtedReceiver = "";
        var inputtedSender = "";
        var inputtedAmount = (decimal?)null;
        
        while (!isValid)
        {
            Console.Write(@"(Sending Account) ");
            inputtedSender = MainMenuViewer.ParseAccount();
            
            if (inputtedSender == "exit")
            {
                return;
            }
            
            Console.Write(@"(Receiving Account) ");
            inputtedReceiver = MainMenuViewer.ParseAccount();
            
            if (inputtedReceiver == "exit")
            {
                return;
            }
            
            inputtedAmount = MainMenuViewer.ParseAmount();
            if (inputtedAmount == null)
            {
                return;
            }

            isValid = MainMenuViewer.Controller.MakeTransfer(inputtedSender,inputtedReceiver, (decimal)inputtedAmount);

            if (!isValid)
            {
                Console.WriteLine("Invalid input: Ids must be valid, with a positive money amount, less than the balance of the sender.");
            }
        }
        
        Console.WriteLine($"Successfully transferred ${inputtedAmount:F2} to from Id: {inputtedSender} to Id:{inputtedReceiver}.");
    }
}