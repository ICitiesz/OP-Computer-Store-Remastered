using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
}