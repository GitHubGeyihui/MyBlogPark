using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBGO.Framework.Core
{
    public class EFInit
    {
        public static void Setting()
        {
            using (var dottextCount = new DottextCount())
            {
                //数据库不存在则创建
                var res = dottextCount.Database.CreateIfNotExists();
            } 
        }
    }
}
