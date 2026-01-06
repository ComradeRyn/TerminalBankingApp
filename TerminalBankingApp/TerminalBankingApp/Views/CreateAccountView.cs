using TerminalBankingApp.Controllers;

namespace TerminalBankingApp.Views;

public class CreateAccountView
{
    public static void CreateAccount()
    {
        Console.WriteLine("Accounts names must be of the structure <first name> <second name> ... <last name> with only letters");
        Console.WriteLine("Type \"exit\" to return to main menu");
        
        var inputtedName = "";
        string? newAccountId = null;

        while (newAccountId == null)
        {
            Console.Write("Enter account name: ");
            inputtedName = Console.ReadLine();

            if (inputtedName == "exit")
            {
                return;
            }

            newAccountId = MainMenuView.ManagerController.CreateAccount(inputtedName);
        }
        
        Console.WriteLine($"Created new account under name of {inputtedName} with an Id of {newAccountId}");
    }
}