using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GroupTherapyWebAppFinal.Areas.Identity.Data;

// Add profile data for application users by adding properties to the GroupTherapyWebAppFinalUser class
public class GroupTherapyWebAppFinalUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }
    [PersonalData]
    public string LastName { get; set; }
}

