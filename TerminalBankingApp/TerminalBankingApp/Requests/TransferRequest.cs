namespace TerminalBankingApp.Requests;

//Attempts to preform a transfer, then reports the results
public class TransferRequest : IRequest
{
    private DepositRequest recipient;
    private WithdrawRequest sender;
    private decimal amount;

    public TransferRequest(Account recipientAccount, Account senderAccount, decimal amount)
    {
        this.amount = amount;
        recipient = new DepositRequest(recipientAccount, amount);
        sender = new WithdrawRequest(senderAccount, amount);
    }

    //Checks if boths requests are valid
    private bool ValidateRequest()
    {
        return recipient.ValidateAmount() && sender.ValidateAmount();
    }
    
    //Varifies the transfer and if legal, performs it then reports results
    public string PreformRequest()
    {
        if (!ValidateRequest())
        {
            return $"Transfer failed: Transfer amount must be positive and within limits of sender's account.";
        }

        recipient.PreformRequest();
        sender.PreformRequest();

        return $"Successfully transfered ${amount.ToString("F2")}.";
    }
}