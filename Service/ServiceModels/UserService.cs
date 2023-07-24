using Domain.Models;

namespace Service.ServiceModels
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
