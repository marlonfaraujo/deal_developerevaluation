namespace Deal.DeveloperEvaluation.WebApi.Entities
{
    public readonly struct Sku
    {
        public string Value { get; }

        public Sku(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Código de produto não pode ser vazio.", nameof(value));

            Value = value.Trim().ToUpperInvariant();
        }

        public override string ToString() => Value;
    }
}
