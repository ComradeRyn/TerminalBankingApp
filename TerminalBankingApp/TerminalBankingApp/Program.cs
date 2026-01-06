using TerminalBankingApp.Views;
using TerminalBankingApp.Controllers;
using TerminalBankingApp.Views.Interfaces;

var views = new IViewable[] {
    new CreateAccountView(), 
    new DepositView(), 
    new WithdrawView(), 
    new CheckAccountBalanceView(), 
    new TransferFundsView()};

var managerController = new BankController();

var isRunning = true;

Console.WriteLine(
    "\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");

while (isRunning)
{
    var counter = 1;
    foreach (var view in views)
    {
        Console.WriteLine($"{counter}: {view.GetActionName()}");
        counter++;
    }
    Console.WriteLine($"{counter + 3}: exit");
    
    var userInput = Console.ReadLine();

    if (!int.TryParse(userInput, out var viewSelection) || viewSelection-- == 0)
    {
        viewSelection = -1;
    }
    
    try
    {
        views[viewSelection].Handle(managerController);
    }

    catch(IndexOutOfRangeException e)
    {
        if (viewSelection == counter + 2)
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


