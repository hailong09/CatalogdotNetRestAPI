using System;
namespace Catalog.Entities
{
    public record Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Decimal Price { get; init; }
        public DateTimeOffset CreateDate { get; init; }
    }
}