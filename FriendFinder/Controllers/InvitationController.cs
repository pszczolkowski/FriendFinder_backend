using FriendFinder.Models;
using FriendFinder.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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

        public InvitationController() { }

        public InvitationController(InvitationRepository invitationRepo, FriendRepository friendRepo)
        {
            this.invitationRepo = invitationRepo;
            this.friendRepo = friendRepo;
        }

        [Route("invitation")]
        [HttpGet]
        public IEnumerable<Invitation> GetInvitations()
        {
            string userId = User.Identity.GetUserId();
            var invitations = invitationRepo.getInvitations(userId);
            return invitations;
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
