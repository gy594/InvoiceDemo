using Domain.Data;
using Domain.Models;
using Repository.Interface;

namespace Repository
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
