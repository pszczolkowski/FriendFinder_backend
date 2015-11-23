using FriendFinder.Models;
using FriendFinder.Repository;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FriendFinder.Controllers
{   [Authorize]
    public class InvitationController : ApiController
    {
        private InvitationRepository invitationRepo = new InvitationRepository();

        public InvitationController() { }

        public InvitationController(InvitationRepository invitationRepo)
        {
            this.invitationRepo = invitationRepo;
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
                invitationRepo.Delete(invitation);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
