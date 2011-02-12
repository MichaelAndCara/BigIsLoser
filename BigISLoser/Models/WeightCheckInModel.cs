using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace BigISLoser.Models
{
    public class WeightCheckInModel
    {
        [DisplayName("Check In Id")]
        public int CheckInId { get; set; }
        
        [DisplayName("User Id")]
        public int UserId { get; set; }

        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Weight")]
        [Required]
        [Range(80, 500)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Must be a whole number")]
        public int Weight { get; set; }

        [DisplayName("Check In Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
    }
}