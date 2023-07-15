namespace CompetitionNetworking.networking.dto
{
    [Serializable]
    public class UserDTO
    {
        public String Username { get; set; }
        public String Password { get; set; }

        public UserDTO(String Username)
        {
            this.Username = Username;
            this.Password = "";
        }
        public UserDTO(String Username, String Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
