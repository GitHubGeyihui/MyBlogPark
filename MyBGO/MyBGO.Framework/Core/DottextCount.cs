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
        public DbSet<User> User { set; get; }//创建用户表
        public DbSet<Blog> Blog { set; get; }//创建博客表
        public DbSet<Catalog> Catalog { set; get; }//创建栏目表
        public DbSet<Article> Article { set; get; }//创建随笔表
        public DbSet<WebCatalog> WebCatalog { set; get; }//创建全站分类表


    }
}   
