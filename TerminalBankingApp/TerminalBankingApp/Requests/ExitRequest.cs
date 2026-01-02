namespace TerminalBankingApp.Requests;

//Generated when menu is exited
public class ExitRequest : IRequest
{
    //Reports that exit was confirmed
    public string PreformRequest()
    {
        return $"Exit Confirmed: Have a nice day!";
    }
}