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

      /*  public IEnumerable<FriendPosition> GetFriendLocation(double longitude, double latitude, int distance)
        {
            var friendsLocation ;//= context.Positions.Where()
            return friendsLocation; 
        }*/
    }
}