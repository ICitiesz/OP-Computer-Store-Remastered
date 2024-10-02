import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

import {catchError, Observable} from "rxjs";
import {HttpClientOption} from "./http-client-option";
import {ApiResponse} from "../../model/api/api-response";

@Injectable({
	providedIn: 'root'
})

export class ApiRequestClient {
	constructor(private httpClient: HttpClient) {
	}

	get<T>(reqUrl: string, httpClientOption?: HttpClientOption): Observable<ApiResponse<T>> {
		return this.httpClient.get<ApiResponse<T>>(`${environment.apiServerUrl}/${reqUrl}`, httpClientOption);
	}

	post<T>(reqUrl: string, body?: any, httpClientOption?: HttpClientOption): Observable<ApiResponse<T>> {
		return this.httpClient.post<ApiResponse<T>>(`${environment.apiServerUrl}/${reqUrl}`, body, httpClientOption);
	}
}
