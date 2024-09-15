namespace DesignPatternDemoComponents.State
{
    // State base class
    public abstract class BankAccountState(BankAccount account)
    {
        protected BankAccount _account = account;

        public abstract void Deposit(decimal amount);
        public abstract void Withdraw(decimal amount);
    }
}