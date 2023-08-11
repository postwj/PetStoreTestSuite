using Newtonsoft.Json;

namespace PetStore.Tests.DTOs
{
    [Serializable]
    public class User : IEquatable<User>
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "userStatus")]
        public int UserStatus { get; set; }

        public bool Equals(User? other)
        {
            if (other == null) { return false; }
            if (Id != other?.Id) { return false; }
            if (UserName != other?.UserName) { return false; }
            if (FirstName != other?.FirstName) {  return false; }
            if (LastName != other?.LastName) {  return false; }
            if (Password != other?.Password) {  return false; }
            if (Phone != other?.Phone) { return false; }
            if (UserStatus != other?.UserStatus) {  return false; }
            return true;
        }

        public override bool Equals(object? obj)
        {
            User? user = obj as User;
            return Equals(user);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
