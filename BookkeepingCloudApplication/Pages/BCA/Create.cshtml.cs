﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookkeepingCloudApplication.Data;
using BookkeepingCloudApplication.Models;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class CreateModel : PageModel
    {
        private readonly BookkeepingCloudApplication.Data.ApplicationDbContext _context;

        public CreateModel(BookkeepingCloudApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Fetch max Invoice Number from the database and increment by 1
            var maxInvoiceNumber = _context.Invoices.Max(i => i.InvoiceNumber);
            var newInvoiceNumber = maxInvoiceNumber + 1;

            // Set DateEntered to today's date and set new invoice number.
            Invoice = new Invoice
            {
                DateEntered = DateTime.Now,
                InvoiceNumber = newInvoiceNumber
            };

            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Invoices == null || Invoice == null)
            {
                return Page();
            }

            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
