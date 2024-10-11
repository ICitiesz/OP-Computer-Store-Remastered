namespace opcs.App.Core.Security;

public static class Permission
{
    public enum PermissionEnum
    {
        ManageSupplier,
        ManageUser,
        ManageRole,
        ManageAdminPanel,
        ManagePermission
    }

    private static readonly List<Tuple<PermissionEnum, string>> PermissionValues =
    [
        Tuple.Create(PermissionEnum.ManageSupplier, "MANAGE_SUPPLIER"),
        Tuple.Create(PermissionEnum.ManageUser, "MANAGE_USER"),
        Tuple.Create(PermissionEnum.ManageRole, "MANAGE_ROLE"),
        Tuple.Create(PermissionEnum.ManageAdminPanel, "MANAGE_ADMIN_PANEL"),
        Tuple.Create(PermissionEnum.ManagePermission, "MANAGE_PERMISSION")
    ];

    public static string GetPermission(PermissionEnum permissionEnum)
    {
        return PermissionValues
            .Single(value => value.Item1 == permissionEnum)
            .Item2;
    }

    public static List<string> GetPermissionsInString()
    {
        return PermissionValues.Select(value => value.Item2).ToList();
    }
}