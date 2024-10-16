using System.ComponentModel.DataAnnotations;
using opcs.App.Core.Attribute;

namespace opcs.App.Data.Dto.Pagination;

public record PaginationRequestDto<TSearch>
(
    TSearch Search,
    [Required]
    [AssertNotLess(errorSetDefault: true, targetValue: 1)]
    int CurrentPage,
    [Required]
    [AssertInCollection(errorSetDefault: true, collection: [10, 25, 50])]
    int TotalItemsPerPage,
    [Required]
    QuerySort Sort);