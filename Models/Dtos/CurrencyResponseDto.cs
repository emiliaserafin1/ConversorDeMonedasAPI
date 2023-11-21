namespace ConversorDeMonedasBack.Models.Dtos
{
    public class CurrencyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Value { get; set; }
    }
}
