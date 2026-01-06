using TerminalBankingApp.Views;
using TerminalBankingApp.Controllers;
using TerminalBankingApp.Views.Interfaces;

/* Things to do:
 * - Implement IViewable *done*
 *      - handle()
 *      - success()
 *      - failure()
 *
 * - Have programs go through and run from a main array that holds a bunch of the interface instances *done*
 * - Make it so rather than having the AccountManagerController being a static object, it is passed from instance to instance *done*
 * - Deal with the success and fail states *done*
 * - Tweak the check methods into the 'try' format *done*
 * - Make it clear where the failure point is *done*
 * - Possibly make it so the private fields in the controllers are private get public set parameters 
 * 
 */

var views = new IViewable[] {new MainMenuView(), new CreateAccountView(), new DepositView(), new WithdrawView(), new CheckAccountBalanceView(), new TransferFundsView()};
var managerController = new BankController();

var isRunning = true;
var viewSelection = 0;

Console.WriteLine(
    "\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");

while (isRunning)
{
    views[0].Handle(managerController);

    var userInput = Console.ReadLine();
    
    if (!int.TryParse(userInput, out viewSelection))
    {
        viewSelection = -1;
    }
    
    try
    {
        Console.WriteLine("Type \"exit\" to return to main menu");
        views[viewSelection].Handle(managerController);
    }

    catch(IndexOutOfRangeException e)
    {
        if (viewSelection == 9)
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


