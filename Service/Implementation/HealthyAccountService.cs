using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repository.Interface;
using Service.Interface;
using Domain.Models;
using Service.Util;

namespace Service
{
    public class HealthyAccountService : IHealthyAccountService
    {
        private readonly IHealthyAccountRepository _healthyAccountRepository;
        private readonly IMapper _mapper;

        public HealthyAccountService(IHealthyAccountRepository healthyAccountRepository, IMapper mapper)
        {
            _healthyAccountRepository = healthyAccountRepository;
            _mapper = mapper;
        }

        public async Task<Dto.AccountsPage> GetAccounts(int? pageNumber, int pageSize)
        {
            var results = _healthyAccountRepository.CalculateRecords();
            await UpdateBasedonResult(results.Item1, results.Item2);

            var pages = await PaginatedList<HealthyAccount>
                .CreateAsync(_healthyAccountRepository.GetAll(), pageNumber ?? 1, pageSize);
            var model = _mapper.Map<Dto.AccountsPage>(pages);
            model.Accounts = _mapper.Map<IEnumerable<Dto.HealthyAccount>>(pages);
            return model;
        }

        private async Task UpdateBasedonResult(IEnumerable<Invoice> healthyResult, IEnumerable<Invoice> allResults)
        {
            try
            {
                // think about processing in batch 
                // or using raw SQL to bulk update based on amount of records
                foreach (var account in allResults)
                {
                    var acct = _healthyAccountRepository.GetById(account.CustomerId);
                    if (acct != null)
                    {
                        acct.IsHealthy = healthyResult.Any(x => x.CustomerId == acct.CustomerId);
                        _healthyAccountRepository.Update(acct);
                    }
                }
                await _healthyAccountRepository.Commit();
            }
            catch (Exception)
            {

            }
        }
    }
}
