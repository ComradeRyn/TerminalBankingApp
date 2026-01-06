using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public static class MainMenuView
{
    /* General Questions:
     * Are my method names within the view classes currently appropriate in this context?
     * Should each of the view classes be static because they are never instantiated and only going to be called within this class?
     * Is it appropriate to have the instance 'managerController' as static? Current reasoning is there will only ever exist one, and it is required in the AccountController, so I just access directly in each of the child classes
     * Was told about the 'Try' naming convention, what does it mean, and how should I apply it?
     * Is it appropriate to use classes that exist within the Controllers folder in other classes within the same folder? (ex: using the AccountManagerController within the AccountController)
     * Would it make sense to move the View helper functions out of the MainMenu class and into their own utilities class?
     */
    
    // Cannot be var, is this fine?
    public static readonly AccountManagerController ManagerController = new AccountManagerController();
    
    public static void Run()
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