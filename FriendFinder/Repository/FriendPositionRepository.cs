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
			 return from position in context.Positions
					 join friend in context.Friends on position.UserId equals friend.FriendId
					 where friend.UserId == userId
					 select new FriendPosition() {
						 UserId = friend.FriendId,
						 UserName = friend.FriendUserName,
						 Longitude = position.Longitude,
						 Latitude = position.Latitude
					 };
          }
    }
}