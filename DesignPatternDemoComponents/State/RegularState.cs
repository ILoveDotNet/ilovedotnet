namespace DesignPatternDemoComponents.State
{
    // Regular state class
    public class RegularState(BankAccount account) : BankAccountState(account)
    {
        public override void Deposit(decimal amount)
        {
            _account.Balance += amount;
            Console.WriteLine("Deposited money in Regular State.");
        }

        public override void Withdraw(decimal amount)
        {
            _account.Balance -= amount;
            if (_account.Balance < 0)
                _account.State = new OverdrawnState(_account);
            Console.WriteLine("Withdrew money in Regular State.");
        }

        override public string ToString()
        {
            return "Regular State";
        }
    }
}