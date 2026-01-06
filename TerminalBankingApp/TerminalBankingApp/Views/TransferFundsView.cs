using TerminalBankingApp.Controllers;
using TerminalBankingApp.Models;
using TerminalBankingApp.Utils;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class TransferFundsView : IViewable
{
    public void Handle(BankController managerController)
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        var isSuccessful = false;

        while (!isSuccessful)
        {
            Console.Write(@"(Sending Account) ");
            var inputtedSender = Parse.Id();
            
            if (inputtedSender == "exit")
            {
                return;
            }
            
            if (!managerController.TryGetAccount(inputtedSender, out var sendingAccount))
            {
                Console.WriteLine(Responses.invalidId);
                continue;
            }
            
            
            Console.Write(@"(Receiving Account) ");
            var inputtedReceiver = Parse.Id();

            if (inputtedSender == "exit")
            {
                return;
            }
            
            if (!managerController.TryGetAccount(inputtedReceiver, out var receivingAccount))
            {
                Console.WriteLine(Responses.invalidId);
                continue;
            }
            
            var inputtedAmount = Parse.Amount();

            switch (inputtedAmount)
            {
                case null:
                    return;
                case -1:
                    Console.WriteLine(Responses.nonNegative);
                    continue;
            }
            
            isSuccessful = sendingAccount.TryMakeTransfer(receivingAccount, (decimal)inputtedAmount);

            Console.WriteLine(isSuccessful
                ? $"Successfully transferred ${inputtedAmount:F2} to from Id: {inputtedSender} to Id: {inputtedReceiver}."
                : Responses.lessThanBalance);
        }
    }
}