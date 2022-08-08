using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IHealthyAccountService
    {
        Task<AccountsPage> GetAccounts(int? pageNumber, int pageSize);

    }
}
