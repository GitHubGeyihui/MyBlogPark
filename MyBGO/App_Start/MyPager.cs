using MyBGO.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyBGO.App_Start
{
    public class MyPager
    {
        //定义一些变量
        int total = 0;      //总数
        int pageSize = 10;  //每页显示几条
        int pageCurr = 1;   //当前页
        int Offset = 5;     //当前页两边偏移几页
        string arg = string.Empty; //代表其他的参数（在地址栏里传送的）

        //利用构造方法接收用户传来的数据
        public MyPager(int total = 0, int pageCurr = 1, int pageSize = 10, int offSet = 5)
        {
            this.total = total;
            this.pageSize = pageSize;
            this.pageCurr = pageCurr;
            this.Offset = offSet;
        }
        public MyPager(int total = 0, int pageCurr = 1, int pageSize = 10, int offSet = 5, string arg = "")
        {
            this.total = total;
            this.pageSize = pageSize;
            this.pageCurr = pageCurr;
            this.Offset = offSet;
            this.arg = arg;
        }

        public string ToHTML()
        {
            //对用户传来的数据做一次校正
            if (pageSize < 1) { pageSize = 10; }
            if (total < 0) { total = 0; }
            if (pageCurr < 1) { pageCurr = 1; }
            if (Offset < 1) { Offset = 5; }

            //做一些运算，返回分页信息字符串
            double rs = (double)total / (double)pageSize;
            int pageCount = (int)Math.Ceiling(rs);
            //有了页面总数，再校验当前页是否正确
            if (pageCurr > pageCount) { pageCurr = pageCount; }
            //生成页码范围（从多少开始，到多少结束）
            int start = pageCurr - Offset;
            int end = pageCurr + Offset;
            //再做一次校验
            if (start < 1) { start = 1; }
            if (end > pageCount) { end = pageCount; }
            //用循环语句产生页码范围


            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='pagination'>");
            if (!string.IsNullOrEmpty(arg)) { arg = "&" + arg; }

            int prev = pageCurr <= 1 ? 1 : pageCurr - 1;

            sb.AppendFormat("<li class='page-item'><a class='page-link' href='?page={0}&key={1}'><</a></li>", prev, arg);
            for (int i = start; i <= end; i++)
            {
                if (i == pageCurr)
                {
                    sb.AppendFormat("<li class='page-item active'><a class='page-link' href='?page={0}{1}'>{0}</a></li>", i, arg);
                }
                else
                {
                    sb.AppendFormat("<li class='page-item'><a class='page-link' href='?page={0}{1}'>{0}</a></li>", i, arg);
                }
            }
            int next = pageCurr >= pageCount ? pageCount : pageCurr + 1;
            sb.AppendFormat("<li class='page-item'><a class='page-link' href='?page={0}&key={1}'>></a></li>", next, arg);
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}