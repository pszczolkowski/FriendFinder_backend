using FriendFinder.Models;
using FriendFinder.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FriendFinder.Controllers
{   [Authorize]
    public class InvitationController : ApiController
    {
        private InvitationRepository invitationRepo = new InvitationRepository();
        private FriendRepository friendRepo = new FriendRepository();
        private UserRepository userRepo = new UserRepository();
		private ApplicationUserManager _userManager;

        public InvitationController() { }

        public InvitationController(InvitationRepository invitationRepo, FriendRepository friendRepo)
        {
            this.invitationRepo = invitationRepo;
            this.friendRepo = friendRepo;
        }

		public ApplicationUserManager UserManager {
			get {
				return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set {
				_userManager = value;
			}
		}

        [Route("invitation")]
        [HttpGet]
        public IEnumerable<Invitation> GetInvitations()
        {
            string userId = User.Identity.GetUserId();
            var invitations = invitationRepo.getInvitations(userId);
            return invitations;
        }

		[Route( "user/{id}/invite" )]
		[HttpPost]
		public HttpResponseMessage SendInvitation( string invitedUserId ) {
			string loggedUserId = User.Identity.GetUserId();
			var invitedUser = UserManager.FindById( invitedUserId );

			if(invitedUser == null) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User with given id doesn't exist");
			}
			if(loggedUserId.Equals(invitedUserId)) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You cannot invite yourself");
			}

			// TODO check if user is already friend with invited one

			if( invitationRepo.getForUsers( loggedUserId , invitedUserId ) != null ) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Friend invitation for that user is already active");
			}

			Invitation invitation = new Invitation() {
				InvitingId = loggedUserId,
				InvitedId = invitedUserId,
				Date = DateTime.Now
			};

			invitationRepo.Add(invitation);
			invitationRepo.Save();
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

        [Route("invitation/{id}/decline")]
        [HttpPost]
        public HttpResponseMessage DeclineInvitation(int id)
        {
            var invitation = invitationRepo.getById(id);
            if(invitation != null)
            {
                string userId = User.Identity.GetUserId();
                if (!userId.Equals(invitation.UserId))
                {
                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
                invitationRepo.Delete(invitation);
                invitationRepo.Save();
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("invitation/{id}/accept")]
        [HttpPost]
        public HttpResponseMessage AcceptInvitation(int id)
        {
            var invitation = invitationRepo.getById(id);
            if (invitation == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            if (!userId.Equals(invitation.UserId))
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            var friend = new Friend()
            {
                UserId = invitation.UserId,
                FriendId = invitation.InviterId,
                FriendUserName = userRepo.FindById(invitation.UserId).UserName
            };
            friendRepo.Add(friend);
            friendRepo.Save();
            invitationRepo.Delete(invitation);
            invitationRepo.Save();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
