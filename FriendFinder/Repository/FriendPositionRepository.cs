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

         public FriendPosition GetFriendLocation(string userId)
          {
               var friendId = context.Friends.Where(f => f.UserId == userId).Select(f => f.FriendId).First();
              var friendUserName = context.Friends.Where(f => f.UserId == userId).Select(f => f.FriendUserName).First();
              var positionLongitude = context.Positions.Where(f => f.UserId == friendId).Select(f => f.Longitude).First();
              var positionLatitude = context.Positions.Where(f => f.UserId == friendId).Select(f => f.Latitude).First();
            
              FriendPosition friendPosition = new FriendPosition()
              {
                  FriendId = friendId,
                  FriendUserName = friendUserName,
                  Longitude = positionLongitude,
                  Latitude = positionLatitude

              };
              return friendPosition;
          }
    }
}