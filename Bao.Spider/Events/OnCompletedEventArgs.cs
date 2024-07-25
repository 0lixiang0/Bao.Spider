namespace Bao.Spider.Events
{
    /// <summary>
    /// 爬虫完成事件
    /// </summary>
    public class OnCompletedEventArgs
    {
        public string Msg { get; private set; }

        public OnCompletedEventArgs(string msg)
        {
            this.Msg = msg;
        }
    }
}
