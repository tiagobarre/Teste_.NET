namespace ZOSS.Teste.Back.DTOS
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Value { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductPutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Value { get; set; }
        public int CategoryId { get; set; }
    }
}
