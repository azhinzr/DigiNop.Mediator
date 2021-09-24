namespace NopCommerce.Api.Authorizer.Models
{
    public class AccessModel
    {
        public AuthorizationModel AuthorizationModel { get; set; }
        public UserAccessModel UserAccessModel { get; set; }
    }
}