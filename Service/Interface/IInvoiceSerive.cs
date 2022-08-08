using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;

namespace Service.Interface
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Dto.Invoice>> GetInvoicesByAccount(int id);

    }
}
