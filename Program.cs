#:property PublishAot=false

public class Program
{
    public static void Main(string[] args)
    {
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
    private List<Account> accounts = new List<Account>();
    
    //Retrieves an account based off requested id
    public Account? GetAccount(string id)
    {
        //TODO: what happens when the returned value is null
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
    public bool ValidateAmount()
    {
        return depositAmount > 0;
    }

    public string PreformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Deposit amount must be positive";
        }

        selectedAccount.DepositFunds(depositAmount);
        return $"Successfully deposited {depositAmount}. \n New Balance: {selectedAccount.Balance}";
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

    public bool ValidateAmount()
    {
        return withdrawAmount > 0 && withdrawAmount <= selectedAccount.Balance;
    }

    public string PreformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Withdraw amount must be positive and less than or equal to your Balance!";
        }

        selectedAccount.WithdrawFunds(withdrawAmount);
        return $"Successfully withdrew {withdrawAmount}. \n New Balance: {selectedAccount.Balance}";
    }
}

//Creates an account and ports the results
public class AccountCreationRequest : IRequest
{
    private AccountManager accountManager;
    private string accountName;

    public AccountCreationRequest(AccountManager manager, string name)
    {
        accountName = name;
        accountManager = manager;
    }

    public string PreformRequest()
    {
        Account newAccount = accountManager.CreateAccount(accountName);
        return $"Account successfully created under {accountName} with Id of {newAccount.Id}";
    }
}

//Generated when an invalid input is passed in main menu
public class InvalidRequest : IRequest
{
    public string PreformRequest()
    {
        return $"Invalid input";
    }
}

//Generated when menu is exited
public class ExitRequest : IRequest
{
    public string PreformRequest()
    {
        return $"Exit Confirmed: Have a nice day!";
    }
}

public class MainMenu
{
    //Need to complete each of these methods
    private static IRequest Deposit()
    {
        return new ExitRequest();
    }

    private static IRequest Withdraw()
    {
        return new ExitRequest();
    }

    private static IRequest Transfer()
    {
        return new ExitRequest();
    }

    private static IRequest Create()
    {
        return new ExitRequest();
    }

    private static IRequest CheckBalance()
    {
        return new ExitRequest();
    }

    public static void start()
    {
        Console.WriteLine("\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");
        string input;
        bool continueRunning = true;
        do
        {
            Console.WriteLine("1: Create Account \n2: Make a Deposit \n3: Make a Withdraw \n4: Check an account balance \n5: Transfer Funds \n9: exit");
            
            input = Console.ReadLine();
            IRequest request;

            switch (input)
            {
                case "1":
                    Console.WriteLine("This will create an account!");
                    request = Deposit();
                    break;
                case "2":
                    Console.WriteLine("This will make a deposit!");
                    request = Withdraw();
                    break;
                case "3":
                    Console.WriteLine("This will make a withdraw!");
                    request = Transfer();
                    break;
                case "4":
                    Console.WriteLine("This will check an account balance!");
                    request = Create();
                    break;
                case "5":
                    Console.WriteLine("This will make an account transfer!");
                    request = CheckBalance();
                    break;
                case "9":
                    request = new ExitRequest();
                    continueRunning = false;
                    break;
                default:
                    request = new InvalidRequest();
                    break;
            }

            string requestCompletionMessage = request.PreformRequest();
            Console.WriteLine(requestCompletionMessage);
        } while (continueRunning);
    }
}
