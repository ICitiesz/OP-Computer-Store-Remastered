namespace opcs.App.Data.Dto.Pagination;

public class QuerySortField
{
    public enum SortFieldEnum
    {
        RoleSortField
    }

    public static QuerySortField Instance => _Instance.Value;

    private static readonly Lazy<QuerySortField> _Instance = new(() => new QuerySortField());

    private readonly Dictionary<SortFieldEnum, List<string>> _sortFields = new()
    {
        { SortFieldEnum.RoleSortField, ["roleName", "createdDate"] }
    };

    private QuerySortField() { }

    public List<string> GetSortFields(SortFieldEnum sortField)
    {
        return _sortFields[sortField];
    }
}