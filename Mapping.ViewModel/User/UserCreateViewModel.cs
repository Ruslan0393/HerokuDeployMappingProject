using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mapping.ViewModel.User
{
    public class UserCreateViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public int Age { get; set; }

        public IFormFile Image { get; set; }
    }
}
