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
    public class PercentLostModel
    {
        [DisplayName("User")]
        public string User { get; set; }

        [DisplayName("Starting Weight")]
        public decimal StartingWeight { get; set; }

        [DisplayName("Current Weight")]
        public decimal CurrentWeight { get; set; }

        [DisplayName("Percent Lost")]
        public decimal PercentLost
        {
            get
            {
                return PoundsLost / StartingWeight * 100;
            }
        }

        [DisplayName("Pounds Lost")]
        public decimal PoundsLost
        {
            get
            {
                return StartingWeight - CurrentWeight;
            }
        }
    }
}