import {SortField} from "../../../../../core/pagination/query-sort/sort-field";

export namespace RoleSortField {
	export const roleName: SortField = "roleName"
	export const createdDate: SortField = "createdDate"

	export function getAllRoleFields(): SortField[] {
		return [roleName, createdDate]
	}
}
