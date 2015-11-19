using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriendFinder.Models;

namespace FriendFinder.Repository
{
    public class PositionRepository : IRepository<Position>
    {
        private ApplicationDbContext context;

        public PositionRepository()
         {
           context = new ApplicationDbContext();
         }

        public void Save()
        {
            context.SaveChanges();
        }

        public Position Add(Position position)
        {
            context.Positions.Add(position);
            return position;
        }
    }
}