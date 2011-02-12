using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BigISLoser.Models;
using BigISLoser.Repositories;

namespace BigISLoser.Controllers
{
    public class AccountController : Controller
    {
        private IFormsAuthenticationService _formsService;
        public IFormsAuthenticationService FormsService 
        { get { return _formsService ?? (_formsService = new FormsAuthenticationService()); } }

        private AccountRepositories _repository;
        private AccountRepositories AccountRepository { get { return _repository ?? (_repository = new AccountRepositories()); } }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel logOnModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                logOnModel.Password = Convert.ToBase64String(
                    new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(
                        Encoding.ASCII.GetBytes(logOnModel.Password)));

                if (AccountRepository.LogOn(logOnModel))
                {
                    FormsService.SignIn(logOnModel.UserName, logOnModel.RememberMe);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(new LogOnViewModel(logOnModel));

            return View(new LogOnModel());
        }

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //check password match
                if (!String.Equals(registerModel.Password, registerModel.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Password and confirm password does not match");
                    return View(registerModel);
                }

                //check if username already exists
                RegisterModel registeredUsername = AccountRepository.GetRegister(userName: registerModel.UserName);
                if (registeredUsername != null)
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View(registerModel);
                }

                ////check if email already exists
                //RegisterModel registeredEmail = AccountRepository.GetRegister(email: registerModel.Email);
                //if (registeredEmail != null)
                //{
                //    ModelState.AddModelError("", "A username for that e-mail address already exists");
                //    return View(registerModel);
                //}

                //encrypt password
                registerModel.Password = Convert.ToBase64String(
                    new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(
                        Encoding.ASCII.GetBytes(registerModel.Password)));

                if (!AccountRepository.RegisterAccount(registerModel))
                {
                    ModelState.AddModelError("", "Oh no!  An unknown error has been thrown.  Please try again later.");
                    return View(registerModel);
                }
                else
                {
                    FormsService.SignIn(registerModel.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(registerModel);
        }

        public ActionResult MyAccount()
        {
            string user = HttpContext.User.Identity.Name;
            return View(AccountRepository.GetAccount(user));
        }

        [HttpPost]
        public ActionResult MyAccount(AccountModel accountModel)
        {
            if (accountModel.UserId != 0)
            {
                AccountRepository.UpdateAccount(accountModel);
            }

            return RedirectToAction("MyAccount");
        }

        public ActionResult AdminWeights()
        {
            List<AccountModel> accounts = AccountRepository.GetAccounts().Where(e => e.UserId != 1063).ToList();
            return View(accounts); ;
        }

        public ActionResult Edit(int id)
        {
            AccountModel model = AccountRepository.GetAccount(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountModel accountModel)
        {
            if (accountModel.UserId != 0)
            {
                AccountRepository.UpdateAccount(accountModel);
            }
            return RedirectToAction("AdminWeights");
        }
    }
}
