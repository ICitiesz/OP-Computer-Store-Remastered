import {RolePermissionModel} from "./role-permission.model";

export interface UpdateRolePermissionRequestModel {
	roleId: number
	grantedPermissions: Array<RolePermissionModel>
	revokedPermissions: Array<RolePermissionModel>
}
