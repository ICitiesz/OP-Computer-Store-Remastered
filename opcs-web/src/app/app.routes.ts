import {Routes} from '@angular/router';
import {AdminDashboardComponent} from "./layout/screen/admin/admin-dashboard/admin-dashboard.component";
import {
	RolePermissionFrameComponent
} from "./layout/component/admin/role-permission-frame/role-permission-frame.component";
import {AdminHomeComponent} from "./layout/screen/admin/admin-home/admin-home.component";
import {LoginComponent} from "./layout/component/security/auth/login/login.component";


export const routes: Routes = [
	{
		path: 'admin/home',
		component: AdminHomeComponent
	},
	{
		path: "login",
		component: LoginComponent
	},
	{
		path: "administration/dashboard",
		component: AdminDashboardComponent,
		title: "Dashboard",
		children: [
			{
				path: "roles-permissions",
				component: RolePermissionFrameComponent,
				title: "Roles & Permissions"
			}
		]
	}
];
