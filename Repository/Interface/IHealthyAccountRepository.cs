using Domain.Models;

namespace Repository.Interface
{
    public interface IHealthyAccountRepository : IBaseRepository<HealthyAccount>
    {
        (IEnumerable<Invoice>, IEnumerable<Invoice>) CalculateRecords();
    }
}
