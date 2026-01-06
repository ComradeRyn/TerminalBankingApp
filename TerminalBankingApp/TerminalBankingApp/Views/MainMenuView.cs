using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public static class MainMenuView
{
    // TODO: Switch out the uses of the AccountController with the AccountManagerController
    // TODO: Check on naming conventions and syntax
    // will need to make different instances of AccountControllers within each call to allow access to the controllers
    
    //public static AccountController Controller = new AccountController();
    public static AccountManagerController ManagerController = new AccountManagerController();
    
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
                    CreateAccountView.CreateAccount();
                    break;
                case "2":
                    DepositView.Deposit();
                    break;
                case "3":
                    WithdrawView.Withdraw();
                    break;
                case "4":
                   CheckAccountBalanceViewer.CheckAccountBalance();
                    break;
                case "5":
                    TransferFundsView.TransferFunds();
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
    
    public static string? ParseAccount()
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
        
        if (inputtedAmount == "exit")
        {
            return null;
        }
        
        if(!decimal.TryParse(inputtedAmount, out amount))
        {
            return -1;
        }

        return amount;
    }
}