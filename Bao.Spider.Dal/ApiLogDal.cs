using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class ApiLogDal : DbBase<ApiLog>
    {
        public ApiLogDal() : base()
        {
            
        }

        public static ApiLogDal DB
        {
            get { return new ApiLogDal(); }
        }

        #region 计数+1
        public void Count(string controller, string action)
        {
            var m = this.Db.FirstOrDefault<ApiLog>($"SELECT * FROM api_log  WHERE controller=@0 AND action=@1", controller, action);

            if (m == null)
            {
                this.Db.Insert(new ApiLog
                {
                    controller = controller,
                    action = action,
                    hits = 1
                });
            }
            else
            {
                this.Update(new { hits = m.hits + 1 }, m.id);
            }

        }
        #endregion
    }
}
