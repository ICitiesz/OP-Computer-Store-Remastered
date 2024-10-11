export interface PaginationRequestModel<TSearch, TSort> {
	search: TSearch,
	currentPage: number,
	totalItemsPerPage: number,
	sort: TSort,
}
