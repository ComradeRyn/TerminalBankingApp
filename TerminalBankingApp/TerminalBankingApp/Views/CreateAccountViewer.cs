namespace TerminalBankingApp.Views;

public class CreateAccountViewer
{
    public static void CreateAccountView()
    {
        Console.WriteLine("Accounts names must be of the structure <first name> <second name> ... <last name> with only letters");
        Console.WriteLine("Type \"exit\" to return to main menu");
        Account? newAccount = null;

        while (newAccount == null)
        {
            Console.Write("Enter account name: ");
            var inputtedName = Console.ReadLine();

            if (inputtedName == "exit")
            {
                return;
            }

            newAccount = MainMenuViewer.Controller.CreateAccount(inputtedName);
        }
        
        Console.WriteLine($"Created new account under name of {newAccount.HolderName} with an Id of {newAccount.Id}");
    }
}