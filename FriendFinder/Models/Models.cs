using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendFinder.Models
{
	[ComplexType]
    public class Position
    {
        public double Longitude {get; set;}
        public double Latitude { get; set; }
		public DateTime LastUpdate { get; set; }
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
        public string UserId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class PositionWithDistance
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Distance { get; set; }
    }
    
}