export interface PaginationResponseModel<TModel> {
	currentPage: number,
	totalItems: number,
	totalPages: number,
	items: Array<TModel>
}
