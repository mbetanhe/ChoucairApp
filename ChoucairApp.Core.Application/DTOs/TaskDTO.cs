namespace ChoucairApp.Core.Application.DTOs
{
    public record TaskDTO(int ID, string Title, string Descripcion, DateTime EndDate, int StatusId, string StatusDesc, string UserID)
    {
        public TaskDTO() : this(0,string.Empty, string.Empty, DateTime.Now, 0, string.Empty, string.Empty) { }
    }
}
