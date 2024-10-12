namespace Catalog.API.Models
{
    public class Product ( string name, List<string> category, string description, string imagefile, decimal price )
    {
        public Guid Id { get; set; } //= Guid.NewGuid ();
        public string Name { get; set; } = name;
        public List<string> Category { get; set; } = category;
        public string Description { get; set; } = description;
        public string Imagefile { get; set; } = imagefile;
        public decimal Price { get; set; } = price;

    }
}
