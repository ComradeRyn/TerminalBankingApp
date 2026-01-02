namespace TerminalBankingApp.Requests;

public class InvalidRequest : IRequest
{
    public string PerformRequest()
    {
        return "Invalid input";
    }
}