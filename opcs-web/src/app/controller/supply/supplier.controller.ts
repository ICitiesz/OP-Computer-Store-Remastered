import {Injectable} from "@angular/core";
import {SupplierService} from "../../service/supplier/supplier.service";
import {map, Observable} from "rxjs";
import {SupplierModel} from "../../model/supplier.model";

@Injectable(
	{providedIn: 'root'}
)
export class SupplierController {
	constructor(private readonly supplierService: SupplierService) {
	}

	getSupplierById(id: number): Observable<SupplierModel> {
		return this.supplierService.getSupplierById("supplier/", id)
			.pipe(map((response) => response.result))
	}
}
