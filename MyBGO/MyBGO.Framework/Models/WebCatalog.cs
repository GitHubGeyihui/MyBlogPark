using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBGO.Framework.Models
{
    public class WebCatalog
    {
        [Key]
        public int ID { set; get; }
        public int PID { set; get; }//记录副级
        [Required]
        [StringLength(20)]
        public string Name { set; get; }
        public int Refleshs{ set; get; }//更新的数量
        public int Total { set; get; }//总的数量
        public DateTime AddTime { set; get; }
        public DateTime EditTime { set; get; }
        public bool Status { set; get; }
    }
}
