import {QuerySortModel} from "./sort/query-sort.model";

export interface PaginationRequestModel<TSearch> {
	search: TSearch,
	currentPage: number,
	totalItemsPerPage: number,
	sort: QuerySortModel,
}
