using Microsoft.EntityFrameworkCore;
using MyOwnIdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Context
{
    public class IdentityAppContext:DbContext
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options)
          : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
