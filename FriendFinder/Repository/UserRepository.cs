﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriendFinder.Models;

namespace FriendFinder.Repository
{
    public class UserRepository : IRepository<ApplicationUser>
    {
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
    }
}