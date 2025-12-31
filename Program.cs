#:property PublishAot=false

public class Program
{
    public static void Main(string[] args)
    {
        
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
    private static List<Account> accounts = new List<Account>();
    
    //Retrieves an account based off requested id
    public static Account? GetAccount(string id)
    {
        //TODO: what happens when the returned value is null
        return accounts.FirstOrDefault(account => account.Id.ToString() == id, null);
    }

    //Creates account with provided name
    public static void CreateAccount(string name)
    {
        Account newAccount = new Account(name);
        accounts.Add(newAccount);
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
