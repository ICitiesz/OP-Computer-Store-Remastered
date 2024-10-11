import {RoleService} from "../../service/security/role/role.service";
import {map, Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {RoleModel} from "../../model/security/role.model";
import {PaginationRequestModel} from "../../model/pagination/pagination.request.model";
import {QueryRoleSort} from "../../model/pagination/sort/query-role.sort";
import {PaginationResponseModel} from "../../model/pagination/pagination.response.model";

@Injectable({
    providedIn: 'root'
})
export class RoleController {
    constructor(
        private readonly roleService: RoleService
    ) {}

	queryPage(pageModel: PaginationRequestModel<any, QueryRoleSort>): Observable<PaginationResponseModel<RoleModel>> {
		return this.roleService.queryRole("role/queryPage", pageModel).pipe(
			map(response => response.result )
		)
	}

    getAllRole(): Observable<Array<RoleModel>> {
        return this.roleService.getAllRole("role/getAll").pipe(
            map((response) => response.result)
        )
    }
}
