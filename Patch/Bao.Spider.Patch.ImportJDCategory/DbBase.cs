using System.Collections.Generic;
using NPoco;
using NPoco.Linq;

namespace Bao.Spider.Patch.ImportJDCategory
{
    public class DbBase<T> where T : class, new()
    {
        #region ctor
        readonly IDatabase _db;
        private static readonly Dictionary<string, IDatabase> dict = new Dictionary<string, IDatabase>();

        public IDatabase Db
        {
            get
            {
                return _db;
            }
        }

        public DbBase(string conn)
        {
            if (dict.ContainsKey(conn))
                _db = dict[conn];
            else
            {
                _db = new Database(conn).OpenSharedConnection();
                dict.Add(conn, _db);
            }
        }
        #endregion

        #region TableInfo
        public TableInfo TableInfo
        {
            get
            {
                //return Db.DefaultMapper.GetTableInfo(t.GetType());
                return TableInfo.FromPoco(new T().GetTheType());
            }
        }
        #endregion

        #region exists & is new
        public bool IsExists(object priaryKey)
        {
            return _db.Exists<T>(priaryKey);
        }
        public bool IsNew(T t)
        {
            return _db.IsNew(t);
        }
        #endregion

        #region get
        public T Get(int id)
        {
            if (id <= 0)
                return default(T);

            return _db.SingleById<T>(id);
        }

        public T Get(string sql, params object[] args)
        {
            return _db.SingleOrDefault<T>(sql, args);
        }
        #endregion

        #region query
        public IQueryProviderWithIncludes<T> Query()
        {
            return _db.Query<T>();
        }

        public IEnumerable<T> Query(Sql sql)
        {
            return _db.Query<T>(sql);
        }

        public IEnumerable<T> Query(string sql, params object[] args)
        {
            return _db.Query<T>(sql, args);
        }
        #endregion

        #region add & insert
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(T t)
        {
            return _db.Insert<T>(t) != null;
        }

        /// <summary>
        /// 新增，并返回ID
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int? Insert(T t)
        {
            object rv = _db.Insert(t);
            int id;
            if (int.TryParse(rv.ToString(), out id))
                return id;

            return null;
        }
        #endregion

        #region update & save
        public bool Update(string sql, params object[] args)
        {
            return _db.Update<T>(sql, args) > 0;
        }

        public bool Update(object poco, object primaryKeyValue)
        {
            return Update(TableInfo.TableName, poco, primaryKeyValue);
        }

        public bool Update(string tableName, object poco, object primaryKeyValue)
        {
            return Update(tableName, poco, TableInfo.PrimaryKey, primaryKeyValue);
        }

        public bool Update(string tableName, object poco, string primaryKeyName, object primaryKeyValue)
        {
            return _db.Update(tableName, primaryKeyName, poco, primaryKeyValue) > 0;
        }

        public void Save(T t)
        {
            _db.Save(t);
        }
        #endregion

        #region delete
        public bool Delete(int id)
        {
            return _db.Delete(id) > 0;
        }

        public bool Delete(string primaryKey, params object[] args)
        {
            string sql = "WHERE " + primaryKey + " = @0";
            return _db.Delete<T>(sql, args) > 0;
        }

        public bool DeleteWhere(string where, params object[] args)
        {
            return _db.DeleteWhere<T>(where, args) > 0;
        }
        #endregion
    }
}
