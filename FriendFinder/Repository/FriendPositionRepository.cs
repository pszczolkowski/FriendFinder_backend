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
        //w budowie :-)
        public IEnumerable<FriendPosition> GetFriendLocation(string userId, double longitude, double latitude, int distance)
        {
            /*  
            IEnumerable<FriendPosition> friendPositionList = new List<FriendPosition>();
            IEnumerable<Friend> friendList = new List<Friend>();
           
            
          var  UsersIdList = context.Users.Select(u => u.Id).AsEnumerable();
        //  var user = UsersIdList.ElementAt(1);
            friendList = context.Friends.Where(f => f.UserId == userId);
            foreach (var friend in friendList)
            {
                for (int i = 0; i < UsersIdList.Count(); i++)
                {
                    if (friend.FriendId == UsersIdList.ElementAt(i)) { 
                   // friendPositionList = friendPositionList.Concat()
                    }
                }
                    
            }*/
            return null;
        }
    }
}