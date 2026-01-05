namespace TerminalBankingApp.Models;

public class Account(string name)
{
    public Guid Id { get; } = Guid.NewGuid();

    public string HolderName { get; } = name;

    public decimal Balance { get; set; }
}