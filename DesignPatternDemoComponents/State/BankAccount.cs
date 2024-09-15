namespace DesignPatternDemoComponents.State
{

    // Context class
    public class BankAccount
    {
        public decimal Balance { get; set; }
        public BankAccountState State { get; set; }

        public BankAccount()
        {
            Balance = 200;
            State = new RegularState(this);
        }

        public void Deposit(decimal amount) => State.Deposit(amount);
        public void Withdraw(decimal amount) => State.Withdraw(amount);
    }
}