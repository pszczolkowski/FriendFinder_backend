using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FriendFinder.Models;
using FriendFinder.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using FriendFinder.Repository;
using System;
using System.Collections.Generic;

namespace FriendFinder.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        public static Mock<UserRepository> userRepoMock = new Mock<UserRepository>();

        [TestMethod()]
        public void FindByIdTest()
        {
            var appUser = new ApplicationUser()
            {
                UserName = "friend",
                Id = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            };
            userRepoMock.Setup(x => x.FindById("239f4fae-ff76-4c80-b86c-b56666f4ac2e")).Returns(appUser);
            var user = userRepoMock.Object.FindById("239f4fae-ff76-4c80-b86c-b56666f4ac2e");
            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void FindAllTest()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users.Add(new ApplicationUser()
            {
                UserName = "friend",
                Id = "2813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            });
            users.Add(new ApplicationUser()
            {
                UserName = "friend2",
                Id = "4813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            });
            users.Add(new ApplicationUser()
            {
                UserName = "friend3",
                Id = "1813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            });
            var userFriends = userRepoMock.Object.FindAll();
            Assert.AreEqual(3, users.Count);
            Assert.IsNotNull(userFriends);
        }

        [TestMethod()]
        public void FindLoggedFriendsOfTest()
        {

            List<ApplicationUser> friends = new List<ApplicationUser>();
            friends.Add(new ApplicationUser()
            {
                UserName = "friend",
                Id = "2813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            });
            userRepoMock.Setup(f => f.FindLoggedFriendsOf("239f4fae-ff76-4c80-b86c-b56666f4ac2e")).Returns(friends);
            var result = userRepoMock.Object.FindLoggedFriendsOf("239f4fae-ff76-4c80-b86c-b56666f4ac2e");
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void FindFriendsOfTest()
        {
            List<ApplicationUser> friends = new List<ApplicationUser>();
            var user2 = new ApplicationUser()
            {
                UserName = "friend2",
                Id = "2813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },
               
            };
            friends.Add(user2);
            userRepoMock.Setup(f => f.FindFriendsOf("8813d05d-5e44-4af6-bcd4-b13ca4095834")).Returns(friends);
            var result = userRepoMock.Object.FindFriendsOf("8813d05d-5e44-4af6-bcd4-b13ca4095834");
            Assert.AreEqual(friends, result);
            Assert.IsNotNull(result);
                     
        }

        [TestMethod()]
        public void FindByUsernameTest()
        {
            var userName = "friend";
            List<ApplicationUser> friends = new List<ApplicationUser>();
           userRepoMock.Setup(f => f.FindByUsername("friend")).Returns(new ApplicationUser()
            {
                UserName = "friend",
                Id = "8813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },
                Friends = friends
            });
            var result = userRepoMock.Object.FindByUsername("friend").UserName;
            Assert.AreEqual(userName, result);
           
        }

        public List<ApplicationUser> GetUserTest()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users.Add(new ApplicationUser(){
                UserName = "friend",
                Id = "2813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },
               
            });
            users.Add(new ApplicationUser()
            {
                UserName = "friend2",
                Id = "3813d05d-5e44-4af6-bcd4-b13ca4095834",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            });
            return users;
        }
    }
}