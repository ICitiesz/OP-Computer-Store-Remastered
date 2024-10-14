import {Component, Inject, OnInit, ViewEncapsulation} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {IconProvider} from "../../../../core/icon-provider";
import {MatIcon} from "@angular/material/icon";
import {MatRipple} from "@angular/material/core";
import {NgForOf, NgIf, NgStyle} from "@angular/common";
import {MatSlideToggle, MatSlideToggleChange} from "@angular/material/slide-toggle";
import {PermissionModel} from "../../../../model/security/permission.model";
import {RolePermissionModel} from "../../../../model/security/role-permission.model";
import {StringUtils} from "../../../../core/utils/string.utils";
import {PermissionController} from "../../../../controller/security/permission.controller";
import {UpdateRolePermissionRequestModel} from "../../../../model/security/update-role-permission.request.model";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
	selector: 'app-permission-dialog',
	standalone: true,
	imports: [MatDialogModule, MatIcon, MatRipple, NgStyle, MatSlideToggle, NgForOf, NgIf],
	encapsulation: ViewEncapsulation.None,
	templateUrl: './permission-dialog.component.html',
	styleUrl: './permission-dialog.component.scss'
})
export class PermissionDialogComponent implements OnInit {
	protected roleName: string
	protected permissions: Array<PermissionModel>
	protected roleId: number
	protected readonly IconProvider = IconProvider;

	protected grantedPermissions: Array<RolePermissionModel> = []
	protected revokedPermissions: Array<RolePermissionModel> = []

	constructor(
		private matDialog: MatDialogRef<PermissionDialogComponent>,
		@Inject(MAT_DIALOG_DATA)private data: any,
		private permissionController: PermissionController,
		private matSnackbar: MatSnackBar
	) {
		this.roleId = data["role"].roleId
		this.roleName = data["role"].roleName
		this.permissions = (data["permissions"] as Array<PermissionModel>).sort((a, b) => a.permissionName.localeCompare(b.permissionName));
	}

	ngOnInit() {
		this.permissionController.getRolePermissionByRoleId(this.roleId)
			.subscribe(response => {
				this.grantedPermissions = response
			})

		this.matDialog.keydownEvents().subscribe(keyEvent => {
			if (keyEvent.code === "Escape") {
				this.closePermissionDialog()
			}
		})
	}

	protected closePermissionDialog(): void {
		this.matDialog.close();
	}

	protected notifyPermissionSave(isSuccess: boolean): void {
		let notifyMessage

		if (isSuccess) {
			notifyMessage = "Permissions updated successfully!"
		} else {
			notifyMessage = "Failed to update permissions!"
		}

		this.matSnackbar.open(notifyMessage, "Close", {
			panelClass: ['permission-save-notification'],
			horizontalPosition: "center",
			verticalPosition: "top",
			duration: 3000
		}).afterOpened()
	}

	/**
	 * Used to update slide toggle based on the permission fetch from server.
	 *
	 * @param permission
	 * @protected
	 */
	protected updateTogglePermission(permission: PermissionModel): boolean {
		return this.grantedPermissions
			.map(grantedPermission => grantedPermission.permission)
			.includes(permission.permissionName);
	}

	/**
	 * Used to grant/revoke permission for targeted role.
	 *
	 * @param permission
	 * @param matSlideToggleChange
	 * @protected
	 */
	protected onPermissionToggle(permission: PermissionModel, matSlideToggleChange: MatSlideToggleChange): void {
		let isChecked = matSlideToggleChange.checked
		let hasGrantedPermission = this.grantedPermissions
			.map(grantedPermission => grantedPermission.permission)
			.includes(permission.permissionName)

		switch (isChecked) {
			case true: {
				if (hasGrantedPermission) return

				let rolePermission = this.revokedPermissions
					.find(revokedPermission => revokedPermission.permission === permission.permissionName)

				if (rolePermission === undefined) {
					rolePermission = {
						id: 0,
						roleId: this.roleId,
						permission: permission.permissionName
					}
				} else {
					this.revokedPermissions.splice(
						this.revokedPermissions.findIndex(
							p => p.permission === permission.permissionName), 1)
				}

				this.grantedPermissions.push(rolePermission)
				break
			}

			case false: {
				if (!hasGrantedPermission) return

				let rolePermission = this.grantedPermissions
					.find(grantedPermission => grantedPermission.permission === permission.permissionName)!

				if (rolePermission.id !== null) {
					this.revokedPermissions.push(rolePermission)
				}

				this.grantedPermissions.splice(
					this.grantedPermissions.findIndex(
						p => p.permission === permission.permissionName), 1)
				break
			}
		}
	}

	/**
	 * Search permission.
	 *
	 * @param e
	 * @protected
	 */
	protected onSearchPermission(e: Event): void {
		let permissionSearchBox = e.currentTarget as HTMLInputElement;
		let searchPermission = permissionSearchBox.value;

		this.permissions = (this.data["permissions"] as Array<PermissionModel>)
			.filter(permission => StringUtils.containsString(permission.permissionName, searchPermission))
	}

	protected saveRolePermission(): void {
		let request: UpdateRolePermissionRequestModel = {
			roleId: this.roleId,
			grantedPermissions: this.grantedPermissions,
			revokedPermissions: this.revokedPermissions
		}

		this.permissionController.updateRolePermission(request).subscribe(
			response => {
				if (response.statusCode === 200) {
					this.notifyPermissionSave(true)
					return
				}

				this.notifyPermissionSave(false)
			}
		)
		this.closePermissionDialog()
	}
}
