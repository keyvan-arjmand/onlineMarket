using Application.Dtos;
using Application.Patterns.Products.Commands.InsertProduct;
using Application.Patterns.Products.Commands.UpdateProduct;
using AutoMapper;
using Domain.Entities.Products;

namespace Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateProductCommand, ProductDto>()
            .ReverseMap();
        CreateMap<InsertProductCommand, ProductDto>()
            .ReverseMap();
        CreateMap<InsertProductCommand, Product>()
            .ReverseMap();
    }
}