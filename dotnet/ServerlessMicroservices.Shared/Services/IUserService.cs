using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerlessMicroservices.Shared.Services
{
  public interface IUserService
  {
    Task<(IEnumerable<User>, Error)> GetUsers();
    Task<(User, Error)> GetUserById(string id);
  }

  public class User
  {
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("displayName")]
    public string DisplayName { get; set; }
    [JsonProperty("givenName")]
    public string GivenName { get; set; }
    [JsonProperty("surname")]
    public string Surname { get; set; }
    [JsonProperty("streetAddress")]
    public string StreetAddress { get; set; }
    [JsonProperty("city")]
    public string City { get; set; }
    [JsonProperty("state")]
    public string State { get; set; }
    [JsonProperty("postalCode")]
    public string PostalCode { get; set; }
    [JsonProperty("country")]
    public string Country { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
  }


  public class Error
  {
    [JsonIgnore]
    public System.Net.HttpStatusCode StatusCode;
    [JsonProperty("code")]
    public string Code { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
  }
}
