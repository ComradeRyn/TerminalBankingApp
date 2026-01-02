namespace TerminalBankingApp.Requests;

//Attempts to deposit requested amount of money and reports result
public class DepositRequest : IRequest
{
    private Account selectedAccount;
    private decimal depositAmount;

    public DepositRequest(Account account, decimal amount)
    {
        selectedAccount = account;
        depositAmount = amount;
    }
    //Validates if the amount is possitive
    public bool ValidateAmount()
    {
        return depositAmount > 0;
    }
    
    //If input is valid, deposit money and report back results
    public string PreformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Deposit amount must be positive";
        }

        selectedAccount.DepositFunds(depositAmount);
        return $"Successfully deposited ${depositAmount.ToString("F2")}. \nNew Balance: ${selectedAccount.Balance.ToString("F2")}";
    }
}