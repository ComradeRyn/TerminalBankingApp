using TerminalBankingApp.Views.Interfaces;
using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public class CreateAccountView : IViewable
{
    public void Handle(BankController bankController)
    {
        Console.WriteLine("Accounts names must be of the structure <first name> <second name> ... <last name> with only letters");
        var isSuccessful = false;

        while (!isSuccessful)
        {
            Console.Write("Enter account name: ");
            var inputtedName = Console.ReadLine();
            string? newAccountId;

            if (inputtedName == "exit")
            {
                return;
            }

            isSuccessful = bankController.TryCreateAccount(inputtedName, out newAccountId);

            if (isSuccessful)
            {
                Success(inputtedName, newAccountId);
            }
            
            else
            {
                Failure(inputtedName);
            }
        }
    }

    private void Success(string name, string id)
    {
        Console.WriteLine($"Created new account under name of {name} with an Id of {id}");
    }

    private void Failure(string name)
    {
        Console.WriteLine($"Invalid input: {name} does not follow the naming parameter.");
    }
}