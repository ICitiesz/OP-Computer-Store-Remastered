import {Injectable} from "@angular/core";
import {PermissionService} from "../../service/security/permission/permission.service";
import {map, Observable} from "rxjs";
import {PermissionModel} from "../../model/security/permission.model";

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
}
