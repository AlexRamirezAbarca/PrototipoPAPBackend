namespace API_PrototipoGestionPAP.Application.DTOs
{
    public class GeneralResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
