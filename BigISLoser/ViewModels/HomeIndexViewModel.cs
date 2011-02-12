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
using BigISLoser.Models;
using BigISLoser.Repositories;

namespace BigISLoser.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<AccountModel> AccountsList { get; private set; }

        public List<PercentLostModel> PercentLostList { get; set; }
        
        [DisplayName("Total Entries")]
        public int UsersCount { get; private set; }
        
        [DisplayName("Entries Total")]
        public string EntriesPot { get; private set; }

        [DisplayName("First Prize")]
        public string FirstPrize { get; private set; }

        [DisplayName("Second Prize")]
        public string SecondPrize { get; private set; }

        [DisplayName("Third Prize")]
        public string ThirdPrize { get; private set; }

        [DisplayName("Potential Entries Total")]
        public string PotentialEntriesPot { get; private set; }

        [DisplayName("Potential First Prize")]
        public string PotentialFirstPrize { get; private set; }

        [DisplayName("Potential Second Prize")]
        public string PotentialSecondPrize { get; private set; }

        [DisplayName("Potential Third Prize")]
        public string PotentialThirdPrize { get; private set; }

        public HomeIndexViewModel()
        {
            AccountRepositories repositories = new AccountRepositories();
            AccountsList = repositories.GetAccounts();
            UsersCount = AccountsList.Count() - 1;
            EntriesPot = String.Format("{0:C}",AccountsList.Sum(e => e.Paid));
            FirstPrize = String.Format("{0:C}",AccountsList.Sum(e => e.Paid * 0.5m));
            SecondPrize = String.Format("{0:C}",AccountsList.Sum(e => e.Paid * 0.35m));
            ThirdPrize = String.Format("{0:C}",AccountsList.Sum(e => e.Paid * 0.15m));

            PotentialEntriesPot = String.Format("{0:C}", UsersCount * 20m);
            PotentialFirstPrize = String.Format("{0:C}", UsersCount * 20 * 0.5m);
            PotentialSecondPrize = String.Format("{0:C}", UsersCount * 20 * 0.35m);
            PotentialThirdPrize = String.Format("{0:C}", UsersCount * 20 * 0.15m);

            WeightRepository repository = new WeightRepository();
            PercentLostList = new List<PercentLostModel>(repository.GetTopStats().OrderByDescending(e => e.PercentLost).ToList());
           // return PartialView(stats);
        }
    }
}