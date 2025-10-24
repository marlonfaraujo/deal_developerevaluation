namespace Deal.DeveloperEvaluation.WebApi.Entities
{
    public readonly struct Money
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Preço com valor negativo.", nameof(value));

            Value = value;
        }

        public Money Add(Money other) => new(Value + other.Value);
        public Money Subtract(Money other) => new(Value - other.Value);
        public bool IsGreaterThan(Money other) => Value > other.Value;

        public override string ToString() => Value.ToString("C2");
    }
}
