using TerminalBankingApp.Controllers;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class TransferFundsView : IViewable
{
    public void Handle(BankController managerController)
    {
        Console.Write(@"(Sending Account) ");
        var inputtedSender = Parse.Id();
        if (!managerController.TryGetAccount(inputtedSender, out var sendingAccount))
        {
            Console.WriteLine(Responses.InvalidId);
            
            return;
        }
        
        Console.Write(@"(Receiving Account) ");
        var inputtedReceiver = Parse.Id();
        if (!managerController.TryGetAccount(inputtedReceiver, out var receivingAccount))
        {
            Console.WriteLine(Responses.InvalidId);
            
            return;
        }
        
        var inputtedAmount = Parse.Amount();
        if (inputtedAmount <= 0)
        {
            Console.WriteLine(Responses.NonNegative);
            
            return;
        }

        if (!sendingAccount!.TryMakeTransfer(receivingAccount!, inputtedAmount))
        {
            Console.WriteLine(Responses.LessThanBalance);
            
            return;
        }

        Success(inputtedAmount);
    }
    
    public string GetActionName()
        => "Transfer Funds";
    
    private void Success(decimal inputtedAmount)
        =>  Console.WriteLine($"Successfully transferred ${inputtedAmount:F2}.");
}