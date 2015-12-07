using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FriendFinder.Repository;
using FriendFinder.Controllers;
using Moq;
using FriendFinder.Models;


namespace Test
{
    public class Tests
    {

        [TestFixture]
        public class TestTest
        {

            public static InvitationRepository invitationRepo = new InvitationRepository();
            public static UserRepository userRepository = new UserRepository();
            public static Mock<UserRepository> userRepoMock = new Mock<UserRepository>();
            public static Mock<InvitationRepository> invitationRepoMock = new Mock<InvitationRepository>();


            [Test]
            public void FindAllTest()
            {
               
                userRepoMock.Setup(x => x.FindAll());
                var users = userRepoMock.Object.FindAll().Count();
                Assert.AreEqual(users, 0);

            }

           /* [Test]
            public void GetFriendTest()
            {
                
            }
            */

        }
    }
}
