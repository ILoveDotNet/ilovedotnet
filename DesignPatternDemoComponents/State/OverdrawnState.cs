namespace DesignPatternDemoComponents.State;

// Overdrawn state class
public class OverdrawnState(BankAccount account) : BankAccountState(account)
{
  public override void Deposit(decimal amount)
  {
    _account.Balance += amount;
    if (_account.Balance >= 0)
      _account.State = new RegularState(_account);
    Console.WriteLine("Deposited money in Overdrawn State.");
  }

  public override void Withdraw(decimal amount)
  {
    Console.WriteLine("Cannot withdraw in Overdrawn State.");
  }

  public override string ToString()
  {
    return "Overdrawn State";
  }
}
