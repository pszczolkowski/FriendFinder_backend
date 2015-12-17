using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FriendFinder.Models;


using FriendFinder.Repository;
using System;
using System.Collections.Generic;

namespace FriendFinder.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        private static Mock<UserRepository> userRepoMock = new Mock<UserRepository>();

        [TestMethod()]
        public void FindByIdTest()
        {
            var appUser = new ApplicationUser();
            userRepoMock.Setup(x => x.FindById("239f4fae-ff76-4c80-b86c-b56666f4ac2e"));
            var user = userRepoMock.Object.FindById("239f4fae-ff76-4c80-b86c-b56666f4ac2e");
            Assert.IsNull(user);
        }

        [TestMethod()]
        public void FindAllTest()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            var user = new ApplicationUser();
            var user2 = new ApplicationUser();
            var user3 = new ApplicationUser();
            users.Add(user);
            users.Add(user2);
            users.Add(user3);
            var userFriends = userRepoMock.Object.FindAll();
            Assert.AreEqual(3, users.Count);

        }

        [TestMethod()]
        public void FindLoggedFriendsOfTest()
        {
            var user2 = new ApplicationUser();
            List<ApplicationUser> friends = new List<ApplicationUser>();
            friends.Add(user2);
            var user = new ApplicationUser() { Friends = friends, Position = new Position() { LastUpdate = DateTime.Now, Latitude = 52.1, Longitude = 52.1 } };
            var userFriends = userRepoMock.Object.FindLoggedFriendsOf("239f4fae-ff76-4c80-b86c-b56666f4ac2e");
            Assert.IsNotNull(userFriends);
            Assert.Equals(friends, userFriends);
        }

        [TestMethod()]
        public void FindFriendsOfTest()
        {
            var user2 = new ApplicationUser();
            List<ApplicationUser> friends = new List<ApplicationUser>();
            friends.Add(user2);
            var user = new ApplicationUser() { Friends = friends, Position = new Position() { LastUpdate = DateTime.Now, Latitude = 52.1, Longitude = 52.1 } };
            var userFriends = userRepoMock.Object.FindFriendsOf("239f4fae-ff76-4c80-b86c-b56666f4ac2e");
            Assert.IsNotNull(userFriends);
            Assert.Equals(friends, userFriends);
        }

        [TestMethod()]
        public void FindByUsernameTest()
        {
            var friends = new List<ApplicationUser>();
            var user = new ApplicationUser() {Friends = friends, Position = new Position() {LastUpdate = DateTime.Now, Latitude = 52.1, Longitude = 52.1 } };
            var mockedUsername = userRepoMock.Object.FindByUsername("mockedUsername");
            Assert.IsNotNull(mockedUsername);
            Assert.AreSame(user, mockedUsername);
        }
    }
}