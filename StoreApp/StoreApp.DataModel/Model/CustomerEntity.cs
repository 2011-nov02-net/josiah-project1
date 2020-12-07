using System.Collections.Generic;

namespace StoreApp.Data
{
    public class CustomerEntity
    {
        public CustomerEntity()
        {
            Orders = new List<OrderEntity>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<OrderEntity> Orders { get; set; }
    }
}
