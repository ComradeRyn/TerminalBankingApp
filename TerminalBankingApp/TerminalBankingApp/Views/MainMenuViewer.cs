using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public static class MainMenuViewer
{
    // Is this correct practice?
    // Should I make a static controller, or should I make a controller that is passed between each of the viewers?
    static AccountController controller = new AccountController();
    
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
                    //Create(accountManager);
                    break;
                case "2":
                    //Deposit(accountManager);
                    break;
                case "3":
                    //Withdraw(accountManager);
                    break;
                case "4":
                   //CheckBalance(accountManager);
                    break;
                case "5":
                    //Transfer(accountManager);
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
        string inputtedId;
        
        Console.Write("Enter account ID: ");
        inputtedId = Console.ReadLine();

        return inputtedId;
    }

    public static decimal ParseAmount()
    {
        decimal amount;

        Console.Write("Enter a money amount: ");
        var inputtedAmount = Console.ReadLine();
        decimal.TryParse(inputtedAmount, out amount);

        return amount;
    }
}