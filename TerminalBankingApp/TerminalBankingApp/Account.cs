namespace TerminalBankingApp;

public class Account
{
    public Guid Id { get; init; }

    public string HolderName { get; init; }

    public decimal Balance { get; set; }
    
    public Account(string name)
    {
        HolderName = name;
        Id = Guid.NewGuid();
        Balance = 0;
    }
    
    public void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }
}