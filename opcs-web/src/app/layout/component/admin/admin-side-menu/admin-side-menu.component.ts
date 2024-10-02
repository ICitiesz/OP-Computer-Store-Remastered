import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatDrawerMode, MatSidenav, MatSidenavContent, MatSidenavModule} from "@angular/material/sidenav";
import {ComponentService} from "../../../../service/layout/component.service";
import {MatIconModule} from "@angular/material/icon";
import {MatListItem, MatNavList} from "@angular/material/list";
import {registerIcon} from "../../../../core/utils/resource-resolver";
import {animate, animateChild, group, query, state, style, transition, trigger} from "@angular/animations";
import {NgIf, NgStyle} from "@angular/common";
import {IconProvider} from "../../../../core/icon-provider";
import {AdminHeaderComponent} from "../admin-header/admin-header.component";
import {RouterLink, RouterOutlet} from "@angular/router";

@Component({
	selector: 'app-admin-side-menu',
	standalone: true,
	imports: [
		MatSidenavModule,
		MatNavList,
		MatIconModule,
		MatListItem,
		NgStyle,
		NgIf,
		AdminHeaderComponent,
		RouterOutlet,
		RouterLink
	],
	templateUrl: './admin-side-menu.component.html',
	styleUrl: './admin-side-menu.component.scss',
	animations: [
		trigger("sideMenuState", [
			state("hoverIn", style({width: "260px"})),
			state("hoverOut", style({width: "60px"})),

			transition("hoverIn <=> hoverOut", [
				group([
					animate("0.3s ease-in"),
					query("@titleFlyIn", animateChild())
				])]
			)]
		),

		trigger("titleFlyIn", [
			state("flyIn", style({zIndex: -1, opacity: 1, transform: "translateX(0)", transition: "opacity 0.5s"})),
			state("flyOut", style({zIndex: -1, opacity: 0, transform: "translateX(-25%)", transition: "opacity 0.5s"})),

			transition("flyIn => flyOut",
				[animate("0.3s ease-out")]
			),
			transition("flyOut => flyIn",
				[animate("0.3s 0.02s ease-in")]
			)
		]),

		trigger("pushNavBar", [
			state("pushNav", style({transform: "translateX(100%)"})),
			state("pullNav", style({transform: "translateX(0)"})),

			transition("pushNav <=> pullNav",
				[animate("0.3s ease-in")]
			)
		])
	]
})

export class AdminSideMenuComponent implements AfterViewInit {
	@ViewChild("sideMenu") sideMenu!: MatSidenav;
	@ViewChild("mainContainer") container!: MatSidenavContent

	/* Side menu properties */
	protected sideMenuToggleState: "hoverIn" | "hoverOut" = "hoverOut"
	protected titleFlyInState: "flyIn" | "flyOut" = "flyOut"
	protected navbarPushState: "pushNav" | "pullNav" = "pullNav"
	protected sideMenuMode: MatDrawerMode = "side"

	protected rolePermissionRouteLink = "roles-permissions"
	protected readonly IconProvider = IconProvider;

	constructor(
		private componentService: ComponentService,
	) {
		registerIcon(IconProvider.DASHBOARD)
		registerIcon(IconProvider.CHART)
		registerIcon(IconProvider.USER_KEY)
		registerIcon(IconProvider.WORKER)
		registerIcon(IconProvider.STORE_LOGO)
	}

	ngAfterViewInit(): void {
		this.componentService.setMatSideNav(this.sideMenu)
		this.componentService.setMatSideNavContainer(this.container)
	}

	protected toggleSideMenu() {
		if (this.sideMenuToggleState == "hoverIn") {
			this.sideMenuToggleState = "hoverOut"
			this.titleFlyInState = "flyOut"
			this.navbarPushState = "pushNav"
		} else if (this.sideMenuToggleState == "hoverOut") {
			this.sideMenuToggleState = "hoverIn"
			this.titleFlyInState = "flyIn"
			this.navbarPushState = "pullNav"
		}
	}
}
