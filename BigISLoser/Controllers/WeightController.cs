using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigISLoser.Repositories;
using BigISLoser.Models;

namespace BigISLoser.Controllers
{
    public class WeightController : Controller
    {
        private WeightRepository _weightRepository;
        private WeightRepository WeightRepository { get { return _weightRepository ?? (_weightRepository = new WeightRepository()); } }

        private AccountRepositories _accountRepository;
        private AccountRepositories AccountRepository { get { return _accountRepository ?? (_accountRepository = new AccountRepositories()); } }

        public ActionResult WeightCheckIns()
        {
            string userName = User.Identity.Name;
            AccountModel user = AccountRepository.GetAccount(userName);
            return View(WeightRepository.GetCheckInWeights(user.UserId).OrderBy(e => e.CheckInDate));
        }

        public ActionResult CheckIn()
        {
            //string userName = User.Identity.Name;
            //AccountModel user = AccountRepository.GetAccount(userName);
            //AccountModel user = AccountRepository.GetAccount(id);
            //WeightCheckInModel checkIn = new WeightCheckInModel();
            //checkIn.UserId = id;
            //checkIn.UserName = user.UserName;
            return View();
        }

        [HttpPost]
        public ActionResult CheckIn(WeightCheckInModel weightCheckInModel)
        {
            string userName = User.Identity.Name;
            AccountModel user = AccountRepository.GetAccount(userName);
            weightCheckInModel.UserId = user.UserId;
            WeightRepository.InsertWeightCheckIn(weightCheckInModel);
            return RedirectToAction("WeightCheckIns");
        }

        public ActionResult Edit(int id)
        {
            WeightCheckInModel model = WeightRepository.GetCheckInWeight(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(WeightCheckInModel model)
        {
            WeightRepository.UpdateCheckIn(model);
            return RedirectToAction("WeightCheckIns");
        }

        public ActionResult Delete(int id)
        {
            WeightRepository.DeleteCheckIn(id);
            return RedirectToAction("WeightCheckIns");
            //WeightCheckInModel model = WeightRepository.GetCheckInWeight(id);
            //return View(model);
        }

        //[HttpPost]
        //public ActionResult Delete(WeightCheckInModel model)
        //{
        //    WeightRepository.DeleteCheckIn(model.CheckInIdid);
        //    return RedirectToAction("WeightCheckIns");
        //}

        public ActionResult Stats()
        {
            string userName = User.Identity.Name;
            AccountModel user = AccountRepository.GetAccount(userName);
            List<WeightCheckInModel> checkIns = WeightRepository.GetCheckInWeights(user.UserId).OrderByDescending(e => e.CheckInDate).ToList();
            if (checkIns.Count > 0)
            {
                StatsModel stats = new StatsModel();
                stats.StartingWeight = Convert.ToDecimal(user.StartWeight);
                stats.GoalWeight = Convert.ToDecimal(user.GoalWeight);
                stats.CurrentWeight = checkIns[0].Weight;
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        
        
    }
}
