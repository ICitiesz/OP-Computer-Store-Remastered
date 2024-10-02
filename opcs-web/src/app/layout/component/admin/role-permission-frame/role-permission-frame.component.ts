import {Component, OnInit} from '@angular/core';
import {IconProvider} from "../../../../core/icon-provider";
import {MatIconModule} from "@angular/material/icon";
import {MatOption, MatRippleModule} from "@angular/material/core";
import {MatSelect} from "@angular/material/select";
import {NgbDropdown, NgbDropdownItem, NgbDropdownMenu, NgbDropdownToggle} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule} from "@angular/forms";
import {MatDialog, MatDialogModule} from "@angular/material/dialog";
import {PermissionDialogComponent} from "../permission-dialog/permission-dialog.component";
import {RoleController} from "../../../../controller/security/role.controller";
import {ApiResponse} from "../../../../model/api/api-response";
import {AsyncPipe, JsonPipe, NgForOf, NgIf} from "@angular/common";
import {RoleModel} from "../../../../model/security/role.model";
import {PermissionController} from "../../../../controller/security/permission.controller";
import {PermissionModel} from "../../../../model/security/permission.model";

@Component({
	selector: 'app-role-permission-frame',
	standalone: true,
	imports: [
		MatIconModule,
		MatRippleModule,
		MatSelect,
		MatOption,
		NgbDropdown,
		NgbDropdownMenu,
		NgbDropdownToggle,
		NgbDropdownItem,
		FormsModule,
		MatDialogModule,
		NgIf,
		JsonPipe,
		AsyncPipe,
		NgForOf
	],
	templateUrl: './role-permission-frame.component.html',
	styleUrl: './role-permission-frame.component.scss'
})
export class RolePermissionFrameComponent implements OnInit {
	protected roles!: Array<RoleModel>;
	protected permissions!: Array<PermissionModel>;

	protected readonly IconProvider = IconProvider;

	constructor(
		private matDialog: MatDialog,
		private roleController: RoleController,
		private permissionController: PermissionController
	) {}

	protected openRolePermissionDialog(data: any): void {
		this.matDialog.open(PermissionDialogComponent, {
			height: '600px',
			width: '800px',
			disableClose: true,
			data: data
		})
	}

	ngOnInit(): void {
	    this.roleController.getAllRole().subscribe(response => this.roles = response)
		this.permissionController.getAllPermission().subscribe(response => {
			this.permissions = response
		})
	}
}
