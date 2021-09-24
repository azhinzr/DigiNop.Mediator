using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ApiClient.NopCommerce.ApiStandardResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NopCommerce.Api.Authorizer.Managers;
using NopCommerce.Api.Authorizer.Models;
using NopCommerce.Api.Authorizer.Parameters;
using ApiClient.ApiStandardResults;

namespace NopCommerce.Api.Authorizer.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private   string ServerUrl;

        public AuthorizationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("TokenProvider")]
        public ActionResult TokenProvider([FromBody] UserAccessModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var redirectUrl = _configuration["RedirectUrl"];
                    var clientId = model.ClientId;
                    var clientSecret = model.ClientSecret;
                    HttpContext.Session.SetString("ServerUrl", model.ServerUrl);
                    HttpContext.Session.SetString("clientId", clientId);
                    HttpContext.Session.SetString("clientSecret", clientSecret);

                    var nopAuthorizationManager = new AuthorizationManager(clientId, clientSecret, model.ServerUrl);
                    ServerUrl = model.ServerUrl;

                    var state = Guid.NewGuid();
                    string authUrl = nopAuthorizationManager.BuildAuthUrl(redirectUrl, new string[] { }, state.ToString());

                    return Redirect(authUrl);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("token")]
        public ApiResult<TokenModel> GetAccessToken(string code, string state)
        {
            try
            {
                var authParameters = new AuthParameters()
                {
                    ClientId = HttpContext.Session.GetString("clientId"),
                    ClientSecret = HttpContext.Session.GetString("clientSecret"),
                    ServerUrl =   HttpContext.Session.GetString("ServerUrl"),
                    RedirectUrl = _configuration["RedirectUrl"],
                    GrantType = "authorization_code",
                    Code = code
                };

                var nopAuthorizationManager = new AuthorizationManager(authParameters.ClientId, authParameters.ClientSecret, authParameters.ServerUrl);

                var responseJson = nopAuthorizationManager.GetAuthorizationData(authParameters);

                var authorizationModel = JsonConvert.DeserializeObject<AuthorizationModel>(responseJson);

                var result = authorizationModel.AccessToken;
                return   ApiResult<TokenModel>.StandardOk(new TokenModel { Token= result});
            }
             
            catch (Exception ex)
            {
                return ApiResult<TokenModel>.StandardOk(null);
            } 
        
        }

     }

    public class TokenModel
    {
        public string Token { get; set; }
    }
}
