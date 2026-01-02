namespace TerminalBankingApp.Requests;

public class DepositRequest : IRequest
{
    private readonly Account _selectedAccount;
    private readonly decimal _depositAmount;

    public DepositRequest(Account account, decimal amount)
    {
        _selectedAccount = account;
        _depositAmount = amount;
    }
    public bool ValidateAmount()
        => _depositAmount > 0;
    
    public string PerformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Deposit amount must be positive";
        }

        _selectedAccount.UpdateBalance(_depositAmount);
        return $"Successfully deposited ${_depositAmount:F2}. \nNew Balance: ${_selectedAccount.Balance:F2}";
    }
}