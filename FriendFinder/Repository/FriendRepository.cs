using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriendFinder.Models;
namespace FriendFinder.Repository
{
    public class FriendRepository : IRepository<Friend>
    {
        private ApplicationDbContext context;

        public FriendRepository()
        {
            context = new ApplicationDbContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<Friend> GetLoggedFriends(string userId)
        {
            var friends = context.Friends.Where(f => f.UserId == userId);
            return friends;
        }

        public Friend Add(Friend friend)
        {
            context.Friends.Add(friend);
            return friend;
        }
    }
}