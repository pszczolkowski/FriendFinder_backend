using FriendFinder.Models;
using FriendFinder.Repository;
using FriendFinder.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FriendFinder.Controllers
{   [Authorize]
    public class InvitationController : ApiController
    {
        private InvitationRepository invitationRepo = new InvitationRepository();
		private ApplicationUserManager _userManager;
		private ApplicationDbContext Context;
		private UserRepository userRepository;

        public InvitationController() { }

        public InvitationController(InvitationRepository invitationRepo, ApplicationDbContext context, UserRepository userRepository)
        {
            this.invitationRepo = invitationRepo;
			this.Context = context;
			this.userRepository = userRepository;
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
        public IEnumerable<InvitationViewModel> GetInvitations()
        {
            string userId = User.Identity.GetUserId();
            IQueryable<Invitation> invitations = invitationRepo.getInvitations(userId);
			IQueryable<ApplicationUser> users = userRepository.FindAll();


			return from invitation in invitations
				   join user in users on invitation.InvitingId equals user.Id
					select new InvitationViewModel(invitation, user);
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
			if (invitedUser.Friends.Any(u => u.Id == loggedUserId)) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You are already friend with that user");
			}
			if (invitationRepo.getForUsers( loggedUserId, invitedUserId) != null) {
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

			if (invitation == null) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invitation doesn't exist");
			}
			if(invitation.InvitedId != User.Identity.GetUserId()) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invitation doesn't exist");
			}

			invitationRepo.Delete( invitation );
			invitationRepo.Save();

			return new HttpResponseMessage( HttpStatusCode.OK );
        }

        [Route("invitation/{id}/accept")]
        [HttpPost]
        public HttpResponseMessage AcceptInvitation(int id)
        {
            var invitation = invitationRepo.getById(id);

			if(invitation == null) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invitation doesn't exist");
			}
			if(invitation.InvitedId != User.Identity.GetUserId()) {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invitation doesn't exist");
			}

			var invitingUser = userRepository.FindById(invitation.InvitingId);
			var loggedUser = userRepository.FindById(User.Identity.GetUserId());

			using (Context.Database.BeginTransaction()) {
				invitingUser.Friends.Add(loggedUser);
				loggedUser.Friends.Add(invitingUser);

				UserManager.Update(invitingUser);
				UserManager.Update(loggedUser);

				invitationRepo.Delete(invitation);
				invitationRepo.Save();
			}

            
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
