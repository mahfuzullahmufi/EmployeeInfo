namespace EmployeeInfo.Models
{
    public class PagedViewModel<T>
    {
        public List<T> PagedData { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
