using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersApi.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Pers> Characters { get; set; }

        /// <summary>
        /// Конфигурационный метод подключения к БД
        /// </summary>
        /// <param name="optBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(
               @"Server=(localdb)\MSSQLLocalDB;
                  DataBase=PersDB;
                  Trusted_Connection=True;");
        }
    }
}
