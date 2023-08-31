using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class DetailsModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;

        public DetailsModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

      public Invoice Invoice { get; set; } = default!; 

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
            else 
            {
                Invoice = invoice;
            }
            return Page();
        }
    }
}
