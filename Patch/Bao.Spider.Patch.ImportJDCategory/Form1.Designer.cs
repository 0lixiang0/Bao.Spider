namespace Bao.Spider.Patch.ImportJDCategory
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblParams = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnImport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 90);
            this.panel1.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("思源黑体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImport.Location = new System.Drawing.Point(180, 13);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(412, 65);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "从JD到NiuShop";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(26, 103);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(720, 34);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "分类";
            // 
            // lblBrand
            // 
            this.lblBrand.Location = new System.Drawing.Point(26, 158);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(720, 34);
            this.lblBrand.TabIndex = 2;
            this.lblBrand.Text = "品牌";
            // 
            // lblParams
            // 
            this.lblParams.Location = new System.Drawing.Point(26, 213);
            this.lblParams.Name = "lblParams";
            this.lblParams.Size = new System.Drawing.Size(720, 34);
            this.lblParams.TabIndex = 3;
            this.lblParams.Text = "参数";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 322);
            this.Controls.Add(this.lblParams);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "从JD到NiuShop";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblParams;
    }
}

