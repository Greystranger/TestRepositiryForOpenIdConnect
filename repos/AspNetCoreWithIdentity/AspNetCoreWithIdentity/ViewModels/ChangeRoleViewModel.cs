using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreWithIdentity.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public IEnumerable<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();

        public IEnumerable<string> UserRoles { get; set; } = new List<string>();
    }
}
