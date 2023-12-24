using Microsoft.EntityFrameworkCore;
using RegisterAndLoginAPI_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAndLoginAPI_DAL.Data
{
    public class Context:DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options):base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
