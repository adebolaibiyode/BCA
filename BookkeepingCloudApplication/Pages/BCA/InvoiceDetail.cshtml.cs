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

        /// <summary>
        /// Constructor for initializing the InvoiceDetailModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        public InvoiceDetailModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        public InvoiceModel Invoice { get; set; } = default!;

        /// <summary>
        /// Handles HTTP GET requests asynchronously for retrieving invoice data.
        /// </summary>
        /// <param name="id">Optional ID parameter for specifying the invoice to retrieve.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
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


        /// <summary>
        /// Calculates and returns the total price of the invoice.
        /// </summary>
        /// <returns>The total price as a decimal.</returns>
        public decimal GetTotalPrice()
        {
            return (decimal)(Invoice.InvoiceLines?.Sum(i => i.GrossAmount) ?? 0);
        }
    }
}
