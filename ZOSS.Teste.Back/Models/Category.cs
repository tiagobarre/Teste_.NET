namespace ZOSS.Teste.Back.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Um para muitos: uma categoria tem muitos produtos
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
