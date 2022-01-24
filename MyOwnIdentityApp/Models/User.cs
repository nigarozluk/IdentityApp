using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public string RememberMe { get; set; }
    }
    public enum RoleType : byte
    {
        [Description("Admin")]
        Admin,
        [Description("User")]
        User
    }
}
