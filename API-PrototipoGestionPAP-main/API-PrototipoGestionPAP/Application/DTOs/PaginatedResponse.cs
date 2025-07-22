namespace API_PrototipoGestionPAP.Application.DTOs
{
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }

}
