using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;

namespace GroupTherapyWebAppFinal.Views.UserModels
{
    public class IndexModel : PageModel
    {
        private readonly GroupTherapyWebAppFinal.Data.ApplicationDbContext _context;

        public IndexModel(GroupTherapyWebAppFinal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UserModel> UserModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserModels != null)
            {
                UserModel = await _context.UserModels.ToListAsync();
            }
        }
    }
}
