using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bao.Spider.Patch.ImportJDCategory.Model;

namespace Bao.Spider.Patch.ImportJDCategory
{
    public partial class Form1 : Form
    {
        #region load
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region import
        private void btnImport_Click(object sender, EventArgs e)
        {
            this.lblCategory.Text = "开始导入分类";
            this.category(0, 0, 1);
            this.lblCategory.Text = "分类导入完成";
            this.lblBrand.Text = "品牌导入完成";
            this.lblParams.Text = "参数导入完成";
        }
        #endregion

        #region 分类
        private void category(int jd_pid, int niu_pid, int level)
        {
            // 先查询JD分类
            var list = JDDb.GetCategory(jd_pid);

            // 导入到NIU
            NiuCategory niu;
            foreach (var jd in list)
            {
                this.lblCategory.Text = "正在导入分类：" + jd.cname;
                Application.DoEvents();

                // 数据转换
                niu = new NiuCategory
                {
                    category_name = "JD" + jd.cname,
                    short_name = jd.cname,
                    pid = niu_pid,
                    level = level,
                    is_visible = 1
                };

                var id = NiuDb.AddCategory(niu);

                // 导入品牌
                this.brand(jd.id, jd.cname);

                // 导入参数
                this.param(jd.id, jd.cname);

                // 下级分类
                category(jd.id, id, level + 1);
            }
        }
        #endregion

        #region 品牌
        void brand(int cid, string catname)
        {
            var list = JDDb.GetBrand(cid);
            if (list == null) return;

            var cate_parent = JDDb.GetCategoryParentsId(cid);

            // 导入到NIU
            NiuBrand niu;
            foreach (var jd in list)
            {
                this.lblBrand.Text = "正在导入品牌：" + jd.cname;
                Application.DoEvents();

                // 获取 logo 文件名
                var logo = System.IO.Path.GetFileName(jd.logo);

                // 数据转换
                niu = new NiuBrand
                {
                    brand_name = "JD" + jd.cname,
                    brand_initial = jd.pinyin,
                    category_name = catname,
                    category_id_1 = cate_parent.c1.id,
                    category_id_2 = cate_parent.c2.id,
                    category_id_3 = cid,

                    brand_pic = logo == "" ? "" : "upload/brand/" + logo //
                };

                var id = NiuDb.AddBrand(niu);
            }
        }
        #endregion

        #region 参数
        void param(int cid, string catname)
        {
            this.lblParams.Text = "正在导入参数：" + catname;
            Application.DoEvents();

            // 查参数
            var paras = JDDb.GetParam(cid);
            if (paras == null) return;

            var cate_parent = JDDb.GetCategoryParentsId(cid);

            // 写入属性表
            NiuAttribute attr = new NiuAttribute
            {
                attr_name = $"JD{cate_parent.c1.cname}_{cate_parent.c2.cname}_{catname}",
                is_use = 1,
                create_time = 0
            };
            var id = NiuDb.AddAttribute(attr);

            NiuAttributeValue valu;
            foreach (var p in paras)
            {
                // 查参数值
                var info = JDDb.GetParamInfo(p.id);

                // 写入属性值表
                valu = new NiuAttributeValue
                {
                    attr_value_name = "JD" + p.name,
                    attr_id = id,
                    value = info,
                    type = 3,
                    is_search = 1
                };
                NiuDb.AddAttributeValue(valu);
            }
            
        }
        #endregion
    }
}
