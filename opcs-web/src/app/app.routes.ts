import {Routes} from '@angular/router';
import {AdminDashboardComponent} from "./layout/screen/admin/admin-dashboard/admin-dashboard.component";
import {
	RolePermissionFrameComponent
} from "./layout/component/admin/role-permission-frame/role-permission-frame.component";
import {LoginComponent} from "./layout/component/security/auth/login/login.component";


export const routes: Routes = [
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
