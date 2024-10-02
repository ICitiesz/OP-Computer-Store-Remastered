import {RoleService} from "../../service/security/role/role.service";
import {map, Observable} from "rxjs";
import {Injectable, OnInit} from "@angular/core";
import {RoleModel} from "../../model/security/role.model";

@Injectable({
    providedIn: 'root'
})
export class RoleController {
    constructor(
        private readonly roleService: RoleService
    ) {}

    getAllRole(): Observable<Array<RoleModel>> {
        return this.roleService.getAllRole("role/getAll").pipe(
            map((response) => response.result)
        )
    }
}
