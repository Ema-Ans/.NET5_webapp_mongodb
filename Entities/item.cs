namespace Catalog.Entities
{
    // use a  record instesd of class
    public record Item
    {
        // initialize properties
        public Guid Id {get; init; }
        public string Name {get; init; }
        public decimal Price {get; init; }
        public DateTimeOffset CreatedDate{get; init; }



    }
}