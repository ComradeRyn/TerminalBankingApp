namespace TerminalBankingApp.Requests;

//Reports the Balance of the requested account
public class CheckBalanceRequest : IRequest
{
    private Account selectedAccount;

    public CheckBalanceRequest(Account account)
    {
        selectedAccount = account;
    }

    //Returns the amount left in the inputted account
    public string PreformRequest()
    {
        return $"Account Balance of ${selectedAccount.Balance.ToString("F2")}.";
    }
}