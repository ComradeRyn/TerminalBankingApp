namespace TerminalBankingApp;

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