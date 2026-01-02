namespace TerminalBankingApp.Requests;

public class ExitRequest : IRequest
{
    public string PerformRequest()
    {
        return $"Exit Confirmed: Have a nice day!";
    }
}