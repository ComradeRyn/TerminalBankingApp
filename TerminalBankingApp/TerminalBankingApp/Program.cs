using TerminalBankingApp.Views;
using TerminalBankingApp.Controllers;

MainMenuView.Run();

/* Things to do:
 * - Implement IViewable
 *      - handle()
 *      - success()
 *      - failure()
 *
 * - Have programs go through and run from a main array that holds a bunch of the interface instances
 * - Make it so rather than having the AccountManagerController being a static object, it is passed from instance to instance
 * - Deal with the success and fail states
 * - Tweak the check methods into the 'try' format
 * - Make it clear where the failure point is
 * - Possibly make it so the private fields in the controllers are private get public set parameters
 * 
 */

var views = new IViewable[] {new MainMenuView(), new CreateAccountView(), new WithdrawView(), new CheckAccountBalanceView(), new TransferFundsView()};
var managerController = new BankController();

var isRunning = true;
var viewSelection = 0;

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


