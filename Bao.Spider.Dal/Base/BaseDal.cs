//using System.Collections.Generic;
//using Bao.Spider.Data.DB;
//using NPoco;
//using NPoco.Linq;

//namespace Bao.Spider.Dal.Base
//{
//    public class BaseDal<T> : IDbBase<T> where T : class, new()
//    {
//        #region ctor
//        protected IDbBase<T> _db;

//        public BaseDal()
//        {
//            _db = new DbBase<T>();
//        }
//        #endregion

//        #region exists & is new
//        public bool IsExists(object priaryKey)
//        {
//            return _db.IsExists(priaryKey);
//        }

//        public bool IsNew(T t)
//        {
//            return _db.IsNew(t);
//        }
//        #endregion

//        #region get
//        public T Get(int id)
//        {
//            return _db.Get(id);
//        }

//        public T Get(string sql, params object[] args)
//        {
//            return _db.Get(sql, args);
//        }
//        #endregion

//        #region query list
//        public IQueryProviderWithIncludes<T> Query()
//        {
//            return _db.Query();
//        }

//        public IEnumerable<T> Query(Sql sql)
//        {
//            return _db.Query(sql);
//        }

//        public IEnumerable<T> Query(string sql, params object[] args)
//        {
//            return _db.Query(sql, args);
//        }
//        #endregion

//        #region add
//        public bool Add(T t)
//        {
//            return _db.Add(t);
//        }

//        public int? Insert(T t)
//        {
//            return _db.Insert(t);
//        }
//        #endregion

//        #region update
//        public bool Update(string sql, params object[] args)
//        {
//            return _db.Update(sql, args);
//        }

//        public bool Update(object poco, object primaryKeyValue)
//        {
//            return _db.Update(poco, primaryKeyValue);
//        }

//        public bool Update(string tableName, object poco, object primaryKeyValue)
//        {
//            return _db.Update(tableName, poco, primaryKeyValue);
//        }

//        public bool Update(string tableName, object poco, string primaryKeyName, object primaryKeyValue)
//        {
//            return _db.Update(tableName, poco, primaryKeyName, primaryKeyValue);
//        }

//        public void Save(T t)
//        {
//            _db.Save(t);
//        }
//        #endregion

//        #region delete
//        public bool Delete(int id)
//        {
//            return _db.Delete(id);
//        }

//        public bool Delete(string primaryKey, params object[] args)
//        {
//            return _db.Delete(primaryKey, args);
//        }

//        public bool DeleteWhere(string where, params object[] args)
//        {
//            return _db.DeleteWhere(where, args);
//        }
//        #endregion
//    }
//}
