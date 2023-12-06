using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_lullabay.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "user name is required")]
        public string UserName { get; set; }
       
        [Required(ErrorMessage = "passsword name is required")]
        public string password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
