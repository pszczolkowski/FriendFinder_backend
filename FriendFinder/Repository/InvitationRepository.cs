using FriendFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FriendFinder.Repository
{
    public class InvitationRepository : IRepository<Invitation>
    {

        private ApplicationDbContext context;

        public InvitationRepository()
        {
            context = new ApplicationDbContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Invitation getById(int id)
        {
            var invitation = context.Invitations.Where(i => i.InvitationId == id).FirstOrDefault();
            return invitation;
        }

        public IEnumerable<Invitation> getInvitations(string UserId)
        {
            var invitations = context.Invitations.Where(i => i.UserId == UserId);
            return invitations;
        }

        public Invitation Add(Invitation invitation)
        {
            context.Invitations.Add(invitation);
            return invitation;
        }

        public void Delete(Invitation invitation)
        {
            context.Invitations.Remove(invitation);
        }

    }
}