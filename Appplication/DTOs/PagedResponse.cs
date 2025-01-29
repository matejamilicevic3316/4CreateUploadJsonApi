namespace Appplication.DTOs
{

    public class PagedResponse<TDto>
    {
        public int PerPage { get; set; }
        public int Page { get; set; }
        public required IEnumerable<TDto> Dtos { get; set; }
        public int TotalPages { get; set; }
    }
}
