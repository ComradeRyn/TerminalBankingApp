namespace TerminalBankingApp.Requests;

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