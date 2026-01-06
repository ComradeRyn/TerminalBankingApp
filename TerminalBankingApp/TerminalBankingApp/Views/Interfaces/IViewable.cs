namespace TerminalBankingApp.Views.Interfaces;

using TerminalBankingApp.Controllers;

public interface IViewable
{
    public void Handle(BankController managerController);

    public string GetActionName();
}