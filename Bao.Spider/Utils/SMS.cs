using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Utils
{
    public class SMS
    {
        /// <summary>
        /// 出现错误或异常时，给我发送提示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Send2Bao(string msg)
        {
#if !DEBUG
            var tel = "18691528284";
            HttpHelper.Get($"http://www.96888sms.cn/api.php/open/send/name/htapp/pwd/e10adc3949ba59abbe56e057f20f883e/phone/{tel}/msg/{msg}【货云网】");
#endif
        }
    }
}
