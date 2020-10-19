using Homework20.AuthModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework20.Context
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) 
        {
            try
            {
                if (Database.EnsureCreated())
                {
                    List<Pers> tempList = JsonConvert.DeserializeObject<List<Pers>>(File.ReadAllText("pers.json"));
                    Characters.AddRange(tempList);
                    this.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public DbSet<Pers> Characters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
                DataBase=PersDB;
                Integrated Security=True");
        }
    }
}
