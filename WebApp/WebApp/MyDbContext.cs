using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<SessionRec> SessionRecs { get; set; }
        public DbSet<SpeakerRec> SpeakerRecs { get; set; }
    }
}
