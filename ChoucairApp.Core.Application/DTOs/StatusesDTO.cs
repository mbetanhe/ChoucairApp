namespace ChoucairApp.Core.Application.DTOs
{
    public record StatusesDTO(int ID, string Description)
    {
        public StatusesDTO() : this(0, string.Empty) { }
    }
}
