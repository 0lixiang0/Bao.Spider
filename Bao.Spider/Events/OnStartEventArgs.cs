namespace Bao.Spider.Events
{
    /// <summary>
    /// 启动事件
    /// </summary>
    public class OnStartEventArgs
    {
        public string Msg { get; set; }

        public OnStartEventArgs(string msg)
        {
            this.Msg = msg;
        }
    }
}
