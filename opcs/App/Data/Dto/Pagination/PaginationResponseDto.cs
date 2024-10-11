namespace opcs.App.Data.Dto.Pagination;

public record PaginationResponseDto<T>
(
    int CurrentPage,
    int TotalItems,
    int TotalPages,
    List<T> Items
);