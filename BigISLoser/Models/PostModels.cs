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
    public class PostModel
    {
        public int PostId { get; set; }

        [Required]
        [DisplayName("Title")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Post Title must not exceed 50.")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Post")]
        public string PostContent { get; set; }

        [DisplayName("Post Date")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Modified Date")]
        public DateTime DateModified { get; set; }

        public int UserId { get; set; }

        [DisplayName("User")]
        public string UserName { get; set; }

        public bool DisplayName { get; set; }

        public List<CommentModel> Comments { get; set; }
    }

    public class CommentModel
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        [DisplayName("Post")]
        public string OriginalPost { get; set; }

        [DisplayName("Comment Date")]
        public DateTime CommentDate { get; set; }

        [DisplayName("Comment")]
        public string CommentContent { get; set; }

        public int UserId { get; set; }

        [DisplayName("User")]
        public string UserName { get; set; }

        public bool DisplayName { get; set; }
    }
}