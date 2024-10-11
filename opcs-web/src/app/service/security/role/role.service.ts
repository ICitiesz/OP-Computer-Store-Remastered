import { Injectable } from '@angular/core';
import {ApiRequestClient} from "../../../core/api-management/api-request-client";
import {Observable} from "rxjs";
import {ApiResponse} from "../../../model/api/api-response";
import {RoleModel} from "../../../model/security/role.model";
import {PaginationRequestModel} from "../../../model/pagination/pagination.request.model";
import {QueryRoleSort} from "../../../model/pagination/sort/query-role.sort";
import {PaginationResponseModel} from "../../../model/pagination/pagination.response.model";
import {PermissionModel} from "../../../model/security/permission.model";
import {RolePermissionModel} from "../../../model/security/role-permission.model";

@Injectable({
  providedIn: 'root'
})
export class RoleService {
	constructor(private apiRequestClient: ApiRequestClient) { }

	queryRole(url: string, pageReqModel: PaginationRequestModel<any, QueryRoleSort>): Observable<ApiResponse<PaginationResponseModel<RoleModel>>> {
		return this.apiRequestClient.post(
			url,
			pageReqModel,
			{
				withCredentials: true
			}
		)
	}

	getAllRole(url: string): Observable<ApiResponse<Array<RoleModel>>> {
		return this.apiRequestClient.get(
			url,
			{
				withCredentials: true
			}
		)
	}
}
