using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriendFinder.Models;

namespace FriendFinder.Repository
{
    public class FriendPositionRepository : IRepository<FriendPosition>
    {
        private ApplicationDbContext context;

        public FriendPositionRepository()
        {
            context = new ApplicationDbContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

         public IQueryable<FriendPosition> GetFriendsLocations(string userId)
         {
			 return from friend in context.Friends
					 join user in context.Users on friend.FriendId equals user.Id
					 where friend.UserId == userId
					 select new FriendPosition() {
						 UserId = friend.FriendId ,
						 UserName = friend.FriendUserName ,
						 Longitude = user.Position.Longitude ,
						 Latitude = user.Position.Latitude
					 };
          }
    }
}