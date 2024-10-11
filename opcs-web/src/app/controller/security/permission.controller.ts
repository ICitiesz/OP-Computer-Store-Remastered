import {Injectable} from "@angular/core";
import {PermissionService} from "../../service/security/permission/permission.service";
import {map, Observable} from "rxjs";
import {PermissionModel} from "../../model/security/permission.model";
import {RolePermissionModel} from "../../model/security/role-permission.model";
import {UpdateRolePermissionRequestModel} from "../../model/security/update-role-permission.request.model";
import {ApiResponse} from "../../model/api/api-response";

@Injectable({
	providedIn: 'root'
})
export class PermissionController {
	constructor(private readonly permissionService: PermissionService) {
	}

	getAllPermission(): Observable<Array<PermissionModel>> {
		return this.permissionService.getAllPermission("permission/getAll")
				.pipe(map(response => response.result )
			)
	}

	getRolePermissionByRoleId(roleId: number): Observable<Array<RolePermissionModel>> {
		return this.permissionService.getRolePermissionById("permission/getPermissionByRoleId", roleId).pipe(
			map(response => response.result )
		)
	}

	updateRolePermission(request: UpdateRolePermissionRequestModel): Observable<ApiResponse<any>> {
		return this.permissionService.updateRolePermission("permission/updatePermission", request)
	}
}
