using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Framework
{
    public static class ConvertHelper
    {
        #region 文本转数字
        /// <summary>
        /// 文本转数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ToInt(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            int d;
            if (!int.TryParse(s, out d))
                return null;

            return d;
        }
        /// <summary>
        /// 文本转数字
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue">不能正常转换时的默认值</param>
        /// <returns></returns>
        public static int ToInt(this string s, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(s))
                return defaultValue;

            int d;
            if (!int.TryParse(s, out d))
                return defaultValue;

            return d;
        }
        #endregion
    }
}
