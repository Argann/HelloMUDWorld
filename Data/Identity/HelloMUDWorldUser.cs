using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HelloMUDWorld.Data.Identity;

// Add profile data for application users by adding properties to the HelloMUDWorldUser class
public class HelloMUDWorldUser : IdentityUser
{
    public string? DisplayName { get; set; }

}

