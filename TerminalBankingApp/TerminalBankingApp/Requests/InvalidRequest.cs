namespace TerminalBankingApp.Requests;

//Generated when an invalid input is passed in main menu
public class InvalidRequest : IRequest
{
    //Reports that request was invalid
    public string PreformRequest()
    {
        return "Invalid input";
    }
}