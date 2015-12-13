using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FriendFinder.Models {
	public class Invitation {
		[Key]
		public int InvitationId { get; set; }
		public string InvitingId { get; set; }
		public string InvitedId { get; set; }
		public DateTime Date { get; set; }


		[ForeignKey("InvitingId")]
		public virtual ApplicationUser InvitingUser  { get; set; }

		[ForeignKey( "InvitedId" )]
		public virtual ApplicationUser InvitedUser  { get; set; }

	}
}