using TerminalBankingApp.Controllers;
using TerminalBankingApp.Views.Interfaces;

namespace TerminalBankingApp.Views;

public class MainMenuView : IViewable
{
    /* General Questions:
     * Are my method names within the view classes currently appropriate in this context?
     * Should each of the view classes be static because they are never instantiated and only going to be called within this class?
     * Is it appropriate to have the instance 'managerController' as static? Current reasoning is there will only ever exist one, and it is required in the AccountController, so I just access directly in each of the child classes
     * Is it appropriate to use classes that exist within the Controllers folder in other classes within the same folder? (ex: using the AccountManagerController within the AccountController)
     * Should input validation be done in the controller or within the view before passing to controller
     */
    
    public void Handle(BankController managerController)
    {
        Console.WriteLine(@"
            1: Create Account 
            2: Make a Deposit 
            3: Make a Withdraw 
            4: Check an account balance 
            5: Transfer Funds 
            9: exit");
    }
}

    