using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyBGO.Framework.Models;
using System.Runtime.Remoting.Contexts;

namespace MyBGO.Framework.Core
{
    public class DottextCount : DbContext
    {
        public DottextCount() : base("name=conn")
        {
             
         }
        public DbSet<Admin> Admin { set; get; }//创建管理员表 
        public DbSet<User> user { set; get; }//创建用户表
        public DbSet<Blog> blog { set; get; }//创建博客表
        public DbSet<Catalog> catalog { set; get; }//创建栏目表
        public DbSet<Article> article { set; get; }//创建随笔表
        public DbSet<WebCatalog> webCatalog { set; get; }//创建全站分类表


    }
}   
