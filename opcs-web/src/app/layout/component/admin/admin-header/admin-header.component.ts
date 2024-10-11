import {Component} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {ComponentService} from "../../../../service/layout/component.service";
import {MatIconModule} from "@angular/material/icon";
import {IconProvider} from "../../../../core/icon-provider";
import {MatRippleModule} from "@angular/material/core";
import {registerIcon} from "../../../../core/utils/resource-resolve.utils";

@Component({
	selector: 'app-admin-header',
	standalone: true,
	imports: [
		MatButton,
		MatIconModule,
		MatRippleModule
	],
	templateUrl: './admin-header.component.html',
	styleUrl: './admin-header.component.scss'
})
export class AdminHeaderComponent {
	protected readonly IconProvider = IconProvider;

	constructor(
		private componentService: ComponentService
	) {
		registerIcon(IconProvider.USER)
		registerIcon(IconProvider.SEARCH)
	}

	toggleSideMenu() {
		this.componentService.toggleMatSideNav()
	}
}
