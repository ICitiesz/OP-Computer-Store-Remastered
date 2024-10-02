import { Injectable } from '@angular/core';
import {ApiRequestClient} from "../../core/api-management/api-request-client";
import {map, Observable} from "rxjs";
import {ApiResponse} from "../../model/api/api-response";
import {LoginModel} from "../../model/auth/login-model";
import {AuthModel} from "../../model/auth/auth-model";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
	constructor(private apiRequestClient: ApiRequestClient) {}

	userLogin(url: string, loginModel: LoginModel): Observable<ApiResponse<AuthModel>> {
		return this.apiRequestClient.post<AuthModel>(url, loginModel, {
				withCredentials: true
			}
		)
	}

	userLogout(url: string): Observable<ApiResponse<any>> {
		return this.apiRequestClient.post<any>(url, null, {
			withCredentials: true
		})
	}

	refreshAuth(url: string): Observable<ApiResponse<AuthModel>> {
		return this.apiRequestClient.post<AuthModel>(url, null, {
			withCredentials: true
		});
	}
}
