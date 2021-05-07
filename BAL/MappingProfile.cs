using AutoMapper;
using BAL.ModelsDTO;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Admin, AdminDTO>();
            CreateMap<AdminDTO, Admin>();

            CreateMap<Status, StatusDTO>();
            CreateMap<StatusDTO, Status>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Order, OrderDTO>()
                .ForMember(
                s => s.StatusName,
                s => s.MapFrom(s => s.Status.Name))
                .ForMember
                (p => p.Price,
                p => p.MapFrom(r => r.Duration * r.Category.Price))
                .ForMember(
                c => c.CategoryName,
                c => c.MapFrom(c => c.Category.Name));
            CreateMap<OrderDTO, Order>();
        }

    }
}
