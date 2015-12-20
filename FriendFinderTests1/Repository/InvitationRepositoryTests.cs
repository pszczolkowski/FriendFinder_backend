using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendFinder.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendFinder.Models;

namespace FriendFinder.Tests
{
    [TestClass()]
    public class InvitationRepositoryTests
    { 
        public static Mock<InvitationRepository> invitationRepoMock = new Mock<InvitationRepository>();
        
       
       [TestMethod()]
        public void getByIdTest()
        {

           var appUser = new ApplicationUser()
            {
                UserName = "friend",
                Id = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            };
           var appUser2 = new ApplicationUser()
            {
                UserName = "friend2",
                Id = "bb022461-cc1e-4176-b094-0f5376490f22",
                Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

            };
           var invitation = new Invitation()
           {
               Date = DateTime.Now,
               InvitationId = 23,
               InvitedId = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               InvitedUser = appUser,
               InvitingId = "bb022461-cc1e-4176-b094-0f5376490f22",
               InvitingUser = appUser2
           };
           invitationRepoMock.Setup(i => i.getById(23)).Returns(invitation);

           var result = invitationRepoMock.Object.getById(23);
           Assert.AreEqual(result, invitation);
        }

       [TestMethod()]
       public void getInvitationsTest()
       {
           var appUser = new ApplicationUser()
           {
               UserName = "friend",
               Id = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
           var appUser2 = new ApplicationUser()
           {
               UserName = "friend2",
               Id = "bb022461-cc1e-4176-b094-0f5376490f22",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
           var appUser3 = new ApplicationUser()
           {
               UserName = "friend3",
               Id = "db022461-cc1e-4176-b094-0f5376490f22",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
           List<Invitation> invitations = new List<Invitation>();
           invitations.Add(new Invitation()
           {
               Date = DateTime.Now,
               InvitationId = 23,
               InvitedId = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               InvitedUser = appUser,
               InvitingId = "bb022461-cc1e-4176-b094-0f5376490f22",
               InvitingUser = appUser2
           });
           invitations.Add(new Invitation()
           {
               Date = DateTime.Now,
               InvitationId = 22,
               InvitedId = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               InvitedUser = appUser,
               InvitingId = "db022461-cc1e-4176-b094-0f5376490f22",
               InvitingUser = appUser3
           });
          
           var result = invitationRepoMock.Object.getInvitations("239f4fae-ff76-4c80-b86c-b56666f4ac2e");
           Assert.AreEqual(2, invitations.Count);
           Assert.IsNotNull(invitations);
       }
        
       [TestMethod()]
       public void getForUsersTest()
       {
           var appUser = new ApplicationUser()
           {
               UserName = "friend",
               Id = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
           var appUser2 = new ApplicationUser()
           {
               UserName = "friend2",
               Id = "bb022461-cc1e-4176-b094-0f5376490f22",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
            var invitation = new Invitation()
           {
               Date = DateTime.Now,
               InvitationId = 23,
               InvitedId = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               InvitedUser = appUser,
               InvitingId = "bb022461-cc1e-4176-b094-0f5376490f22",
               InvitingUser = appUser2
           };
           invitationRepoMock.Setup(i => i.getForUsers("239f4fae-ff76-4c80-b86c-b56666f4ac2e", "bb022461-cc1e-4176-b094-0f5376490f22")).Returns(invitation);
           var result = invitationRepoMock.Object.getForUsers("239f4fae-ff76-4c80-b86c-b56666f4ac2e", "bb022461-cc1e-4176-b094-0f5376490f22");
           Assert.AreEqual(result, invitation);
           Assert.IsNotNull(result);
           
       }

       [TestMethod()]
       public void AddTest()
       {
           var appUser = new ApplicationUser()
           {
               UserName = "friend",
               Id = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
           var appUser2 = new ApplicationUser()
           {
               UserName = "friend2",
               Id = "bb022461-cc1e-4176-b094-0f5376490f22",
               Position = new Position() { Longitude = 16.9, Latitude = 54.6, LastUpdate = DateTime.Now },

           };
            var invitation = new Invitation()
           {
               Date = DateTime.Now,
               InvitationId = 23,
               InvitedId = "239f4fae-ff76-4c80-b86c-b56666f4ac2e",
               InvitedUser = appUser,
               InvitingId = "bb022461-cc1e-4176-b094-0f5376490f22",
               InvitingUser = appUser2
           };
            invitationRepoMock.Setup(i => i.Add(invitation)).Returns(invitation);
            var result = invitationRepoMock.Object.Add(invitation);
            Assert.AreEqual(result, invitation);
            Assert.IsNotNull(result);
       }
     
       
    }
}