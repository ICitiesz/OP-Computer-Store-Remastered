import {SortField} from "./sort-field";
import {SortOrder} from "./sort-order";

export interface ISorter {
	sort(sortField: SortField): void;

	updateSortIndicator(sortField: SortField, sortOrder: SortOrder): object;

	resetSort(): void;
}
