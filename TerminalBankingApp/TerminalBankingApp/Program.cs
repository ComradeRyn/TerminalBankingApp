using TerminalBankingApp.Views;
using TerminalBankingApp.Controllers;
using TerminalBankingApp.Views.Interfaces;

var views = new IViewable[] {
    new CreateAccountView(), 
    new DepositView(), 
    new WithdrawView(), 
    new CheckAccountBalanceView(), 
    new TransferFundsView()
};

var managerController = new BankController();

var isRunning = true;

Console.WriteLine(
    "\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");

while (isRunning)
{
    for (var i = 0; i < views.Length; i++)
    {
        Console.WriteLine($"{i + 1}: {views[i].GetActionName()}");
    }
    Console.WriteLine("q: exit");
    
    var userInput = Console.ReadLine();

    if (!int.TryParse(userInput, out var viewSelection))
    {
        viewSelection = -1;
    }
    
    try
    {
        views[viewSelection - 1].Handle(managerController);
    }

    catch(IndexOutOfRangeException e)
    {
        if (userInput == "q")
        {
            isRunning = false;
            Console.WriteLine("Exit Confirmed: Have a nice day!");
        }

        else
        {
            Console.WriteLine("Invalid input: Must choose input from menu options");
        }
    }
}


