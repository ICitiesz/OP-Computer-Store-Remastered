import { Injectable } from '@angular/core';
import {ApiRequestClient} from "../../../core/api-management/api-request-client";
import {Observable} from "rxjs";
import {PermissionModel} from "../../../model/security/permission.model";
import {ApiResponse} from "../../../model/api/api-response";
import {RolePermissionModel} from "../../../model/security/role-permission.model";
import {UpdateRolePermissionRequestModel} from "../../../model/security/update-role-permission.request.model";

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

	constructor(private apiRequestClient: ApiRequestClient) {
	}

	getAllPermission(url: string): Observable<ApiResponse<Array<PermissionModel>>> {
	  return this.apiRequestClient.get(url, {
		  withCredentials: true
	  })
	}

	getRolePermissionById(url: string, roleId: number): Observable<ApiResponse<Array<RolePermissionModel>>> {
		return this.apiRequestClient.get(
			url,
			{
				params: {
					roleId: roleId
				},
				withCredentials: true
			}
		)
	}

	updateRolePermission(url: string, request: UpdateRolePermissionRequestModel): Observable<ApiResponse<any>> {
		return this.apiRequestClient.post(
			url,
			request,
			{
				withCredentials: true
			}
		)
	}
}
