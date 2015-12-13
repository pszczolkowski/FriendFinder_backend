using FriendFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendFinder.ViewModels {

	public class FriendPositionViewModel {

		public string id { get; set; }
		public string username { get; set; }
		public double longitude { get; set; }
		public double latitude { get; set; }

	}

}