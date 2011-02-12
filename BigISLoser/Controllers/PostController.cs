using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigISLoser.Models;
using BigISLoser.ViewModels;
using BigISLoser.Repositories;

namespace BigISLoser.Controllers
{
    public class PostController : Controller
    {
        private PostRepositories _postRepository;
        private PostRepositories PostRepository
        {
            get { return _postRepository ?? (_postRepository = new PostRepositories()); }
        }

        private AccountRepositories _repository;
        private AccountRepositories AccountRepository { get { return _repository ?? (_repository = new AccountRepositories()); } }

        public ActionResult Index()
        {
            return View(new List<PostModel>(PostRepository.GetPosts()));
        }

        public ActionResult Post()
        {
            string userName = User.Identity.Name;
            AccountModel user = AccountRepository.GetAccount(userName);
            PostModel postModel = new PostModel();
            postModel.UserId = user.UserId;
            return View(postModel);
        }

        [HttpPost]
        public ActionResult Post(PostModel postModel)
        {
            PostRepository.InsertPost(postModel);
            return RedirectToAction("Index");
        }

        public ActionResult Comment(int id)
        {
            string userName = User.Identity.Name;
            AccountModel user = AccountRepository.GetAccount(userName);
            PostModel post = PostRepository.GetPost(id);
            CommentModel comment = new CommentModel();
            comment.UserId = user.UserId;
            comment.PostId = post.PostId;
            comment.OriginalPost = post.PostContent;
            return View(comment);
        }

        [HttpPost]
        public ActionResult Comment(CommentModel commentModel)
        {
            PostRepository.InsertComment(commentModel);
            return RedirectToAction("Index");
        }
    }
}
