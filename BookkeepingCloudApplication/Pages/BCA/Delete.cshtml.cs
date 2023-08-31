using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class DeleteModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;

        public DeleteModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _invoiceManager.GetIsInvoicesNull())
            {
                return NotFound();
            }

            _invoiceManager.DeleteInvoiceById((int)id);

            return RedirectToPage("./Index");
        }
    }
}
