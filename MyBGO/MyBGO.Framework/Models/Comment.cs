using System;
using System.ComponentModel.DataAnnotations;

namespace MyBGO.Framework.Models
{
    public class Comment
    {
        [Key]
        public int ID { set; get; }
        [Required]
        [StringLength(400)]
        public string Contents { set; get; }//评论内容
        [Required]
        public int ArticleID { set; get; }//博文ID
        public string ArticleTitle { set; get; }//博文标题
        public int UserID { set; get; }//评论用户id(表示评论人的id，通过该id我们可以检索到评论人的相关信息。)
        public DateTime AddTime { set; get; }
        public int To_UserID { set; get; }
        public virtual User User { set; get; }

    }
}
