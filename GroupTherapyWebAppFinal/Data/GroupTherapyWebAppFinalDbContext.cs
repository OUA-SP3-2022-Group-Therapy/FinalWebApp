using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroupTherapyWebAppFinal.Data
{
    public class GroupTherapyWebAppFinalDbContext : IdentityDbContext
    {
        public GroupTherapyWebAppFinalDbContext(DbContextOptions<GroupTherapyWebAppFinalDbContext> options)
            : base(options) 
        { 
        }

    }
}
