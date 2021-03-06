﻿using System;
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
    public class StatsModel
    {
        [DisplayName("Total days left of the competition")]
        public int DaysLeft { get { return (new DateTime(2011, 4, 29) - DateTime.Today).Days; } }

        [DisplayName("Total weeks left of the competition")]
        public int WeeksLeft { get { return Convert.ToInt32(Math.Ceiling((double)DaysLeft / 7)); } }

        [DisplayName("User")]
        public string User { get; set; }

        [DisplayName("Starting Weight")]
        public decimal StartingWeight { get; set; }
        
        [DisplayName("Goal Weight")]
        public decimal GoalWeight { get; set; }

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

        [DisplayName("Weight to meet goal")]
        public decimal WeightLeft
        {
            get
            {
                return StartingWeight - PoundsLost - GoalWeight;
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

        [DisplayName("Weight per week to meet goal")]
        public decimal PerWeek 
        { 
            get 
            {
                if (WeeksLeft > 0)
                {
                    return (WeightLeft / WeeksLeft);
                }
                else
                {
                    return 0;
                }
            } 
        }
    }
}