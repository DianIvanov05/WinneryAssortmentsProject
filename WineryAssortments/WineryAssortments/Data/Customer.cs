using Microsoft.AspNetCore.Identity;

namespace WineryAssortments.Data
{
    public class Customer:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Order> Orders { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}