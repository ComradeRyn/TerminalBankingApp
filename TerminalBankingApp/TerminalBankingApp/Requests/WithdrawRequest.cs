namespace TerminalBankingApp.Requests;

public class WithdrawRequest : IRequest
{
    private readonly Account _selectedAccount;
    private readonly decimal _withdrawAmount;

    public WithdrawRequest(Account account, decimal amount)
    {
        _selectedAccount = account;
        _withdrawAmount = amount;
    }
    
    public bool ValidateAmount()
        => _withdrawAmount > 0 && _withdrawAmount <= _selectedAccount.Balance;
    
    public string PerformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Withdraw amount must be positive and less than or equal to your Balance!";
        }

        _selectedAccount.UpdateBalance(_withdrawAmount * -1);
        return $"Successfully withdrew ${_withdrawAmount:F2}. \nNew Balance: ${_selectedAccount.Balance:F2}";
    }
}