using FriendFinder.Models;
using FriendFinder.Providers;
using FriendFinder.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Cors;
using FriendFinder.Repository;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;
using FriendFinder.ViewModels;

namespace FriendFinder.Controllers
{
    [Authorize]
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private InvitationRepository invitationRepo = new InvitationRepository();
		private UserRepository userRepository = new UserRepository();

        public UserController() {}

        public UserController(InvitationRepository _invitationRepo, UserRepository userRepository)
        {
            this.invitationRepo = _invitationRepo;
        }

        public UserController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        
         public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
                
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Login  };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("Message", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }
            
        // POST user/logout
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
        [Route("position")]
        [HttpPost]
        public HttpResponseMessage PostPosition([FromBody]JToken json)
        {
			ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());

			user.Position = new Position()
            {
				Longitude = (double)json[ "Longitude" ] ,
				Latitude = (double)json[ "Latitude" ],
				LastUpdate = DateTime.Now

            };

			UserManager.Update(user);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("identity")]
        [HttpGet]
        public IdentityUser GetIdentity()
        {
            String userId = User.Identity.GetUserId();
            String userName = User.Identity.GetUserName();

            IdentityUser userIdentity = new IdentityUser()
            {
                Id = userId,
                UserName = userName
            };
            return userIdentity;
        }
            
        [Route("friend")]
        [HttpGet]
        public IEnumerable<FriendViewModel> GetFriend()
        {
            String loggedUserId = User.Identity.GetUserId();
			var friends = userRepository.FindFriendsOf(loggedUserId);

			return from friend in friends
				   select new FriendViewModel() {
					   id = friend.Id,
					   username = friend.UserName
				   };
        }

        [Route("location")]
        [HttpGet]
		public IEnumerable<FriendPositionViewModel> GetLocation()
        {
			String loggedUserId = User.Identity.GetUserId();
			var friends = userRepository.FindLoggedFriendsOf(loggedUserId);

			return from friend in friends
				   select new FriendPositionViewModel() {
					   id = friend.Id,
					   latitude = friend.Position.Latitude,
					   longitude = friend.Position.Longitude,
					   username = friend.UserName
				   };
        }

        // POST user/changePassword
        [Route("changePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST user/setPassword
        [Route("setPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            //public string UserName { get; set; }
            public string Login { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (/*UserName*/ Login!= null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, /*UserName*/Login, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    Login = identity.FindFirstValue(ClaimTypes.Name)
                   // UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
