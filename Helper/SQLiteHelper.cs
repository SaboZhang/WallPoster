using SQLite.CodeFirst;
using System;
using System.Data.Entity;
using System.Data.SQLite;
using WallPoster.Models;

namespace WallPoster.Helper
{
    public class SQLiteHelper : DbContext
    {
        private static string dbPath = AppDomain.CurrentDomain.BaseDirectory + "data.db";

        public SQLiteHelper() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = dbPath,
                    ForeignKeys = true
                }.ConnectionString
            }, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //如果不存在数据库，则创建 
            Database.SetInitializer(new SqliteDropCreateDatabaseWhenModelChanges<SQLiteHelper>(modelBuilder));
        }

        public DbSet<PathModel> Paths { get; set; }

        public DbSet<FilesModel> Files { get; set; }

    }
}
