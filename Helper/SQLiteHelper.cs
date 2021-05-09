using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using WallPoster.Models;

namespace WallPoster.Helper
{
    public class SQLiteHelper<T> : DbContext
    {
        private static string dbPath = AppDomain.CurrentDomain.BaseDirectory + "data.db";

        private static readonly object LockObj = new();
        private static SQLiteHelper<T> helper = null;

        /// <summary>
        /// 单例模式双锁
        /// </summary>
        /// <returns></returns>
        public static SQLiteHelper<T> GetInstance()
        {
            if (helper == null)
            {
                lock (LockObj)
                {
                    if (helper == null)
                    {
                        helper = new SQLiteHelper<T>();
                    }
                }
            }
            return helper;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static SQLiteHelper()
        {
            GetInstance();
        }

        public SQLiteHelper() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = dbPath,
                    ForeignKeys = true,
                    Enlist = true,
                    Pooling = true,
                    CacheSize = 300
                }.ConnectionString
            }, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //如果不存在数据库，则创建 
            Database.SetInitializer(new SqliteDropCreateDatabaseWhenModelChanges<SQLiteHelper<T>>(modelBuilder));
        }

        public DbSet<PathModel> Paths { get; set; }

        public DbSet<FilesModel> Files { get; set; }

        public DbSet<AreaModel> Areas { get; set; }

        public DbSet<AdmModel> Adms { get; set; }

        /// <summary>  
        /// 分页查询 + 条件查询 + 排序  
        /// </summary>  
        /// <typeparam name="Tkey">泛型</typeparam>  
        /// <param name="pageSize">每页大小</param>  
        /// <param name="pageIndex">当前页码</param>  
        /// <param name="total">总数量</param>  
        /// <param name="whereLambda">查询条件</param>  
        /// <param name="orderbyLambda">排序条件</param>  
        /// <param name="isAsc">是否升序</param>  
        /// <returns>IQueryable 泛型集合</returns>  
        public IQueryable<T> LoadPageItems<T, TKey>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderbyLambda, bool isAsc) where T : class
        {

            total = helper.Set<T>().Where(whereLambda).Count();
            //倒序或升序
            if (isAsc)
            {
                var temp = helper.Set<T>().Where(whereLambda)
                             .OrderBy<T, TKey>(orderbyLambda)
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize);
                return temp.AsQueryable();
            }
            else
            {
                var temp = helper.Set<T>().Where(whereLambda)
                           .OrderByDescending<T, TKey>(orderbyLambda)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize);
                return temp.AsQueryable();
            }
        }

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Add<T>(T Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                helper.Set<T>().Add(Entity);
                int Count = helper.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 批量的插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool AddRange<T>(List<T> Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                helper.Set<T>().AddRange(Entity);
                int Count = helper.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 根据查询条件进行删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                var EntityModel = helper.Set<T>().Where(whereLambda).FirstOrDefault();
                if (EntityModel != null)
                {
                    helper.Set<T>().Remove(EntityModel);
                    int Count = helper.SaveChanges();
                    Ts.Complete();
                    return Count > 0;
                }
                return false;
            }
        }

        /// <summary>
        /// 删除单个对象的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity">实体对象</param>
        /// <returns></returns>
        public bool Delete<T>(T Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                helper.Set<T>().Attach(Entity);
                helper.Set<T>().Remove(Entity);
                int Count = helper.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 批量的进行更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool Update<T>(List<T> Entity) where T : class
        {
            int Count = 0;
            using (TransactionScope Ts = new TransactionScope(TransactionScopeOption.Required))
            {
                if (Entity != null)
                {
                    foreach (var items in Entity)
                    {
                        var EntityModel = helper.Entry(Entity);
                        helper.Set<T>().Attach(items);
                        EntityModel.State = EntityState.Modified;
                    }

                }
                Count = helper.SaveChanges();
                Ts.Complete();
            }

            return Count > 0;
        }

        /// <summary>
        /// 进行修改单个实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity">实体对象</param>
        /// <returns></returns>
        public bool Update<T>(T Entity) where T : class
        {
            using (TransactionScope Ts = new TransactionScope())
            {
                var EntityModel = helper.Entry<T>(Entity);
                helper.Set<T>().Attach(Entity);
                EntityModel.State = EntityState.Modified;
                int Count = helper.SaveChanges();
                Ts.Complete();
                return Count > 0;
            }
        }

        /// <summary>
        /// 查询条件进行修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="WhereLambda"></param>
        /// <param name="ModifiedProNames"></param>
        /// <returns></returns>
        public bool Update<T>(T model, Expression<Func<T, bool>> WhereLambda, params string[] ModifiedProNames) where T : class
        {
            //查询要修改的数据
            List<T> ListModifing = helper.Set<T>().Where(WhereLambda).ToList();
            Type t = typeof(T);
            List<PropertyInfo> ProInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            Dictionary<string, PropertyInfo> DitProList = new Dictionary<string, PropertyInfo>();
            ProInfos.ForEach(p =>
            {
                if (ModifiedProNames.Contains(p.Name))
                {
                    DitProList.Add(p.Name, p);
                }
            });

            if (DitProList.Count <= 0)
            {
                throw new Exception("指定修改的字段名称有误或为空");
            }
            foreach (var item in DitProList)
            {
                PropertyInfo proInfo = item.Value;
                object newValue = proInfo.GetValue(model, null);
                //批量进行修改相互对应的属性
                foreach (T oModel in ListModifing)
                {
                    proInfo.SetValue(oModel, newValue, null);//设置其中新的值
                }
            }

            return helper.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public T FindByID<T>(dynamic ID) where T : class
        {
            return helper.Set<T>().Find(ID) ?? null;
        }

        /// <summary>
        ///根据查询条件进行查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public List<T> GetAllQuery<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ? helper.Set<T>().Where(WhereLambda).ToList() ?? null : helper.Set<T>().ToList() ?? null;
        }

        /// <summary>
        ///判断对象是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public bool GetAny<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ? helper.Set<T>().Where(WhereLambda).Any() : helper.Set<T>().Any();
        }

        /// <summary>
        /// 获取查询条件的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public int GetCount<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ? helper.Set<T>().Where(WhereLambda).Count() : helper.Set<T>().Count();
        }

        /// <summary>
        /// 获取单条的记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public T GetFristDefault<T>(Expression<Func<T, bool>> WhereLambda = null) where T : class
        {
            return WhereLambda != null ? helper.Set<T>().Where(WhereLambda).FirstOrDefault() ?? null : helper.Set<T>().FirstOrDefault() ?? null;
        }

        /// <summary>
        /// 查询对象的转化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        public List<T> GetSelect<T>(Expression<Func<T, bool>> WhereLambda) where T : class
        {
            return helper.Set<T>().Where(WhereLambda).ToList() ?? null;
        }
    }
}
