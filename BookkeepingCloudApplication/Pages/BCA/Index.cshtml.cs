using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class IndexModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;
        
    /// <summary>
    /// Constructor for initializing the IndexModel class with dependencies.
    /// </summary>
    /// <param name="invoiceManager">The invoice manager instance.</param>
        public IndexModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        /// <summary>
        /// Handles the HTTP GET request for the index page asynchronously.
        /// Retrieves invoices if they are not null in the invoice manager.
        /// </summary>
        public async Task OnGetAsync()
        {
            if (!_invoiceManager.GetIsInvoicesNull())
            {
                Invoice = _invoiceManager.GetInvoices(int.MaxValue, 0);
            }
        }
    }
}
