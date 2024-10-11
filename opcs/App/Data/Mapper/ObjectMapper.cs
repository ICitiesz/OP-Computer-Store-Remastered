using AutoMapper;
using opcs.App.Data.Dto.Security.Permission;
using opcs.App.Data.Dto.Security.Role;
using opcs.App.Data.Dto.Supplier;
using opcs.App.Entity.Security;
using opcs.App.Entity.Supplier;

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
            configure.CreateMap<Role, RoleDto>()
                .ForCtorParam(
                    "CreatedDate",
                    option => option.MapFrom(role =>  role.CreatedAt));
            configure.CreateMap<AccessPermission, RolePermissionDto>();
        });
        MapperConfig.CompileMappings();
        Mapper = MapperConfig.CreateMapper();
    }

    public static IMapper GetMapper()
    {
        return Mapper;
    }
}