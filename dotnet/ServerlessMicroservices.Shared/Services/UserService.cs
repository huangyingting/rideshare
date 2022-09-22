using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Azure.Identity;


namespace ServerlessMicroservices.Shared.Services
{
  public class UserService : IUserService
  {

    private GraphServiceClient graphClient;

    public UserService(ISettingService settingService)
          : this(settingService.GetGraphTenantId(), settingService.GetGraphClientId(), settingService.GetGraphClientSecret())
    {
    }

    public UserService(string tenantId, string clientId, string clientSecret)
    {
      if (string.IsNullOrEmpty(tenantId))
      {
        throw new ArgumentNullException(
          nameof(tenantId),
          "GraphTenantId environment variable must be set before instantiating UserService.");
      }
      var scopes = new[] { "https://graph.microsoft.com/.default" };
      var options = new TokenCredentialOptions
      {
        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
      };
      var clientSecretCredential = new ClientSecretCredential(
          tenantId, clientId, clientSecret, options);
      graphClient = new GraphServiceClient(clientSecretCredential, scopes);
    }


    public async Task<(IEnumerable<User>, Error)> GetUsers()
    {
      try
      {
        var graphUsers = await graphClient.Users
          .Request()
          .Select("identities,id,displayName,givenName,surname,streetAddress,city,state,postalCode,country,otherMails")
          .GetAsync();
        var users = from graphUser in graphUsers
                    select new User
                    {
                      Id = graphUser.Id,
                      DisplayName = graphUser.DisplayName,
                      GivenName = graphUser.GivenName,
                      Surname = graphUser.Surname,
                      StreetAddress = graphUser.StreetAddress,
                      City = graphUser.City,
                      State = graphUser.State,
                      PostalCode = graphUser.PostalCode,
                      Country = graphUser.Country,
                      Email = graphUser.Identities?.FirstOrDefault(x => x.SignInType == "emailAddress")?.IssuerAssignedId ??
          graphUser.OtherMails?.FirstOrDefault(),
                    };
        return (users, null);
      }
      catch (ServiceException e)
      {
        return (null, new Error
        {
          StatusCode = e.StatusCode,
          Code = e.Error.Code,
          Message = e.Error.Message,
        });
      }
    }

    public async Task<(User, Error)> GetUserById(string id)
    {
      if (String.IsNullOrWhiteSpace(id))
      {
        throw new ArgumentNullException(nameof(id));
      }
      try
      {
        var graphUser = await graphClient.Users[id]
          .Request()
          .Select("identities,id,displayName,givenName,surname,streetAddress,city,state,postalCode,country,otherMails")
          .GetAsync();
        return (new User
        {
          Id = graphUser.Id,
          DisplayName = graphUser.DisplayName,
          GivenName = graphUser.GivenName,
          Surname = graphUser.Surname,
          StreetAddress = graphUser.StreetAddress,
          City = graphUser.City,
          State = graphUser.State,
          PostalCode = graphUser.PostalCode,
          Country = graphUser.Country,
          Email = graphUser.Identities?.FirstOrDefault(x => x.SignInType == "emailAddress")?.IssuerAssignedId ??
          graphUser.OtherMails?.FirstOrDefault(),
        }, null);
      }
      catch (ServiceException e)
      {
        return (null, new Error
        {
          StatusCode = e.StatusCode,
          Code = e.Error.Code,
          Message = e.Error.Message,
        });
      }
    }
  }
}