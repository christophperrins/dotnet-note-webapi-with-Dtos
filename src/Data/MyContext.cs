using Microsoft.EntityFrameworkCore;
using src.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Data
{
    public class MyContext: DbContext
    {
        public MyContext (DbContextOptions<MyContext> options): base(options)
        {

        }

        public DbSet<Note> Note { get; set; }

    }
}
