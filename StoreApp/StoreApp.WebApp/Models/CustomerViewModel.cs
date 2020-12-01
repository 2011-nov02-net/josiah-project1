using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First name")]
        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage ="Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
    }
}
