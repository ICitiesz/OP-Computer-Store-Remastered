import { Injectable } from '@angular/core';
import {ApiRequestClient} from "../../../core/api-management/api-request-client";
import {Observable} from "rxjs";
import {ApiResponse} from "../../../model/api/api-response";
import {RoleModel} from "../../../model/security/role.model";

@Injectable({
  providedIn: 'root'
})
export class RoleService {
	constructor(private apiRequestClient: ApiRequestClient) { }

	getAllRole(url: string): Observable<ApiResponse<Array<RoleModel>>> {
		return this.apiRequestClient.get(
			url,
			{
				withCredentials: true
			}
		)
	}
}
