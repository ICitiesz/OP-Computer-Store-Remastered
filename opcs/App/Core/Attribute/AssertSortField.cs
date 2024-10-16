using System.ComponentModel.DataAnnotations;
using opcs.App.Data.Dto.Pagination;
using opcs.Resources;

namespace opcs.App.Core.Attribute;

[AttributeUsage(AttributeTargets.Parameter)]
public class AssertSortField<TSearch>(QuerySortField.SortFieldEnum sortFieldEnum, string? errorMessage = null) : ValidationAttribute
{
    private readonly string _errorMessage = errorMessage ?? CodeMessages.opcs_error_request_value_not_found;

    protected override ValidationResult? IsValid(object? paginationRequest, ValidationContext validationContext)
    {
        if (paginationRequest is null) return new ValidationResult(CodeMessages.opcs_error_request_empty_property);

        var paginationRequestDto = (paginationRequest as PaginationRequestDto<TSearch>)!;
        var sortFields = QuerySortField.Instance.GetSortFields(sortFieldEnum);

        if (sortFields.Contains(paginationRequestDto.Sort.SortBy)) return ValidationResult.Success;

        var defaultSort = new QuerySort(SortBy: sortFields.First(), Descending: false);

        validationContext.ObjectType.GetProperty(nameof(paginationRequestDto.Sort))!.SetValue(paginationRequestDto, defaultSort);

        return ValidationResult.Success;
    }
}
