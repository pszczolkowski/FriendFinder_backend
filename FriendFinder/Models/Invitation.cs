using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendFinder.Models {
	public class Invitation {
		[Key]
		public int Id { get; set; }
		public string InvitingId { get; set; }
		public string InvitedId { get; set; }
		public DateTime Date { get; set; }
	}
}