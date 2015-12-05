using FriendFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendFinder.ViewModels {
	public class InvitationViewModel {

		public InvitationViewModel(Invitation invitation, ApplicationUser invitingUser) {
			this.id = invitation.Id;
			this.invitingId = invitation.InvitingId;
			this.invitingName = invitingUser.UserName;
			this.sentAt = invitation.Date;
		}

		public int id { get; set; }
		public string invitingId { get; set; }
		public string invitingName { get; set; }
		public DateTime sentAt { get; set; }

	}
}