using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyBlogPark.Models;

namespace MyBlogPark.Core
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
    }   
}