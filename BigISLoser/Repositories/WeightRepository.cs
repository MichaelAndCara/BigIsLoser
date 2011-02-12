using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BigISLoser.Models;

namespace BigISLoser.Repositories
{
    public class WeightRepository
    {
        public List<PercentLostModel> GetTopStats()
        {
            List<PercentLostModel> percentList = new List<PercentLostModel>();
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var stats = entity.WeightCheckIns.Include("WeightUser").GroupBy(e => e.UserID)
                    .Select(g => g.OrderByDescending(e => e.CheckInDate).FirstOrDefault()).ToList();

                foreach (var item in stats)
                {
                    if (item.WeightUser.StartWeight != null && item.WeightUser.StartWeight != 0 &&
                        item.Weight != 0 && item.WeightUser.Paid > 0)
                    {
                        PercentLostModel model = new PercentLostModel();
                        model.User = item.WeightUser.DisplayName ? item.WeightUser.UserName : item.UserID.ToString();
                        model.CurrentWeight = Convert.ToDecimal(item.Weight);
                        model.StartingWeight = Convert.ToDecimal(item.WeightUser.StartWeight);
                        percentList.Add(model);
                    }
                }
                return percentList;
            }
        }

        public List<WeightCheckInModel> GetCheckInWeights(int userId)
        {
            List<WeightCheckInModel> weightList = new List<WeightCheckInModel>();
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var weights = entity.WeightCheckIns.Include("WeightUser").Where(e => e.UserID == userId);
                foreach (var weight in weights)
                {
                    WeightCheckInModel model = new WeightCheckInModel();
                    model.CheckInId = weight.CheckInID;
                    model.UserId = weight.UserID;
                    model.UserName = weight.WeightUser.UserName;
                    model.Weight = weight.Weight;
                    model.CheckInDate = weight.CheckInDate;
                    weightList.Add(model);
                }
            }
            return weightList;
        }

        public WeightCheckInModel GetCheckInWeight(int checkInId)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var weight = entity.WeightCheckIns.Include("WeightUser").Where(e => e.CheckInID == checkInId).FirstOrDefault();
                if (weight != null)
                {
                    WeightCheckInModel model = new WeightCheckInModel();
                    model.CheckInId = weight.CheckInID;
                    model.UserId = weight.UserID;
                    model.UserName = weight.WeightUser.UserName;
                    model.Weight = weight.Weight;
                    model.CheckInDate = weight.CheckInDate;
                    return model;
                }
            }
            return null;
        }

        public bool InsertWeightCheckIn(WeightCheckInModel model)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightCheckIn checkIn = new WeightCheckIn();
                checkIn.UserID = model.UserId;
                checkIn.Weight = model.Weight;
                checkIn.CheckInDate = model.CheckInDate;
                entity.AddToWeightCheckIns(checkIn);
                int count = entity.SaveChanges();
                if (count > 0)
                    return true;
            }
            return false;
        }

        public bool UpdateCheckIn(WeightCheckInModel model)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightCheckIn checkIn = entity.WeightCheckIns.Where(e => e.CheckInID == model.CheckInId).FirstOrDefault();

                if (checkIn != null)
                {
                    //user.UserName = accountModel.UserName;
                    checkIn.CheckInDate = model.CheckInDate;
                    checkIn.Weight = model.Weight;
                    int count = entity.SaveChanges();
                    if (count > 0)
                        return true;
                }
            }
            return false;
        }

        public bool DeleteCheckIn(int checkInId)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightCheckIn checkIn = entity.WeightCheckIns.Where(e => e.CheckInID == checkInId).FirstOrDefault();

                if (checkIn != null)
                {
                    entity.DeleteObject(checkIn);
                    int count = entity.SaveChanges();
                    if (count > 0)
                        return true;
                }
            }
            return false;
        }
    }

}