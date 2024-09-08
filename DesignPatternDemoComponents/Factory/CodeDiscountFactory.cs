namespace DesignPatternDemoComponents.Factory
{
    // Concrete Creator 2
    public class CodeDiscountFactory(string code) : DiscountFactory
    {
        public override DiscountService CreateDiscountService()
        {
            return new CodeDiscountService(code);
        }
    }
}