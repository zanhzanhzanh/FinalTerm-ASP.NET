using AutoMapper;
using FinalTerm.Dto;
using FinalTerm.Models;

namespace FinalTerm.Helper {
    public class MappingProfiles : Profile {
        public MappingProfiles() {
            // For Nullable Value Type to Default Value Type 
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

            // For Dto to Models
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateOrderDto, Order>()
                .ForMember(x => x.Customer, opt => opt.Ignore())
                .ForMember(x => x.OrderItems, opt => opt.Ignore());
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
