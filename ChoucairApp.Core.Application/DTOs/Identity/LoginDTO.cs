namespace ChoucairApp.Core.Application.DTOs.Identity
{
    public record LoginDTO(string Email, string Password)
    {
        public LoginDTO() : this(string.Empty, string.Empty) { }
    }
}
