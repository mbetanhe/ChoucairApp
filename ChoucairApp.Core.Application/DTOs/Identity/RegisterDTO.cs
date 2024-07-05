namespace ChoucairApp.Core.Application.DTOs.Identity
{
    public record RegisterDTO(string Document, string Firsname, string Lastname, string UserName, string Email, string Password)
    {
        public RegisterDTO() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }
    }
}
