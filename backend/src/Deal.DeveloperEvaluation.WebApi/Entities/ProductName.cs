namespace Deal.DeveloperEvaluation.WebApi.Entities
{
    public readonly struct ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty.", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("Product name is too long.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
