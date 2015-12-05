using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FriendFinder.Models {
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
		public ApplicationDbContext() : base("FriendFinderDB") { }

		public DbSet<Invitation> Invitations { get; set; }
		
		public static ApplicationDbContext Create() {
			return new ApplicationDbContext();
		}
	}

}