namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public static class ProductFilterStrategyFactory
    {
        public static IProductFilterStrategy Create(Type propertyType)
        {
            var strategies = new List<IProductFilterStrategy>
            {
                new ProductGuidFilterStrategy(),
                new ProductNameFilterStrategy(),
                new ProductSkuFilterStrategy(),
                new ProductDefaultStringFilterStrategy()
            };
                        
            return strategies.FirstOrDefault(s => s.CanHandle(propertyType)) 
                ?? new ProductDefaultStringFilterStrategy();
        }
    }
}
