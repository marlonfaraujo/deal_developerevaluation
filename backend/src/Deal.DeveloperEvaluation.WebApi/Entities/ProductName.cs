namespace Deal.DeveloperEvaluation.WebApi.Entities
{
    public readonly struct ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nome do produto não pode ser vazio.", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("Nome do produto com mais de 100 caracteres.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
