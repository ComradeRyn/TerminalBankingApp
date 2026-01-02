namespace TerminalBankingApp.Requests;

public class CheckBalanceRequest : IRequest
{
    private readonly Account _selectedAccount;

    public CheckBalanceRequest(Account account)
    {
        _selectedAccount = account;
    }
    
    public string PerformRequest()
        => $"Account Balance of ${_selectedAccount.Balance:F2}.";
}