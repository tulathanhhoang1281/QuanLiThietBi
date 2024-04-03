using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiThietBi.Domain.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter your username!")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string? Password { get; set; }
        public bool RememeberMe { get; set; }
    }
}
