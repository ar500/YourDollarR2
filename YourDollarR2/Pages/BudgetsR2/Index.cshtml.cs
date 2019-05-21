﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourDollarR2.Core;
using YourDollarR2.DataAccess;

namespace YourDollarR2.Pages.BudgetsR2
{
    public class IndexModel : PageModel
    {
        private readonly YourDollarR2.DataAccess.YourDollarContext _context;

        public IndexModel(YourDollarR2.DataAccess.YourDollarContext context)
        {
            _context = context;
        }

        public IList<Budget> Budget { get;set; }

        public async Task OnGetAsync()
        {
            Budget = await _context.Budgets.ToListAsync();
        }
    }
}
