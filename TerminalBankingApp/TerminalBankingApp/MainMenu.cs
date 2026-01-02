namespace TerminalBankingApp;

using TerminalBankingApp.Requests;

//Gathers userinput and responds accordingly
public class MainMenu
{
    //Parse the inputted acount and return if succesful or not
    private static bool ParseAccount(AccountManager manager, out Account? retrievedAccount)
    {
        //Read input and retrieve the account
        Console.Write("Enter account ID: ");
        string? inputtedID = Console.ReadLine();

        retrievedAccount = manager.GetAccount(inputtedID);

        //If the given account does not exist, return that the request was invalid
        if (retrievedAccount == null)
        {
            return false;
        }

        return true;
    }

    //Parse the inputted amount and return if succesful or not
    private static bool ParseAmount(out decimal amount)
    {
        //Gets the amount from the user1
        Console.Write("Enter a money amount: ");
        string inputtedAmount = Console.ReadLine();
        
        //If input valid, go through with request
        if (decimal.TryParse(inputtedAmount, out amount))
        {
            return true;
        }
        
        //If not, request is invalid
        else
        {
            return false;
        }
    }

    //Parses an inputted ID and amount and returns if it was successful or not
    private static bool ParseAccountAndAmount(AccountManager manager, out Account? retrievedAccount, out decimal amount)
    {
        //Check if inputted account exists
        bool AccountSuccessful = ParseAccount(manager, out retrievedAccount);

        //If not, end prematurly
        if (!AccountSuccessful)
        {
            amount = 0;
            return false;
        }

        //Check if the inputted amount is formated correctly
        bool AmountSuccessful = ParseAmount(out amount);

        return AmountSuccessful;
    }
    
    //Series of methods with preform the respective function they are named after
    private static IRequest Deposit(AccountManager manager)
    {
        Account? retrievedAccount;
        decimal retrievedAmount;
        
        bool parseSucessful = ParseAccountAndAmount(manager, out retrievedAccount, out retrievedAmount);
        
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

        bool parseSucessful = ParseAccountAndAmount(manager, out retrievedAccount, out retrievedAmount);

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
        Account? reciever;
        decimal amount;
        
        Console.WriteLine("Sending account and amount");

        bool firstParseSuccessful = ParseAccountAndAmount(manager, out sender, out amount);
        
        if (!firstParseSuccessful)
        {
            return new InvalidRequest();
        }

        Console.WriteLine("Recieving account");
        bool secondParseSuccessful = ParseAccount(manager, out reciever);

        if (!secondParseSuccessful)
        {
            return new InvalidRequest();
        }

        return new TransferRequest(reciever, sender, amount);
    }

    private static IRequest Create(AccountManager manager)
    {
        Console.Write("Enter the name of the account holder: ");
        string holderName = Console.ReadLine();
        return new AccountCreationRequest(manager, holderName);
    }

    private static IRequest CheckBalance(AccountManager manager)
    {
        Account? inputtedAccount;
        bool parseSuccessful = ParseAccount(manager, out inputtedAccount);

        if (parseSuccessful)
        {
            return new CheckBalanceRequest(inputtedAccount);
        }
        else
        {
            return new InvalidRequest();
        }
    }

    //The enter point to the program
    public static void start()
    {
        Console.WriteLine("\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");
        
        string input;
        bool continueRunning = true;
        AccountManager accoutManager = new AccountManager();
        
        do
        {
            Console.WriteLine("1: Create Account \n2: Make a Deposit \n3: Make a Withdraw \n4: Check an account balance \n5: Transfer Funds \n9: exit");
            
            //Reads the inital user input
            input = Console.ReadLine();
            
            //A request that will be populated by the switch statement, and excuted afterwards
            IRequest request;

            //switch case that handles the inital user input
            switch (input)
            {
                case "1":
                    Console.WriteLine("This will create an account!");
                    request = Create(accoutManager);
                    break;
                case "2":
                    Console.WriteLine("This will make a deposit!");
                    request = Deposit(accoutManager);
                    break;
                case "3":
                    Console.WriteLine("This will make a withdraw!");
                    request = Withdraw(accoutManager);
                    break;
                case "4":
                    Console.WriteLine("This will check an account balance!");
                    request = CheckBalance(accoutManager);
                    break;
                case "5":
                    Console.WriteLine("This will make an account transfer!");
                    request = Transfer(accoutManager);
                    break;
                case "9":
                    request = new ExitRequest();
                    continueRunning = false;
                    break;
                default:
                    request = new InvalidRequest();
                    break;
            }

            //Preform the given request, and print the results from it
            string requestCompletionMessage = request.PreformRequest();
            Console.WriteLine("\n" + requestCompletionMessage + "\n");
            
        } while (continueRunning);
    }
}
