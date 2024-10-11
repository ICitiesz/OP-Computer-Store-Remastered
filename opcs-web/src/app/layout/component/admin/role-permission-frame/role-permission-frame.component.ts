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
import {AsyncPipe, JsonPipe, NgForOf, NgIf, NgStyle} from "@angular/common";
import {RoleModel} from "../../../../model/security/role.model";
import {PermissionController} from "../../../../controller/security/permission.controller";
import {PermissionModel} from "../../../../model/security/permission.model";
import {registerIcon} from "../../../../core/utils/resource-resolve.utils";
import {DateTimeUtils} from "../../../../core/utils/date-time.utils";

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
		NgForOf,
		NgStyle
	],
	templateUrl: './role-permission-frame.component.html',
	styleUrl: './role-permission-frame.component.scss'
})
export class RolePermissionFrameComponent implements OnInit {
	protected readonly IconProvider = IconProvider;
	protected readonly DateTimeUtils = DateTimeUtils;

	protected roles!: Array<RoleModel>
	protected permissions!: Array<PermissionModel>

	/* Pagination properties */
	protected roleSearch = ""
	protected totalItemsPerPage: number = 10
	protected totalItems!: number
	protected currentPage: number = 1
	protected totalPages!: number
	private pageNumberListSize = 5
	protected firstItemCount = 1
	protected lastItemCount = 10
	protected pageIndexList: number[] = Array()

	constructor(
		private matDialog: MatDialog,
		private roleController: RoleController,
		private permissionController: PermissionController
	) {
		registerIcon(IconProvider.LEFT_ARROW)
		registerIcon(IconProvider.RIGHT_ARROW)
	}

	ngOnInit(): void {
		this.sendQueryPageRequest()

		this.permissionController.getAllPermission().subscribe(response => {
			this.permissions = response
		})
	}

	protected openRolePermissionDialog(data: any): void {
		this.matDialog.open(PermissionDialogComponent, {
			height: '600px',
			width: '800px',
			disableClose: true,
			data: data
		})
	}

	protected updateRoleDisplaySize(e: Event) {
		let itemSize = this.tryParseNumber((e.currentTarget as HTMLInputElement).value)

		if (isNaN(itemSize)) return

		if (itemSize > this.totalItemsPerPage) {
			this.currentPage = 1;
		}

		this.totalItemsPerPage = itemSize

		this.sendQueryPageRequest()
	}

	protected navigateToPage(e: Event) {
		let pageNumber = this.tryParseNumber((e.currentTarget as HTMLAnchorElement).innerText)

		if (isNaN(pageNumber)) return

		if (pageNumber < 1 || pageNumber > this.totalPages || pageNumber === this.currentPage) {
			return
		}

		this.currentPage = pageNumber

		this.sendQueryPageRequest()
	}

	protected navigatePage(direction: 'previous' | 'next') {
		switch (direction) {
			case 'previous': {
				if (this.currentPage <= 1) break

				this.currentPage -= 1;
				this.sendQueryPageRequest()
				break
			}

			case 'next': {
				if (this.currentPage >= this.totalPages) break

				this.currentPage += 1;
				this.sendQueryPageRequest()
				break
			}
		}
	}

	protected showEllipsis(position: 'start' | 'end'): boolean {
		let hasCurrentPageIndex = this.pageIndexList.includes(this.currentPage)

		if (!hasCurrentPageIndex) return false

		if (position === 'start') {
			return this.pageIndexList[0] !== 1
		}

		if (position === 'end') {
			return this.pageIndexList[this.pageIndexList.length - 1] !== this.totalPages
		}

		return false
	}

	private sendQueryPageRequest() {
		this.roleController.queryPage({
			search: this.roleSearch,
			currentPage: this.currentPage,
			totalItemsPerPage: this.totalItemsPerPage,
			sort: {
				roleNameDesc: false
			}
		}).subscribe(response => {
			console.log(response);
			this.roles = response.items
			this.totalPages = response.totalPages
			this.currentPage = response.currentPage
			this.totalItems = response.totalItems

			this.updatePaginationIndex()
			this.updateShownItemCount()
		});
	}

	/**
	 * Used to update pagination index.
	 *
	 * @private
	 */
	private updatePaginationIndex(): void {
		let pageIndexStart
		let pageIndexEnd

		this.pageIndexList = Array()

		/* Configuring pageIndexStart */
		if (this.currentPage <= 2 || this.currentPage < 5) {
			pageIndexStart = 1
		} else if ((this.totalPages - this.currentPage) > 2) {
			pageIndexStart = this.currentPage - 2
		} else {
			pageIndexStart = this.currentPage - 2 + (this.totalPages - this.currentPage - 2)
		}

		/* Configuring pageIndexEnd */
		if (((pageIndexStart - 1) + this.pageNumberListSize) >= this.totalPages) {
			pageIndexEnd = this.totalPages
		} else {
			pageIndexEnd = (pageIndexStart - 1) + this.pageNumberListSize
		}

		//this.pageIndexList = Array.from({length: pageIndexEnd - pageIndexStart + 1}, (_ :number, index) => pageIndexStart + index)

		for (let i = pageIndexStart; i <= pageIndexEnd; i++) {
			this.pageIndexList.push(i)
		}
	}

	private updateShownItemCount(): void {
		this.lastItemCount = this.currentPage === this.totalPages ? this.totalItems : this.roles.length * this.currentPage
		this.firstItemCount = this.roles.length < this.totalItemsPerPage ? this.lastItemCount - (this.roles.length - (this.totalItemsPerPage - this.roles.length)) : this.lastItemCount - (this.totalItemsPerPage - 1)
	}

	// protected showPageNumber(pageIndex: number) {
	// 	let currentPagePointer = (this.currentPage) / 10
	// 	let pageIndexPointer = pageIndex / 10
	// 	let pageRangeStart = Math.floor(currentPagePointer)
	// 	let pageRangeEnd = Math.ceil(currentPagePointer)
	//
	// 	// console.log("Current Page Index: " + pageIndex)
	// 	// console.log("Current Page: " + this.currentPage)
	// 	// console.log("PageIndexPointer: " + pageIndexPointer)
	// 	// console.log("currentPagePointer: " + currentPagePointer)
	// 	// console.log("Ceil: " + pageRangeStart)
	// 	// console.log("Floor: " + pageRangeEnd)
	//
	// 	// guiItemIndex = maxItemPerPage * guiPageIndex + index
	// 	// newPageIndex = 10 * pageIndex +
	//
	// 	if (pageRangeStart === pageRangeEnd) {
	// 		return pageIndexPointer <= currentPagePointer
	// 	}
	//
	// 	return (pageIndexPointer > pageRangeStart && pageIndexPointer <= pageRangeEnd)
	// }

	private tryParseNumber(value: any): number {
		try {
			return Number(value)
		} catch(e) {
			console.error("Error parsing number:", (e as Error).message);
			return NaN
		}
	}
}
