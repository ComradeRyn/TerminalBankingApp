namespace TerminalBankingApp;

using TerminalBankingApp.Requests;

public class MainMenu
{
    public static void Start()
    {
        Console.WriteLine("\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");
        
        string input;
        var continueRunning = true;
        var accountManager = new AccountManager();
        
        do
        {
            Console.WriteLine(@"
            1: Create Account 
            2: Make a Deposit 
            3: Make a Withdraw 
            4: Check an account balance 
            5: Transfer Funds 
            9: exit");
            
            input = Console.ReadLine();
            
            IRequest request;
            
            switch (input)
            {
                case "1":
                    request = Create(accountManager);
                    break;
                case "2":
                    request = Deposit(accountManager);
                    break;
                case "3":
                    request = Withdraw(accountManager);
                    break;
                case "4":
                    request = CheckBalance(accountManager);
                    break;
                case "5":
                    request = Transfer(accountManager);
                    break;
                case "9":
                    request = new ExitRequest();
                    continueRunning = false;
                    break;
                default:
                    request = new InvalidRequest();
                    break;
            }
            
            var requestCompletionMessage = request.PerformRequest();
            Console.WriteLine("\n" + requestCompletionMessage + "\n");
            
        } while (continueRunning);
    }
    private static bool ParseAccount(AccountManager manager, out Account? retrievedAccount)
    {
        Console.Write("Enter account ID: ");
        var inputtedId = Console.ReadLine();

        retrievedAccount = manager.GetAccount(inputtedId);

        return retrievedAccount is not null;
    }
    
    private static bool ParseAmount(out decimal amount)
    {
        Console.Write("Enter a money amount: ");
        var inputtedAmount = Console.ReadLine();

        return decimal.TryParse(inputtedAmount, out amount);
    }
    
    private static bool ParseAccountAndAmount(AccountManager manager, 
        out Account? retrievedAccount, 
        out decimal amount)
    {
        var accountSuccessful = ParseAccount(manager, out retrievedAccount);
        
        if (!accountSuccessful)
        {
            amount = 0;
            return false;
        }
        
        var amountSuccessful = ParseAmount(out amount);

        return amountSuccessful;
    }
    
    private static IRequest Deposit(AccountManager manager)
    {
        Account? retrievedAccount;
        decimal retrievedAmount;
        
        var parseSucessful = ParseAccountAndAmount(manager, out retrievedAccount, out retrievedAmount);
        
        if (parseSucessful)
        {
            return new DepositRequest(retrievedAccount, retrievedAmount);
        }
        
        else
        {
            return new InvalidRequest();
        }
    }

    private static IRequest Withdraw(AccountManager manager)
    {
        Account? retrievedAccount;
        decimal retrievedAmount;

        var parseSucessful = ParseAccountAndAmount(manager, out retrievedAccount, out retrievedAmount);

        if (parseSucessful)
        {
            return new WithdrawRequest(retrievedAccount, retrievedAmount);
        }
        
        else
        {
            return new InvalidRequest();
        }
    }

    private static IRequest Transfer(AccountManager manager)
    {
        Account? sender;
        Account? receiver;
        decimal amount;
        
        Console.WriteLine("Sending account and amount");

        var firstParseSuccessful = ParseAccountAndAmount(manager, out sender, out amount);
        
        if (!firstParseSuccessful)
        {
            return new InvalidRequest();
        }

        Console.WriteLine("Receiving account");
        var secondParseSuccessful = ParseAccount(manager, out receiver);

        if (!secondParseSuccessful)
        {
            return new InvalidRequest();
        }

        return new TransferRequest(receiver, sender, amount);
    }

    private static IRequest Create(AccountManager manager)
    {
        Console.Write("Enter the name of the account holder: ");
        var holderName = Console.ReadLine();
        return new AccountCreationRequest(manager, holderName);
    }

    private static IRequest CheckBalance(AccountManager manager)
    {
        Account? inputtedAccount;
        var parseSuccessful = ParseAccount(manager, out inputtedAccount);

        if (parseSuccessful)
        {
            return new CheckBalanceRequest(inputtedAccount);
        }
        else
        {
            return new InvalidRequest();
        }
    }
}
