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
    
    // public Account CreateAccount(string name)
    // {
    //     var newAccount = new Account(name);
    //     _accounts.Add(newAccount);
    //
    //     return newAccount;
    // }

    public Account? CreateAccount(string name)
    {
        if (!ValidateName(name))
        {
            return null;
        }

        var newAccount = new Account(name);
        _accounts.Add(newAccount);

        return newAccount;
    }
    
    private bool ValidateName(string accountName)
    {
        var nameTokens = accountName.Split(" ");
        return nameTokens.All(name => name.All(char.IsLetter) && name != "");
    }
    
    
}