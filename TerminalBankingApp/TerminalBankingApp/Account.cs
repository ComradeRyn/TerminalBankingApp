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

    public bool MakeDeposit(decimal amount)
    {
        if (amount <= 0) return false;
        UpdateBalance(amount);
        return true;
    }

    public bool MakeWithdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance) return false;
        UpdateBalance(amount * -1);
        return true;
    }

    public bool MakeTransfer(Account recipient, decimal amount)
    {
        if (MakeWithdraw(amount))
        {
            return recipient.MakeDeposit(amount);
        }
        
        return false;
    }
    
    private void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }
}