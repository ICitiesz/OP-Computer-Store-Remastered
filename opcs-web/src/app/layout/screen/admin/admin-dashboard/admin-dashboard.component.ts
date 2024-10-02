import {AfterViewInit, Component, HostListener, ViewChild} from '@angular/core';
import {AdminHeaderComponent} from "../../../component/admin/admin-header/admin-header.component";
import {MatSidenavContent, MatSidenavModule} from "@angular/material/sidenav";
import {ComponentService} from "../../../../service/layout/component.service";
import {MatListItem, MatNavList} from "@angular/material/list";
import {MatIconModule} from "@angular/material/icon";
import {NgOptimizedImage} from "@angular/common";
import {AdminSideMenuComponent} from "../../../component/admin/admin-side-menu/admin-side-menu.component";

@Component({
	selector: 'app-admin-dashboard',
	standalone: true,
	imports: [
		AdminHeaderComponent,
		MatSidenavModule,
		MatListItem,
		MatIconModule,
		NgOptimizedImage,
		MatNavList,
		AdminSideMenuComponent
	],
	templateUrl: './admin-dashboard.component.html',
	styleUrl: './admin-dashboard.component.scss'
})
export class AdminDashboardComponent implements AfterViewInit {
	protected scrollHistory = 0

	constructor(
		private componentService: ComponentService
	) {

	}

	ngAfterViewInit() {
		this.componentService.getMatSideNavContainer().elementScrolled().subscribe(e =>{
			this.onPageScroll(e)
		})
	}

	/**
	 * Perform auto hide /show navbar while scrolling the page.
	 *
	 * @param e Scroll event
	 */
	onPageScroll(e: Event) {
		const domAsElement = e.currentTarget as Element

		if (domAsElement == null) return

		const domAsDoc = domAsElement.ownerDocument

		const adminNavBar = domAsDoc.getElementById("admin-nav-bar")!

		if (adminNavBar == null) return

		const navbarStyle = adminNavBar.style

		if (domAsElement.scrollTop <= this.scrollHistory) {
			navbarStyle.transform = "translateY(0)"
			navbarStyle.transition = "0.2s transform ease-in-out"

			this.scrollHistory = domAsElement.scrollTop
		} else if (domAsElement.scrollTop >= this.scrollHistory) {
			navbarStyle.transform = "translateY(-120%)"
			navbarStyle.transition = "0.2s transform ease-in-out"

			this.scrollHistory = domAsElement.scrollTop
		}
	}
}
