﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendFinder.Models
{
    public class Position
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public double Longitude {get; set;}
        public double Latitude { get; set; }
    }

    public class Friend
    {
        [Key]
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public string FriendUserName { get; set; }
    }

    public class FriendPosition
    {
        [Key]
        [Column(Order = 1)]
        public string FriendId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string FriendUserName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class PositionWithDistance
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Distance { get; set; }
    }
    public class Invitation
    {
        [Key]
        public int InvitationId { get; set; }
        public string UserId { get; set; }
        public string InviterId { get; set; }
        public DateTime Date { get; set; }
    }
}