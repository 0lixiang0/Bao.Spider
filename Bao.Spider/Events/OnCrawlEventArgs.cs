namespace Bao.Spider.Events
{
    /// <summary>
    /// 抓取事件
    /// </summary>
    public class OnCrawlEventArgs
    {
        public string Msg { get; set; }

        public OnCrawlEventArgs(string msg)
        {
            this.Msg = msg;
        }
    }
}
