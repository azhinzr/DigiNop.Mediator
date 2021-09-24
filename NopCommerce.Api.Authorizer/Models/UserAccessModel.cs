namespace NopCommerce.Api.Authorizer.Models
{
    public class UserAccessModel
    {
        public string ClientId { get; set; } 
        public string ClientSecret { get; set; } 
        public string ServerUrl { get; set; }

     }
}