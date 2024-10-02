import { Injectable } from '@angular/core';
import {MatSidenav, MatSidenavContent} from "@angular/material/sidenav";

@Injectable({
  providedIn: 'root'
})
export class ComponentService {
	private _matSideNav!: MatSidenav;
	private _matSideNavContainer!: MatSidenavContent

	setMatSideNav(value: MatSidenav) {
		this._matSideNav = value;
	}

	setMatSideNavContainer(value: MatSidenavContent) {
		this._matSideNavContainer = value
	}

	getMatSideNavContainer(): MatSidenavContent {
		return this._matSideNavContainer
	}

	toggleMatSideNav() {
		return this._matSideNav.toggle();
	}

	constructor() { }
}
