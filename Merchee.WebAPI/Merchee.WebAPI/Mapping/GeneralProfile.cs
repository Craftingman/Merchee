﻿using AutoMapper;
using Merchee.Domain.Entities;

namespace Merchee.WebAPI.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Company, Company>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Shelves, opt => opt.Ignore())
                .ForMember(c => c.Products, opt => opt.Ignore())
                .ForMember(c => c.Users, opt => opt.Ignore());

            CreateMap<Product, Product>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CompanyId, opt => opt.Ignore());

            CreateMap<Shelf, Shelf>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CompanyId, opt => opt.Ignore());

            CreateMap<ShelfProduct, ShelfProduct>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CompanyId, opt => opt.Ignore())
                .ForMember(c => c.Items, opt => opt.Ignore())
                .ForMember(c => c.ShelfID, opt => opt.Ignore())
                .ForMember(c => c.Shelf, opt => opt.Ignore())
                .ForMember(c => c.Product, opt => opt.Ignore());
        }
    }
}
