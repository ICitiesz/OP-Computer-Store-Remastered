import {Component, Inject, ViewEncapsulation} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {IconProvider} from "../../../../core/icon-provider";
import {MatIcon} from "@angular/material/icon";
import {MatRipple} from "@angular/material/core";
import {NgForOf, NgStyle} from "@angular/common";
import {MatSlideToggle} from "@angular/material/slide-toggle";
import {PermissionModel} from "../../../../model/security/permission.model";

@Component({
	selector: 'app-permission-dialog',
	standalone: true,
	imports: [MatDialogModule, MatIcon, MatRipple, NgStyle, MatSlideToggle, NgForOf],
	encapsulation: ViewEncapsulation.None,
	templateUrl: './permission-dialog.component.html',
	styleUrl: './permission-dialog.component.scss'
})
export class PermissionDialogComponent {
	protected roleName: string
	protected permissions: Array<PermissionModel>

	protected readonly IconProvider = IconProvider;

	constructor(
		private matDialog: MatDialogRef<PermissionDialogComponent>,
		@Inject(MAT_DIALOG_DATA) data: any,
	) {
		this.roleName = data["roleName"];
		this.permissions = data["permissions"];
	}

	protected closePermissionDialog(): void {
		this.matDialog.close();
	}
}
