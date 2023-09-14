using BookkeepingCloudApplication.Models;

namespace BookkeepingCloudApplication.Managers
{
    public interface IInvoiceManager
    {
        /// <summary>
        /// Retrieves the maximum invoice number in the system
        /// </summary>
        /// <returns>The invoice number</returns>
        /// <exception cref="NullReferenceException">Throws if there is no invoice in the system</exception>
        int GetMaximumInvoiceNumber();

        /// <summary>
        /// Checks if the Invoices database table is null.
        /// </summary>
        /// <returns>boolean indicating the is the table is null</returns>
        bool GetIsInvoicesNull();

        /// <summary>
        /// Creates a new invoice.
        /// </summary>
        /// <param name="invoice">The invoice to be created</param>
        /// <returns>Indicates if the invoice was created successfully.</returns>
        bool CreateInvoice(Invoice invoice);

        /// <summary>
        /// Retrieves an invoice specified by an ID
        /// </summary>
        /// <param name="id">Invoice to retrieve</param>
        /// <returns>The retrieved invoice.</returns>
        Invoice GetInvoiceById(int id);

        /// <summary>
        /// Retrieves an invoice specified by invoicenumber
        /// </summary>
        /// <param name="invoicenumber">Invoice to retrieve</param>
        /// <returns>The retrieved invoice.</returns>
        InvoiceModel GetInvoiceByInvoiceNumber(int invoicenumber);

        /// <summary>
        /// Deletes the specified Invoice by an ID
        /// </summary>
        /// <param name="id">Invoice to delete</param>
        /// <returns>Indicates if the invoice was deleted successfully</returns>
        bool DeleteInvoiceById(int id);
        
        /// <summary>
        /// Attaches and saves an invoice to the database.
        /// </summary>
        /// <param name="invoice">The invoice to update</param>
        /// <returns>Indicates if the invoice has been updated successfully</returns>
        bool UpdateInvoice(Invoice invoice);

        /// <summary>
        /// Adds new invoice line to existing invoice andnd saves to the database.
        /// </summary>
        /// <param name="invoice">The invoice to update</param>
        /// <returns>Indicates if the invoice has been updated successfully</returns>
        bool AddInvoiceLine(Invoice invoice);

        /// <summary>
        /// Retrieves all invoices.
        /// </summary>
        /// <returns>List containing invoices on the specified page.</returns>
        IList<Invoice> GetInvoices(uint count, uint page,string username="");
    }
}
