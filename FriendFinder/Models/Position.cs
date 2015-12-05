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
    
}