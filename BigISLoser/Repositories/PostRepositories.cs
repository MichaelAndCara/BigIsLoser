using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BigISLoser.Models;

namespace BigISLoser.Repositories
{
    public class PostRepositories
    {
        public List<PostModel> GetPosts()
        {
            List<PostModel> postList = new List<PostModel>();
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var posts = entity.WeightMsg_Post.Include("WeightUser").Select(e => e).OrderByDescending(f => f.DateCreated);

                foreach (WeightMsg_Post post in posts)
                {
                    PostModel postModel = new PostModel();
                    postModel.PostId = post.Post_ID;
                    postModel.DateCreated = post.DateCreated;
                    postModel.DateModified = post.DateModified;
                    postModel.DisplayName = post.WeightUser.DisplayName;
                    postModel.PostContent = post.Post_Content;
                    postModel.Title = post.Title;
                    postModel.UserId = post.UserID;
                    postModel.UserName = post.WeightUser.UserName;
                    postModel.Comments = this.GetComments(postModel.PostId);
                    postList.Add(postModel);
                }
            }
            return postList;
        }

        public PostModel GetPost(int postId)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var post = entity.WeightMsg_Post.Include("WeightUser").Where(e => e.Post_ID == postId).FirstOrDefault();
                PostModel postModel = new PostModel();
                postModel.PostId = post.Post_ID;
                postModel.DateCreated = post.DateCreated;
                postModel.DateModified = post.DateModified;
                postModel.DisplayName = post.WeightUser.DisplayName;
                postModel.PostContent = post.Post_Content;
                postModel.Title = post.Title;
                postModel.UserId = post.UserID;
                postModel.UserName = post.WeightUser.UserName;
                postModel.Comments = this.GetComments(postModel.PostId);
                return postModel;
            }
        }

        public List<CommentModel> GetComments(int postId)
        {
            List<CommentModel> commentList = new List<CommentModel>();
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                var comments = entity.WeightMsg_Comment.Include("WeightMsg_Post").Include("WeightUser").Where(e => e.Post_ID == postId).OrderBy(f => f.Comment_DateTime);

                foreach (WeightMsg_Comment comment in comments)
                {
                    CommentModel commentModel = new CommentModel();
                    commentModel.CommentId = comment.Comment_ID;
                    commentModel.PostId = comment.Post_ID;
                    commentModel.CommentDate = comment.Comment_DateTime;
                    commentModel.DisplayName = comment.WeightUser.DisplayName;
                    commentModel.CommentContent = comment.Comment_HTML;
                    commentModel.UserId = comment.UserID;
                    commentModel.UserName = comment.WeightUser.UserName;
                    commentModel.OriginalPost = comment.WeightMsg_Post.Post_Content;
                    commentList.Add(commentModel);
                }
            }
            return commentList;
        }

        public bool InsertPost(PostModel postModel)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightMsg_Post post = new WeightMsg_Post();
                post.DateCreated = DateTime.Now;
                post.DateModified = DateTime.Now;
                post.Post_Content = postModel.PostContent;
                post.Title = postModel.Title;
                post.UserID = postModel.UserId;
                entity.AddToWeightMsg_Post(post);
                int rows = entity.SaveChanges();
                if (rows > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool InsertComment(CommentModel commentModel)
        {
            using (Entities entity = new Entities(BaseBISL.ConnectionString))
            {
                WeightMsg_Comment comment = new WeightMsg_Comment();
                comment.Comment_DateTime = DateTime.Now;
                comment.Comment_HTML = commentModel.CommentContent;
                comment.Post_ID = commentModel.PostId;
                comment.UserID = commentModel.UserId;
                entity.AddToWeightMsg_Comment(comment);
                int rows = entity.SaveChanges();
                if (rows > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}