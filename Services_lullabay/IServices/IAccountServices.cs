using Microsoft.AspNetCore.Mvc;
using Services_lullabay.DTO;
using Services_lullabay.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_lullabay.IServices
{
    public interface IAccountServices
    {
        public  Task<string> Register([FromBody] RegisterModel model);
        public  Task<IActionResult> login([FromBody] loginModel model);
    }
}
