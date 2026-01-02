namespace TerminalBankingApp.Requests;

public class TransferRequest : IRequest
{
    private readonly DepositRequest _recipient;
    private readonly WithdrawRequest _sender;
    private readonly decimal _amount;

    public TransferRequest(Account recipientAccount, Account senderAccount, decimal amount)
    {
        _amount = amount;
        _recipient = new DepositRequest(recipientAccount, amount);
        _sender = new WithdrawRequest(senderAccount, amount);
    }
    
    private bool ValidateRequest()
        => _recipient.ValidateAmount() && _sender.ValidateAmount();
    
    public string PerformRequest()
    {
        if (!ValidateRequest())
        {
            return $"Transfer failed: Transfer amount must be positive and within limits of sender's account.";
        }

        _recipient.PerformRequest();
        _sender.PerformRequest();

        return $"Successfully transferred ${_amount:F2}.";
    }
}