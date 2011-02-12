using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigISLoser.Models;

namespace BigISLoser.Repositories
{
    public class AccountRepositories
    {
        public bool RegisterAccount(RegisterModel model)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightUser user = new WeightUser();
                user.UserName = model.UserName;
                user.PW = model.Password;
                user.SignUpDateTime = DateTime.Now;
                entity.AddToWeightUsers(user);
                int count = entity.SaveChanges();
                if (count > 0)
                    return true;
            }
            return false;
        }

        public RegisterModel GetRegister(string userName)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightUser user = null;
                if (userName != null)
                    user = entity.WeightUsers.Where(e => e.UserName == userName).FirstOrDefault();
                else
                    return null;

                if (user != null)
                {
                    RegisterModel register = new RegisterModel()
                    {
                        UserName = user.UserName
                    };
                    return register;
                }

                return null;
            }
        }

        public bool LogOn(LogOnModel logOnModel)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var user = entity.WeightUsers.Where
                    (e => e.UserName == logOnModel.UserName && e.PW == logOnModel.Password).FirstOrDefault();

                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }

        public AccountModel GetAccount(string userName)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightUser user = null;
                if (userName != null)
                    user = entity.WeightUsers.Where(e => e.UserName == userName).FirstOrDefault();
                else
                    return null;

                if (user != null)
                {
                    AccountModel accountModel = new AccountModel();
                    accountModel.UserId = user.UserID;
                    accountModel.UserName = user.UserName;
                    accountModel.DisplayName = user.DisplayName;
                    accountModel.StartWeight = user.StartWeight;
                    accountModel.GoalWeight = user.GoalWeight;
                    accountModel.EndWeight = user.EndWeight;
                    accountModel.Paid = user.Paid;
                    accountModel.Admin = user.Admin;
                    return accountModel;
                }

                return null;
            }
        }

        public AccountModel GetAccount(int userId)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightUser user = null;
                if (userId != 0)
                    user = entity.WeightUsers.Where(e => e.UserID == userId).FirstOrDefault();
                else
                    return null;

                if (user != null)
                {
                    AccountModel accountModel = new AccountModel();
                    accountModel.UserId = user.UserID;
                    accountModel.UserName = user.UserName;
                    accountModel.DisplayName = user.DisplayName;
                    accountModel.StartWeight = user.StartWeight;
                    accountModel.GoalWeight = user.GoalWeight;
                    accountModel.EndWeight = user.EndWeight;
                    accountModel.Paid = user.Paid;
                    accountModel.Admin = user.Admin;
                    return accountModel;
                }

                return null;
            }
        }

        public List<AccountModel> GetAccounts()
        {
            List<AccountModel> accounts = new List<AccountModel>();
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var users = entity.WeightUsers.Select(e => e);

                foreach (WeightUser weightUser in users)
                {
                    AccountModel accountModel = new AccountModel();
                    accountModel.Admin = weightUser.Admin;
                    accountModel.DisplayName = weightUser.DisplayName;
                    accountModel.EndWeight = weightUser.EndWeight;
                    accountModel.GoalWeight = weightUser.GoalWeight;
                    accountModel.Paid = weightUser.Paid;
                    accountModel.StartWeight = weightUser.StartWeight;
                    accountModel.UserId = weightUser.UserID;
                    accountModel.UserName = weightUser.UserName;
                    accounts.Add(accountModel);
                }
            }
            return accounts;
        }

        public bool UpdateAccount(AccountModel accountModel)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightUser user = entity.WeightUsers.Where(e => e.UserID == accountModel.UserId).FirstOrDefault();

                if (user != null)
                {
                    //user.UserName = accountModel.UserName;
                    user.DisplayName = accountModel.DisplayName;
                    user.GoalWeight = accountModel.GoalWeight;
                    user.StartWeight = accountModel.StartWeight;
                    user.EndWeight = accountModel.EndWeight;
                    user.Admin = accountModel.Admin;
                    user.Paid = accountModel.Paid;
                    int count = entity.SaveChanges();
                    if (count > 0)
                        return true;
                }
            }
            return false;
        }
    }
}