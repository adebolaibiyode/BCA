using BookkeepingCloudApplication.Data;
using BookkeepingCloudApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookkeepingCloudApplication.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Creates a new instance of the Invoice Manager class. The Invoice manager is responsible for business logic and database access.
        /// </summary>
        /// <param name="context"></param>
        public InvoiceManager (ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public bool CreateInvoice(Invoice invoice)
        {
            // TODO - additional model validation.

            // Fetch max Invoice Number from the database and increment by 1
            int newInvoiceNumber;
            try
            {
                int maxInvoiceNumber = GetMaximumInvoiceNumber();
                newInvoiceNumber = maxInvoiceNumber + 1;
            }
            catch (Exception ex)
            {
                newInvoiceNumber = 1;
            }
            invoice.Id = newInvoiceNumber;

            _context.Invoices.Add(invoice);
            return SaveChanges();
        }

        /// <inheritdoc/>
        public bool DeleteInvoiceById(int id)
        {
            var dbInvoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
            if (dbInvoice != null)
            {
                _context.Invoices.Remove(dbInvoice);
            }
            return SaveChanges();
        }

        /// <inheritdoc/>
        public Invoice GetInvoiceById(int id)
        {
            return _context.Invoices.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <inheritdoc/>
        public IList<Invoice> GetInvoices(uint count, uint page)
        {
            return _context.Invoices.Skip((int)(0 * count)).Take((int)count).ToList();
        }

        /// <inheritdoc/>
        public bool GetIsInvoicesNull()
        {
            return _context.Invoices == null ? true : false;
        }

        /// <inheritdoc/>
        public int GetMaximumInvoiceNumber()
        {
            return _context.Invoices.Max(i => i.InvoiceNumber);
        }

        /// <inheritdoc/>
        public bool UpdateInvoice(Invoice invoice)
        {
            _context.Attach(invoice).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// Saves changes in the database and returns true if more than one row was updated.
        /// </summary>
        /// <returns>Indicates if the save was successful</returns>
        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
