using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bao.Spider.Framework;

namespace Bao.Spider
{
    public partial class MainFrm : Form
    {
        #region ctor
        public MainFrm()
        {
            InitializeComponent();

            this.Opacity = 0.93;
        }
        #endregion

        #region load
        private void MainFrm_Load(object sender, EventArgs e)
        {
            // jd
            this.btnCrawler.Text = "抓取分类";
            this.btnCrawlProduct.Text = "抓取品牌";
            this.btnCrawlDetails.Text = "抓取参数";
        }
        #endregion

        #region event
        private void MainFrm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        #region btnCrawler_Click
        // 抓取分类
        private void btnCrawler_Click(object sender, EventArgs e)
        {
            // 一
            //Thread t = new Thread(new ThreadStart(run));
            //t.Start();

            Task.Run(() =>
            {
                run(1);
            });
        }

        // 抓取产品
        private void btnCrawlProduct_Click(object sender, EventArgs e)
        {
            // 二
            Task.Run(() =>
            {
                run(2);
            });
        }

        // 抓取明细
        private void btnCrawlDetails_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                run(3);
            });
        }
        #endregion

        #region run
        private void run(int type)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.btnCrawler.Enabled = false;
                this.btnCrawlProduct.Enabled = false;
                this.btnCrawlDetails.Enabled = false;
            }));

            ICrawler crawler = null;// = new Websites.PCOnline.Crawler();

            try
            {
                //crawler = new Websites.PCOnline.Crawler();
                crawler = new Websites.JD.Crawler();
            }
            catch (Exception ex)
            {
                Utils.SMS.Send2Bao("抓取产品出错，请查看日志");

                Logger.Default.Error(ex, crawler.GetType().FullName);
            }

            crawler.OnStart += (s, e) =>
            {
                show(e.Msg);
            };
            crawler.OnCompleted += (s, e) =>
            {
                Utils.SMS.Send2Bao(e.Msg);

                show(e.Msg);

                this.btnCrawler.Enabled = true;
                this.btnCrawlProduct.Enabled = true;
                this.btnCrawlDetails.Enabled = true;
            };
            crawler.OnCrawl += (s, e) =>
            {
                show(e.Msg);
            };
            crawler.OnError += (s, e) =>
            {
                Logger.Default.Error(e.Exception, $"{e.Website} | {e.Url}");
            };
            crawler.OnClear += (s, e) =>
            {
                clear();
            };

            switch (type)
            {
                case 1:
                    crawler.CrawlCategory();
                    break;
                case 2:
                    crawler.CrawlProduct();
                    break;
                case 3:
                    crawler.CrawlDetail();
                    break;
            }
        }
        #endregion

        #region 显示进度信息
        private void show(string msg)
        {
            //Action act = () =>
            //{
            //    this.txtResult.AppendText(msg + "\r\n");
            //};
            //this.BeginInvoke(act);

            this.BeginInvoke(new Action(() =>
            {
                this.txtResult.AppendText(msg + "\r\n");
                //this.txtResult.Text = msg;
            }));

            // MethodInvoker是无参无返回值
            //MethodInvoker mi = new MethodInvoker(()=> {
            //    this.txtResult.Text = result;
            //});
            //this.BeginInvoke(mi);
        }
        private void clear()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.txtResult.Clear();
            }));
        }
        #endregion
    }
}
