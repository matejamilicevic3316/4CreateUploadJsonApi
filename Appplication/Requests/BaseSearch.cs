namespace Appplication.Requests
{

    public abstract class BaseSearch
    {
        public int PerPage { get; set; } = 3;
        public int Page { get; set; } = 1;
        public string SortProperty { get; set; } = "Id";
        public QuerySort QuerySort { get; set; } = QuerySort.ASC;
    }

    public enum QuerySort
    {
        ASC,
        DESC
    }
}
