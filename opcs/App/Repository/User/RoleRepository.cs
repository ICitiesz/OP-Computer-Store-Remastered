using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Data.Dto.Pagination;
using opcs.App.Data.Dto.Pagination.Search;
using opcs.App.Data.Dto.Pagination.Sort;
using opcs.App.Database;
using opcs.App.Entity.Security;
using opcs.App.Repository.User.Interface;

namespace opcs.App.Repository.User;

public class RoleRepository(AppDbContext dbContext) : IRoleRepository
{
    public async Task<List<Role>> GetAllRolesAsync()
    {
        return await dbContext.Role.AsNoTracking().ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(long roleId)
    {
        return await dbContext.Role.FindAsync(roleId);
    }

    public bool HasRoleByRoleId(long roleId)
    {
        return dbContext.Role.Exists(role => role.RoleId == roleId);
    }

    public async Task<bool> HasUserRole(string userId)
    {
        FormattableString sqlQuery = $"""
                                      SELECT *
                                      FROM t_role AS tRole
                                      WHERE EXISTS(
                                          SELECT 1 FROM t_user AS tUser
                                          WHERE tUser.user_id = {userId}
                                          AND tUser.role_id = tRole.role_id)
                                      """;

        return await dbContext.Database.ExecuteSqlInterpolatedAsync(sqlQuery) > 0;
    }

    public async Task<Role?> GetUserRole(string userId)
    {
        FormattableString sqlQuery = $"""
                                      SELECT *
                                      FROM t_role AS tRole
                                      WHERE EXISTS(
                                          SELECT 1 FROM t_user AS tUser
                                          WHERE tUser.user_id = {userId}
                                          AND tUser.role_id = tRole.role_id)
                                      """;

        var roles = await dbContext.Database.SqlQuery<Role>(sqlQuery).AsNoTracking().ToListAsync();


        return !roles.IsNullOrEmpty() ? roles.First() : null;
    }

    public async Task<(List<Role>, int)> QueryRole(PaginationRequestDto<QueryRoleSearch, QueryRoleSort> requestDto)
    {
        var search = requestDto.Search;
        var sorts = requestDto.Sort;
        var sqlQuery = dbContext.Role.AsNoTracking();

        if (!search.RoleName.IsNullOrEmpty())
        {
            sqlQuery = sqlQuery
                .Where(role =>
                    EF.Functions.Like(role.RoleName, $"%{requestDto.Search.RoleName}%"));
        }

        sqlQuery = sorts.RoleNameDesc switch
        {
            false => sqlQuery
                .OrderBy(role => role.RoleName.Length)
                .ThenBy(role => role.RoleId),

            true => sqlQuery
                .OrderByDescending(role => role.RoleName.Length)
                .ThenByDescending(role => role.RoleName)
        };

        var totalRoleCount = await sqlQuery.CountAsync();

        var roles = await sqlQuery
            .Skip((requestDto.CurrentPage - 1) * requestDto.TotalItemsPerPage)
            .Take(requestDto.TotalItemsPerPage)
            .ToListAsync();

        return (roles, totalRoleCount);
    }
}