using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookkeepingCloudApplication.Data;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class InvoiceLineModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for initializing the InvoiceLineModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor instance.</param>
        public InvoiceLineModel(IInvoiceManager invoiceManager, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _invoiceManager = invoiceManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

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

            var invoice = _invoiceManager.GetInvoiceById((int)id);
            if (invoice == null)
            {
                return NotFound();
            }

            string loggedUser = "";
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            // If the user is logged in, user will be non-null.
            if (user != null)
            {
                loggedUser = user.Email;             
            }
            Invoice = new Invoice
            {
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceType =invoice.InvoiceType,
                AccountType=invoice.AccountType,
                AccountName=invoice.AccountName,
                AccountReference = invoice.AccountReference,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceStatus = invoice.InvoiceStatus,
                InvoiceControlAccount=invoice.InvoiceControlAccount,
                AccountEmail=invoice.AccountEmail,
                CurrencyCode=invoice.CurrencyCode,
                DateEntered=DateTime.Now,
                EnteredBy= loggedUser
            };

           

            return Page();
        }


        /// <summary>
        /// Handles HTTP POST requests asynchronously for adding an invoice line to the system.
        /// </summary>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _invoiceManager.GetIsInvoicesNull() || Invoice == null)
            {
                return Page();
            }

            _invoiceManager.AddInvoiceLine(Invoice);

            return RedirectToPage("/BCA/Details", new { id = Invoice.Id });
        }
    }
}
