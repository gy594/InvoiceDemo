using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;
using Service.Util;

namespace Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.Invoice>> GetInvoicesByAccount(int id)
        {
            IEnumerable<Dto.Invoice> model = new List<Dto.Invoice>();
            var invoiceList = await _invoiceRepository.Find(x => x.CustomerId == id)
                .OrderByDescending(x => x.IssuedDate).Take(10).ToListAsync();
            if (invoiceList.Any())
            {
                model = _mapper.Map<IEnumerable<Dto.Invoice>>(invoiceList);
            }
            return model;
        }

    }
}
