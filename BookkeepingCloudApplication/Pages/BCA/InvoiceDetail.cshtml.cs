using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookkeepingCloudApplication.Data;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class InvoiceDetailModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;
        public InvoiceDetailModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        public InvoiceModel Invoice { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _invoiceManager.GetIsInvoicesNull())
            {
                return NotFound();
            }

            var invoice = _invoiceManager.GetInvoiceByInvoiceNumber((int)id);
            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                Invoice = invoice;
            }
            return Page();
        }


        public decimal GetTotalPrice()
        {
            return (decimal)(Invoice.InvoiceLines?.Sum(i => i.GrossAmount) ?? 0);
        }



    }
}
