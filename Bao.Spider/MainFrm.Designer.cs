namespace Bao.Spider
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.btnCrawler = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCrawlDetails = new System.Windows.Forms.Button();
            this.btnCrawlProduct = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtResult = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCrawler
            // 
            this.btnCrawler.Location = new System.Drawing.Point(29, 7);
            this.btnCrawler.Name = "btnCrawler";
            this.btnCrawler.Size = new System.Drawing.Size(168, 58);
            this.btnCrawler.TabIndex = 0;
            this.btnCrawler.Text = "抓取分类";
            this.btnCrawler.UseVisualStyleBackColor = true;
            this.btnCrawler.Click += new System.EventHandler(this.btnCrawler_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCrawlDetails);
            this.panel1.Controls.Add(this.btnCrawlProduct);
            this.panel1.Controls.Add(this.btnCrawler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 72);
            this.panel1.TabIndex = 2;
            // 
            // btnCrawlDetails
            // 
            this.btnCrawlDetails.Location = new System.Drawing.Point(377, 7);
            this.btnCrawlDetails.Name = "btnCrawlDetails";
            this.btnCrawlDetails.Size = new System.Drawing.Size(168, 58);
            this.btnCrawlDetails.TabIndex = 1;
            this.btnCrawlDetails.Text = "抓取产品详情";
            this.btnCrawlDetails.UseVisualStyleBackColor = true;
            this.btnCrawlDetails.Click += new System.EventHandler(this.btnCrawlDetails_Click);
            // 
            // btnCrawlProduct
            // 
            this.btnCrawlProduct.Location = new System.Drawing.Point(203, 7);
            this.btnCrawlProduct.Name = "btnCrawlProduct";
            this.btnCrawlProduct.Size = new System.Drawing.Size(168, 58);
            this.btnCrawlProduct.TabIndex = 1;
            this.btnCrawlProduct.Text = "抓取产品列表";
            this.btnCrawlProduct.UseVisualStyleBackColor = true;
            this.btnCrawlProduct.Click += new System.EventHandler(this.btnCrawlProduct_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "产品抓取程序";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.ForeColor = System.Drawing.Color.Lime;
            this.txtResult.Location = new System.Drawing.Point(0, 72);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(575, 195);
            this.txtResult.TabIndex = 1;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 267);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品抓取程序";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.SizeChanged += new System.EventHandler(this.MainFrm_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCrawler;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCrawlProduct;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnCrawlDetails;
        private System.Windows.Forms.TextBox txtResult;
    }
}

