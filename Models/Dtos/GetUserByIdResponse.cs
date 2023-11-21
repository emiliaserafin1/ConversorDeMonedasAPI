using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Enum;

namespace ConversorDeMonedasBack.Models.Dtos
{
    public class GetUserByIdResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SuscriptionId { get; set; }
        public ICollection<Conversion> Conversions { get; set; }
        public ICollection<Currency> FavouriteCurrencies { get; set; }
        public UserRoleEnum Role { get; set; }
        
        
    }
}
