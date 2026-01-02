namespace TerminalBankingApp;

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