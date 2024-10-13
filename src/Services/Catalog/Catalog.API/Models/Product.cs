namespace Catalog.API.Models
{
    public class Product
    {
        public Product ( )
        {

        }
        public Product ( string name, List<string> category, string description, string imagefile, decimal price )
        {
            Name = name;
            Category = category;
            Description = description;
            Imagefile = imagefile;
            Price = price;
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = [];
        public string Description { get; set; } = default!;
        public string Imagefile { get; set; } = default!;
        public decimal Price { get; set; }

    }
}
