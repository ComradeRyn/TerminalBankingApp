namespace TerminalBankingApp.Requests;

public class AccountCreationRequest : IRequest
{
    private readonly AccountManager _accountManager;
    private readonly string _accountName;

    public AccountCreationRequest(AccountManager manager, string name)
    {
        _accountName = name;
        _accountManager = manager;
    }

    public bool ValidateName()
    {
        var nameTokens = _accountName.Split(" ");
        foreach (var name in nameTokens)
        {
            if (!name.All(char.IsLetter) || name == "")
            {
                return false; 
            }
        }

        return true;
    }
    
    public string PerformRequest()
    {
        var newAccount = _accountManager.CreateAccount(_accountName);
        return $"Account successfully created under {_accountName} with Id of {newAccount.Id}";
    }
}