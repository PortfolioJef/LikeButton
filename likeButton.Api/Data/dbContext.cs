using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace likeButtonApi.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
       : base(options)
        {
        }

        #region DBSet

        public DbSet<Like> Like { get; set; }
        #endregion
    }
}