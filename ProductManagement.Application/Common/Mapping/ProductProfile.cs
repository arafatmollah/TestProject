using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Common.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.ProductTypeName,
                opt => opt.MapFrom(src =>
                    src.ProductType == null
                        ? string.Empty
                        : src.ProductType.Name))
            .ForMember(
                dest => dest.ExpirationDate,
                opt => opt.MapFrom(src =>
                    src.ProductExpiration == null
                        ? (DateTime?)null
                        : src.ProductExpiration.ExpirationDate))
            .ForMember(
                dest => dest.Tags,
                opt => opt.MapFrom(src =>
                    src.ProductTags.Select(x => x.Name)));
    }
}