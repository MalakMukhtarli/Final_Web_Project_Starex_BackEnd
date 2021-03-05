using Entity.Entities;
using Entity.Entities.Branches;
using Entity.Entities.Cities;
using Entity.Entities.Contacts;
using Entity.Entities.Tariffs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starex.Identity
{
    public class MyIdentityDbContext : IdentityDbContext<AppUser>
    {
        public MyIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
