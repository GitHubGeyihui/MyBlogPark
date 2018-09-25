using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlogPark.Areas.Admin.ViewModels
{
    public class BlogApply
    { 
        [Required]
        [StringLength(20)]
        [DisplayName("博主昵称")]
        public string NiName { set; get; }

        [Required]
        [StringLength(20)]
        [DisplayName("博客标识符")]
        public string Identity { set; get; }

        [StringLength(100)]
        [DisplayName("博客标题")]

        public string Title { set; get; }
        [StringLength(100)]
        [DisplayName("签名")]

        public string Signal { set; get; }//签名
        [StringLength(250)]

        [DisplayName("博客描述")]

        public string Discription { set; get; }//博客描述
        [DisplayName("皮肤")]

        public int ThemeID { set; get; }//皮肤，主题ID

    }
}