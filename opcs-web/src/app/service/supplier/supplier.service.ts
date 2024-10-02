import {Injectable} from '@angular/core';
import {catchError, first, Observable, of, Subject, takeUntil, tap} from "rxjs";
import {ApiResponse} from "../../model/api/api-response";
import {ApiRequestClient} from "../../core/api-management/api-request-client";
import {HttpHeaders} from "@angular/common/http";
import {SupplierModel} from "../../model/supplier.model";

@Injectable({
	providedIn: 'root'
})
export class SupplierService {
	constructor(private apiRequestClient: ApiRequestClient) {
	}

	getSupplierById(url: string, id: number): Observable<ApiResponse<SupplierModel>> {
		return this.apiRequestClient.get(
			url,
			{
				params: {
					id: id
				},
				withCredentials: true
			}
		)
	}
}
