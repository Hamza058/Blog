﻿using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
	public class Context : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.UseSqlServer("server=HAMZA\\SQLEXPRESS; database=DbBlog;integrated security=true;");
        }

		public DbSet<Admin>? Admins { get; set; }
		public DbSet<Writing>? Writings { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<About>? Abouts { get; set; }
    }
}
