import { Injectable } from '@angular/core';
import {ApiRequestClient} from "../../../core/api-management/api-request-client";
import {Observable} from "rxjs";
import {PermissionModel} from "../../../model/security/permission.model";
import {ApiResponse} from "../../../model/api/api-response";

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
}
