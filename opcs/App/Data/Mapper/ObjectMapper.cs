using AutoMapper;
using opcs.App.Data.Dto;
using opcs.App.Data.Dto.General;
using opcs.App.Model.Supplier;
using opcs.App.Model.User;

namespace opcs.App.Data.Mapper;

public static class ObjectMapper
{
    private static readonly MapperConfiguration MapperConfig;
    private static readonly IMapper Mapper;

    static ObjectMapper()
    {
        MapperConfig = new MapperConfiguration(configure =>
        {
            configure.CreateMap<Supplier, SupplierDto>();
            configure.CreateMap<Role, RoleDto>();
        });
        MapperConfig.CompileMappings();
        Mapper = MapperConfig.CreateMapper();
    }

    public static IMapper GetMapper()
    {
        return Mapper;
    }
}