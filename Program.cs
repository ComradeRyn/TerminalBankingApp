#:property PublishAot=false

public class Program
{
    public static void Main(string[] args)
    {
        //Run the program
        MainMenu.start();
    }
}

//Data structure which represents a Bank Account
public class Account
{
    //ID which will be used whenever a new Account is created
    public Guid Id { get; init; }

    public string HolderName { get; init; }

    public decimal Balance { get; set; }

    //Initializes all data and increments the unique ID value for the next account
    public Account(string name)
    {
        HolderName = name;
        Id = Guid.NewGuid();
        Balance = 0;
    }
    
    //Modifies balance
    public void DepositFunds(decimal amount)
    {
        Balance += amount;
    }
    
    public void WithdrawFunds(decimal amount)
    {
        Balance -= amount;
    }
}

//Stores all accounts and permits access to said accounts
public class AccountManager
{
    //Holds all Bank Accounts
    private List<Account> accounts;

    public AccountManager()
    {
        accounts = new List<Account>();
    }
    
    //Retrieves an account based off requested id
    public Account? GetAccount(string id)
    {
        return accounts.FirstOrDefault(account => account.Id.ToString() == id, null);
    }

    //Creates account with provided name and returns the newly created account
    public Account CreateAccount(string name)
    {
        Account newAccount = new Account(name);
        accounts.Add(newAccount);

        return newAccount;
    }
    
}

//Interface for all Request objects
public interface IRequest
{
    public string PreformRequest();
}

//Attempts to deposit requested amount of money and reports result
public class DepositRequest : IRequest
{
    private Account selectedAccount;
    private decimal depositAmount;

    public DepositRequest(Account account, decimal amount)
    {
        selectedAccount = account;
        depositAmount = amount;
    }
    //Validates if the amount is possitive
    public bool ValidateAmount()
    {
        return depositAmount > 0;
    }
    
    //If input is valid, deposit money and report back results
    public string PreformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Deposit amount must be positive";
        }

        selectedAccount.DepositFunds(depositAmount);
        return $"Successfully deposited ${depositAmount.ToString("F2")}. \nNew Balance: ${selectedAccount.Balance.ToString("F2")}";
    }
}

//Attempts to withdraw requested amount of money and reports result
public class WithdrawRequest : IRequest
{
    private Account selectedAccount;
    private decimal withdrawAmount;

    public WithdrawRequest(Account account, decimal amount)
    {
        selectedAccount = account;
        withdrawAmount = amount;
    }

    //Checks if amount is possitive and if the requested amount is less than or equal to the user account balance
    public bool ValidateAmount()
    {
        return withdrawAmount > 0 && withdrawAmount <= selectedAccount.Balance;
    }

    //Validates input withdraws amount if valid. Reports results back
    public string PreformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Withdraw amount must be positive and less than or equal to your Balance!";
        }

        selectedAccount.WithdrawFunds(withdrawAmount);
        return $"Successfully withdrew ${withdrawAmount.ToString("F2")}. \nNew Balance: ${selectedAccount.Balance.ToString("F2")}";
    }
}

//Attempts to preform a transfer, then reports the results
public class TransferRequest : IRequest
{
    private DepositRequest recipient;
    private WithdrawRequest sender;
    private decimal amount;

    public TransferRequest(Account recipientAccount, Account senderAccount, decimal amount)
    {
        this.amount = amount;
        recipient = new DepositRequest(recipientAccount, amount);
        sender = new WithdrawRequest(senderAccount, amount);
    }

    //Checks if boths requests are valid
    private bool ValidateRequest()
    {
        return recipient.ValidateAmount() && sender.ValidateAmount();
    }
    
    //Varifies the transfer and if legal, performs it then reports results
    public string PreformRequest()
    {
        if (!ValidateRequest())
        {
            return $"Transfer failed: Transfer amount must be positive and within limits of sender's account.";
        }

        recipient.PreformRequest();
        sender.PreformRequest();

        return $"Successfully transfered ${amount.ToString("F2")}.";
    }
}

//Creates an account and reports the results
public class AccountCreationRequest : IRequest
{
    private AccountManager accountManager;
    private string accountName;

    public AccountCreationRequest(AccountManager manager, string name)
    {
        accountName = name;
        accountManager = manager;
    }

    //Creates account and reports results back
    public string PreformRequest()
    {
        Account newAccount = accountManager.CreateAccount(accountName);
        return $"Account successfully created under {accountName} with Id of {newAccount.Id}";
    }
}

//Reports the Balance of the requested account
public class CheckBalanceRequest : IRequest
{
    private Account selectedAccount;

    public CheckBalanceRequest(Account account)
    {
        selectedAccount = account;
    }

    //Returns the amount left in the inputted account
    public string PreformRequest()
    {
        return $"Account Balance of ${selectedAccount.Balance.ToString("F2")}.";
    }
}

//Generated when an invalid input is passed in main menu
public class InvalidRequest : IRequest
{
    //Reports that request was invalid
    public string PreformRequest()
    {
        return "Invalid input";
    }
}

//Generated when menu is exited
public class ExitRequest : IRequest
{
    //Reports that exit was confirmed
    public string PreformRequest()
    {
        return $"Exit Confirmed: Have a nice day!";
    }
}

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
