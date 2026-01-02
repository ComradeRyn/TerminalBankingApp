namespace TerminalBankingApp;

public class AccountManager
{
    private List<Account> _accounts;

    public AccountManager()
    {
        _accounts = new List<Account>();
    }
    
    public Account? GetAccount(string id) 
        => _accounts.FirstOrDefault(account => account.Id.ToString() == id);
    
    public Account CreateAccount(string name)
    {
        var newAccount = new Account(name);
        _accounts.Add(newAccount);

        return newAccount;
    }
}