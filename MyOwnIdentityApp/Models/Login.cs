using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName {get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public bool RememberLogin {get;set;}
    }
}
