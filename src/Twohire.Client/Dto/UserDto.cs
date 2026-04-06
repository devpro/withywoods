namespace Withywoods.Twohire.Client.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Role { get; set; }
    }
}
