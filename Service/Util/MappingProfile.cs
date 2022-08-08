using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;


namespace Service.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HealthyAccount, Dto.HealthyAccount>();
            CreateMap<Invoice, Dto.Invoice>();
            CreateMap<PaginatedList<HealthyAccount>, Dto.AccountsPage>();
            CreateMap<PaginatedList<Invoice>, Dto.InvoicesPageViewModel>();
        }
    }
}
