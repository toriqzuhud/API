using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_WebApplication.ViewModels
{
    public class Register
    {
        public string FullName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int RoleId { get; set; } = 2;
    }
}
