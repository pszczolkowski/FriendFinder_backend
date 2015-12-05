using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriendFinder.Models;

namespace FriendFinder.Repository
{
    /*public class FriendPositionRepository : IRepository<FriendPosition>
    {
		private const int MAX_POSITION_UPDATE_SECONDS = 30;

        private ApplicationDbContext context;

        public FriendPositionRepository()
        {
            context = new ApplicationDbContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

         public IQueryable<FriendPosition> GetLoggedFriendsLocations(string userId)
         {
			 return from friend in context.Friends
					 join user in context.Users on friend.FriendId equals user.Id
					 where friend.UserId == userId && user.Position.LastUpdate.AddSeconds(MAX_POSITION_UPDATE_SECONDS) > DateTime.Now
					 select new FriendPosition() {
						 UserId = friend.FriendId ,
						 UserName = friend.FriendUserName ,
						 Longitude = user.Position.Longitude ,
						 Latitude = user.Position.Latitude
					 };
          }
    }*/
}