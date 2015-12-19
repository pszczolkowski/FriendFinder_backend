using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FriendFinder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendFinder.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        public static Mock<UserRepository> userRepoMock = new Mock<UserRepository>();
        [TestInitialize]
        public void SetUp()
        {
            //userRepoMock.Object.Save
        }
        [TestMethod()]
        public void UserRepositoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            //userRepoMock.When();
            Assert.Fail();
        }

        [TestMethod()]
        public void FindLoggedFriendsOfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindFriendsOfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindByUsernameTest()
        {
            Assert.Fail();
        }
    }
}