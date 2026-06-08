using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
    }
}