using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookkeepingCloudApplication.Data;
using BookkeepingCloudApplication.Models;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class IndexModel : PageModel
    {
        private readonly BookkeepingCloudApplication.Data.ApplicationDbContext _context;

        public IndexModel(BookkeepingCloudApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Invoices != null)
            {
                Invoice = await _context.Invoices.ToListAsync();
            }
        }
    }
}
