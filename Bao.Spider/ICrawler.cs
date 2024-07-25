using System;
using System.Threading;
using System.Threading.Tasks;
using Bao.Spider.Events;

namespace Bao.Spider
{
    public interface ICrawler
    {
        /// <summary>
        /// 启动事件
        /// </summary>
        event EventHandler<OnStartEventArgs> OnStart;

        /// <summary>
        /// 爬虫完成事件
        /// </summary>
        event EventHandler<OnCompletedEventArgs> OnCompleted;

        /// <summary>
        /// 抓取事件
        /// </summary>
        event EventHandler<OnCrawlEventArgs> OnCrawl;

        /// <summary>
        /// 爬虫出错事件
        /// </summary>
        event EventHandler<OnErrorEventArgs> OnError;

        /// <summary>
        /// 清空事件
        /// </summary>
        event EventHandler OnClear;
                
        
        /// <summary>
        /// 抓取分类
        /// </summary>
        void CrawlCategory();

        /// <summary>
        /// 抓取产品
        /// </summary>
        void CrawlProduct();

        /// <summary>
        /// 抓取产品详情
        /// </summary>
        void CrawlDetail();
    }
}
