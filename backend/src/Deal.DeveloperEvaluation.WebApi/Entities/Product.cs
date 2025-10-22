namespace Deal.DeveloperEvaluation.WebApi.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public ProductName Name { get; private set; }
        public Sku Code { get; private set; }
        public Money Price { get; private set; }

        private Product() { }

        public Product(string name, string code, decimal price)
        {
            Name = new ProductName(name);
            Code = new Sku(code);
            Price = new Money(price);
        }

        public void ChangePrice(decimal newPrice)
        {
            var newPriceMoney = new Money(newPrice);
            if (!newPriceMoney.IsGreaterThan(new Money(0)))
                throw new InvalidOperationException("New price must be greater than zero.");

            Price = newPriceMoney;
        }
    }
}
