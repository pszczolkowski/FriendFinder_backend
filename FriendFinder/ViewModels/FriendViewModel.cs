using FriendFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendFinder.ViewModels {
	public class FriendViewModel {

		public FriendViewModel(ApplicationUser user) {
			this.id = user.Id;
			this.username = user.UserName;
		}

		public string id { get; set; }

		public string username { get; set; }

	}
}