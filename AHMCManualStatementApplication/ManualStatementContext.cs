using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AHMCManualStatementApplication
{
    public class ManualStatementContext : DbContext
    {
        public ManualStatementContext() : base("ManualStatementConnection")
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Facility> Facilities { get; set; }
    }
}
