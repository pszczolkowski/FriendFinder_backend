using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriendFinder.Models;

namespace FriendFinder.Repository
{
    public class UserRepository : IRepository<ApplicationUser>
    {
		private const int MAX_UPDATE_INTERVAL_FOR_BEING_LOGGED = 30;

        private ApplicationDbContext context;

        public UserRepository()
        {
            context = new ApplicationDbContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

       public ApplicationUser FindById(string UserId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id.Equals(UserId));
            return user;
        }

	   public IQueryable<ApplicationUser> FindAll() {
		   return context.Users;
	   }

	   public IEnumerable<ApplicationUser> FindLoggedFriendsOf(string userId) {
			return context.Users
                .First(u => u.Id == userId)
                .Friends
			    .Where(u => u.Position.LastUpdate.AddSeconds(MAX_UPDATE_INTERVAL_FOR_BEING_LOGGED) >= DateTime.Now);
	   }

	   public ApplicationUser FindByUsername(string username) {
		   return context.Users
			   .FirstOrDefault(u => u.UserName == username);
	   }
    }
}