using AutoMapper;
using scopic_test_server.Data;
using scopic_test_server.DTO;

public class Profiler : Profile
{
    public Profiler()
    {
        CreateMap<Product, ProductReadDto>();
        CreateMap<Bid, BidReadDto>();
        CreateMap<BidCreateDto, Bid>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<User, UserDto>();
    }
}