using System.Collections.Generic;
using NPoco;
using NPoco.Linq;

namespace Bao.Spider.Data.DB
{
    public interface IDbBase<T> where T : class, new()
    {
        #region exists & is new
        bool IsExists(object priaryKey);

        bool IsNew(T t);
        #endregion

        #region get
        T Get(int id);

        T Get(string sql, params object[] args);
        #endregion

        #region query
        IQueryProviderWithIncludes<T> Query();

        IEnumerable<T> Query(Sql sql);

        IEnumerable<T> Query(string sql, params object[] args);
        #endregion

        #region add & insert
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Add(T t);

        /// <summary>
        /// 新增，并返回ID
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int? Insert(T t);
        #endregion

        #region update & save
        bool Update(string sql, params object[] args);

        bool Update(object poco, object primaryKeyValue);

        bool Update(string tableName, object poco, object primaryKeyValue);

        bool Update(string tableName, object poco, string primaryKeyName, object primaryKeyValue);

        /// <summary>
        /// 自动发送Insert（如果表中不存在）或Update子句
        /// </summary>
        /// <param name="t"></param>
        void Save(T t);
        #endregion

        #region delete
        /// <summary>
        /// 直接从数据库中删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 根据给定的字段删除
        /// </summary>
        /// <param name="primaryKey">字段名</param>
        /// <param name="args">值</param>
        /// <returns></returns>
        bool Delete(string primaryKey, params object[] args);

        bool DeleteWhere(string where, params object[] args);
        #endregion
    }
}
