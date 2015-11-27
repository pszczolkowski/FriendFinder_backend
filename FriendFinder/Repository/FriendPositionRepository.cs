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

         public IQueryable<Position> GetFriendsLocations(string userId)
         {
			 var friendIds = context.Friends
				 .Where(f => f.UserId == userId)
				 .Select(f => f.FriendId);

			 return context.Positions
				 .Where(p => friendIds.Contains(p.UserId));
          }
    }
}