using Domain.Data;
using Domain.Models;
using Repository.Interface;

namespace Repository
{
    public class HealthyAccountRepository : BaseRepository<HealthyAccount>, IHealthyAccountRepository
    {
        public HealthyAccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public (IEnumerable<Invoice>, IEnumerable<Invoice>) CalculateRecords()
        {
            var invoiceList = _context.Invoices
                .Where(x => x.IssuedDate > DateTime.Now.AddMonths(-6)).ToList();
            var noHealthy = invoiceList.Where(x => x.OutstandingAmount > 0
                            && x.IssuedDate < DateTime.Now.AddDays(-90)).ToList();

            var query = from invoice in invoiceList
                        group invoice by invoice.CustomerId into invoiceByCustomerId
                        let customerInvoiceSum = new
                        {
                            Sum = invoiceByCustomerId.Sum(i => i.OriginalAmount),
                            Customer = invoiceByCustomerId
                                .FirstOrDefault(c => c.CustomerId == invoiceByCustomerId.Key)
                        }
                        where customerInvoiceSum.Sum > 100000
                        select customerInvoiceSum.Customer;
            var helathyList = query.ToList();

            return (helathyList.Except(noHealthy), invoiceList);
        }

    }
}
