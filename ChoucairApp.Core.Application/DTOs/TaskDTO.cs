namespace ChoucairApp.Core.Application.DTOs
{
    public record TaskDTO(int ID, string Title, string Descripcion, DateTime EndDate, int StatusId)
    {
        public TaskDTO() : this(0,string.Empty, string.Empty, DateTime.Now,0) { }
    }
}
