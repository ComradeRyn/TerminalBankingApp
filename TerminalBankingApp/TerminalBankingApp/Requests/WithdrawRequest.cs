namespace TerminalBankingApp.Requests;

//Attempts to withdraw requested amount of money and reports result
public class WithdrawRequest : IRequest
{
    private Account selectedAccount;
    private decimal withdrawAmount;

    public WithdrawRequest(Account account, decimal amount)
    {
        selectedAccount = account;
        withdrawAmount = amount;
    }

    //Checks if amount is possitive and if the requested amount is less than or equal to the user account balance
    public bool ValidateAmount()
    {
        return withdrawAmount > 0 && withdrawAmount <= selectedAccount.Balance;
    }

    //Validates input withdraws amount if valid. Reports results back
    public string PreformRequest()
    {
        if (!ValidateAmount())
        {
            return "Action failed: Withdraw amount must be positive and less than or equal to your Balance!";
        }

        selectedAccount.WithdrawFunds(withdrawAmount);
        return $"Successfully withdrew ${withdrawAmount.ToString("F2")}. \nNew Balance: ${selectedAccount.Balance.ToString("F2")}";
    }
}