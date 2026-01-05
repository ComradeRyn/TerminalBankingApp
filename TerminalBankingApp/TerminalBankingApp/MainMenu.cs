namespace TerminalBankingApp;

using TerminalBankingApp.Requests;

public class MainMenu
{
    public static void Start()
    {
        Console.WriteLine("\nWelcome to my Terminal Banking App! Please select one of the following numbers for the corresponding option:");
        
        string input;
        var continueRunning = true;
        var accountManager = new AccountManager();
        
        do
        {
            Console.WriteLine(@"
            1: Create Account 
            2: Make a Deposit 
            3: Make a Withdraw 
            4: Check an account balance 
            5: Transfer Funds 
            9: exit");
            
            input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    Create(accountManager);
                    break;
                case "2":
                    Deposit(accountManager);
                    break;
                case "3":
                    Withdraw(accountManager);
                    break;
                case "4":
                    CheckBalance(accountManager);
                    break;
                case "5":
                    Transfer(accountManager);
                    break;
                case "9":
                    Console.WriteLine("Exit Confirmed: Have a nice day!");
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Not a valid command");
                    break;
            }
        } while (continueRunning);
    }
    private static bool ParseAccount(AccountManager manager, out Account? retrievedAccount)
    {
        string inputtedId;
        do
        {
            Console.Write("Enter account ID: ");
            inputtedId = Console.ReadLine();

            retrievedAccount = manager.GetAccount(inputtedId);
        } while (retrievedAccount is null && inputtedId != "exit");
        

        return retrievedAccount is not null;
    }
    
    private static bool ParseAmount(out decimal amount)
    {
        string inputtedAmount;
        do
        {
            Console.Write("Enter a money amount: ");
            inputtedAmount = Console.ReadLine();
        } while (!decimal.TryParse(inputtedAmount, out amount) && inputtedAmount != "exit");
        

        return decimal.TryParse(inputtedAmount, out amount);
    }
    
    private static bool ParseAccountAndAmount(AccountManager manager, 
        out Account? retrievedAccount, 
        out decimal amount)
    {
        var accountSuccessful = ParseAccount(manager, out retrievedAccount);
        
        if (!accountSuccessful)
        {
            amount = 0;
            return false;
        }
        
        var amountSuccessful = ParseAmount(out amount);

        return amountSuccessful;
    }

    
    private static void Deposit(AccountManager manager)
    {
        Console.WriteLine("type \"exit\" to return back to main menu");
        
        Account? retrievedAccount;
        decimal retrievedAmount;

        var parseSuccessful = ParseAccountAndAmount(manager, out retrievedAccount, out retrievedAmount);

        if (parseSuccessful)
        {
            //var request = new DepositRequest(retrievedAccount, retrievedAmount);
            //Console.WriteLine(request.PerformRequest());
            var depositSuccessful = retrievedAccount.MakeDeposit(retrievedAmount);

            if (depositSuccessful)
            {
                Console.WriteLine($"Successfully deposited ${retrievedAmount:F2}. \nNew Balance: ${retrievedAccount.Balance:F2}"); 
            }
            else
            {
                Console.WriteLine("Action failed: Deposit amount must be positive");
            }
        }
    }

    private static void Withdraw(AccountManager manager)
    {
        Console.WriteLine("type \"exit\" to return back to main menu");
        
        Account? retrievedAccount;
        decimal retrievedAmount;

        var parseSuccessful = ParseAccountAndAmount(manager, out retrievedAccount, out retrievedAmount);

        if (parseSuccessful)
        {
            // var request = new WithdrawRequest(retrievedAccount, retrievedAmount);
            // Console.WriteLine(request.PerformRequest());

            var withdrawSuccessful = retrievedAccount.MakeWithdraw(retrievedAmount);

            if (withdrawSuccessful)
            {
                Console.WriteLine($"Successfully withdrew ${retrievedAmount:F2}. \nNew Balance: ${retrievedAccount.Balance:F2}");
            }

            else
            {
                Console.WriteLine("Action failed: Withdraw amount must be positive and less than or equal to your Balance!");
            }
        }
    }

    private static void Transfer(AccountManager manager)
    {
        Console.WriteLine("type \"exit\" to return back to main menu");
        
        Account? sender;
        Account? receiver;
        decimal amount;
        
        Console.WriteLine("Sending account and amount");

        var firstParseSuccessful = ParseAccountAndAmount(manager, out sender, out amount);
        
        if (!firstParseSuccessful)
        {
            return;
        }

        Console.WriteLine("Receiving account");
        var secondParseSuccessful = ParseAccount(manager, out receiver);

        if (!secondParseSuccessful)
        {
            return;
        }
        
        // var request = new TransferRequest(receiver, sender, amount);
        // Console.WriteLine(request.PerformRequest());

        var transferSuccessful = sender.MakeTransfer(receiver, amount);

        if (transferSuccessful)
        {
            Console.WriteLine($"Successfully transferred ${amount:F2}.");
        }
        
        else
        {
            Console.WriteLine("Transfer failed: Transfer amount must be positive and within limits of sender's account.");
        }
    }

    private static void Create(AccountManager manager)
    {
        Console.WriteLine("type \"exit\" to return back to main menu");
        Console.WriteLine("Names should only be composed of letters in format of <first name> <second name> <...> <last name>");
        
        //AccountCreationRequest request;
        Account newAccount;
        do
        {
            Console.Write("Enter the name of the account holder: ");
            var holderName = Console.ReadLine();
            //request = new AccountCreationRequest(manager, holderName);

            if (holderName == "exit")
            {
                return;
            }

            newAccount = manager.CreateAccount(holderName);
        } while (newAccount == null);
            
        Console.WriteLine($"Account successfully created under {newAccount.HolderName} with Id of {newAccount.Id}");
    }

    private static void CheckBalance(AccountManager manager)
    {
        Console.WriteLine("type \"exit\" to return back to main menu");
        
        Account? inputtedAccount;
        var parseSuccessful = ParseAccount(manager, out inputtedAccount);

        if (parseSuccessful)
        {
            //var request = new CheckBalanceRequest(inputtedAccount);
            Console.WriteLine($"Account Balance of ${inputtedAccount.Balance:F2}.");
        }
    }
}
