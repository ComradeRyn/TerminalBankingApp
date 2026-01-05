using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public static class MainMenuViewer
{
    // Is this correct practice?
    // Should I make a static controller, or should I make a controller that is passed between each of the viewers?
    public static AccountController Controller = new AccountController();
    
    public static void Start()
    {
        Console.WriteLine("\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");

        var continueRunning = true;
        
        do
        {
            Console.WriteLine(@"
            1: Create Account 
            2: Make a Deposit 
            3: Make a Withdraw 
            4: Check an account balance 
            5: Transfer Funds 
            9: exit");
            
            var input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    CreateAccountViewer.CreateAccountView();
                    break;
                case "2":
                    DepositViewer.DepositView();
                    break;
                case "3":
                    WithdrawViewer.WithdrawView();
                    break;
                case "4":
                   CheckAccountBalanceViewer.CheckAccountBalanceView();
                    break;
                case "5":
                    TransferFundsViewer.TransferFundsView();
                    break;
                case "9":
                    Console.WriteLine("Exit Confirmed: Have a nice day!");
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Not a valid command");
                    break;
            }
        } while (continueRunning);
    }
    
    public static string ParseAccount()
    {
        Console.Write("Enter account ID: ");
        var inputtedId = Console.ReadLine();

        return inputtedId;
    }

    public static decimal? ParseAmount()
    {
        decimal amount;

        Console.Write("Enter a money amount: ");
        
        var inputtedAmount = Console.ReadLine();
        
        if (inputtedAmount == "exit" || !decimal.TryParse(inputtedAmount, out amount))
        {
            return null;
        }

        return amount;
    }
}