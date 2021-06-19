using Com_CSSkin;
using CommunTools.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZCS_Common;
using ZCS_FormUI.Controls;
using ZCS_FormUI.Forms;
using static CommunTools.Enums.FuncItemCom;

namespace CommunTools
{
    public partial class FrmMenu : CSSkinMain
    {
        public FrmMenu()
        {
            InitializeComponent();
            isSonSingle = ConfigHelper.GetConfigBool("isSonSingle");
        }

        private List<KeyValuePair<Type, string>> lstMenuGroup;
        private bool isSonSingle = false;

        private void GetMenuGroup()
        {
            var attrBase = new AttributesContext<Com_BaseFuncItem>();
            var attrProto = new AttributesContext<Com_ProtoFuncItem>();
            lstMenuGroup = new List<KeyValuePair<Type, string>>()
            {
               new KeyValuePair<Type,string>(typeof(Com_BaseFuncItem),attrBase.XGroup()),
               new KeyValuePair<Type,string>(typeof(Com_ProtoFuncItem),attrProto.XGroup())
            };
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            GetMenuGroup();

            int gpbIndex = 0;
            int totolHeight = 0;

            foreach (var item in lstMenuGroup)
            {
                int itemCount = EnumHelper.NumberOfEnumValues(item.Key);

                int lineCount = itemCount / 2;
                if (itemCount % 2 > 0)
                {
                    lineCount += 1;
                }

                string[] groupInf = item.Value.Split(',');

                GroupBox gpb = new GroupBox();
                gpb.Name = "gpb_" + groupInf[0];

                int gpbHeight = 98 + (lineCount - 1) * 58;
                gpb.Size = new Size(349, gpbHeight);

                gpb.Location = new Point(20, 33 + totolHeight + 4 * gpbIndex);
                gpb.Text = groupInf[1];
                this.Controls.Add(gpb);

                totolHeight += gpbHeight;

                IList<EnumListModel> lstItem = EnumHelper.GetEnumList(item.Key);

                for (int i = 0, itemCnt = lstItem.Count; i < itemCnt; i++)
                {
                    FuncItemBox box = new FuncItemBox();
                    box.Name = "box_" + lstItem[i].EnumName;

                    var attrUri = new AttributesContext<System.Enum>();
                    System.Enum t = lstItem[i].EnumType;
                    string itemUri = attrUri.XUri(t);

                    string[] itemName = lstItem[i].EnumDescrip.Split('$');
                    string nameMark = itemName.Length > 1 ? itemName[1] : "";
                    box.NewBox(itemName[0], nameMark);
                    box.pnlBox.Tag = box.lblFuncItem.Tag = box.lblFuncMark.Tag = box.Tag = itemUri;

                    box.pnlBox.Click += new EventHandler(box_Click);
                    box.lblFuncItem.Click += new EventHandler(boxItem_Click);
                    box.lblFuncMark.Click += new EventHandler(boxMark_Click);

                    int linId = (i + 1) / 2;
                    if ((i + 1) % 2 > 0)
                    {
                        linId += 1;
                    }

                    int lineAdd = (i + 2) % 2;
                    int box_xAdd = lineAdd > 0 ? box.Width + lineAdd * 23 : 0;
                    int box_yAdd = linId > 1 ? box.Height * (linId - 1) + 14 : 0;

                    int box_Px = 27 + box_xAdd;
                    int box_Py = 29 + box_yAdd;

                    if (linId >= 3) box_Py = box_Py + 14 + 14 * (linId - 3);

                    box.Location = new Point(box_Px, box_Py);
                    gpb.Controls.Add(box);
                }

                gpbIndex++;
                this.Refresh();
            }
        }

        private void boxMark_Click(object sender, EventArgs e)
        {
            box_Click(((Com_CSSkin.SkinControl.SkinLabel)sender).Parent, null);
        }

        private void boxItem_Click(object sender, EventArgs e)
        {
            box_Click(((Com_CSSkin.SkinControl.SkinLabel)sender).Parent, null);
        }

        private void box_Click(object sender, EventArgs e)
        {
            string funcUrl = ((Com_CSSkin.SkinControl.SkinPanel)sender).Parent.Tag.ToString();
            SkipToFunction(funcUrl);
        }

        /// <summary>
        /// 点击按钮跳转到功能
        /// </summary>
        private void SkipToFunction(string funcUrl)
        {
            if (!string.IsNullOrWhiteSpace(funcUrl))
            {
                try
                {
                    if (!FrmOpenJujg(funcUrl))
                    {
                        Form frm = AssemblyCacheManager.Instance.GetFormArg("CommunTools." + funcUrl, null);

                        if (frm != null)
                        {
                            frm.Show();
                            frm.Activate();
                            frm.Focus();
                        }
                        else
                        {
                            FrmDialog.ShowDialog(this, "此功能正在开发！");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteException(ex);
                    FrmDialog.ShowDialog(this, "此功能正在开发！");
                }
            }
            else
            {
                FrmDialog.ShowDialog(this, "此功能尚未开发！");
            }
        }

        private bool FrmOpenJujg(string funcUrl)
        {
            if (isSonSingle)
            {
                int frmCount = Application.OpenForms.Count;
                for (int i = frmCount - 1; i >= 0; i--)
                {
                    string openName = Application.OpenForms[i].Name;
                    if (openName.Equals(funcUrl))
                    {
                        //如果窗口已打开，则激活
                        Application.OpenForms[i].Activate();
                        Application.OpenForms[i].Focus();
                        return true;
                    }
                }
                return false;
            }
            else return true;
        }

        private void FrmMenu_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.notifySystem.Visible = true;
                this.Hide();
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        private void tsmShow_Click(object sender, EventArgs e)
        {
            //this.notifySystem.Visible = false;
            if (!this.Visible)
            {
                this.Show();
                this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            }
            else
            {
                this.Hide();
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.notifySystem.Visible = false;
            this.notifySystem.Dispose();      //解决托盘残留图标

            //杀掉自己的进程
            Process.GetCurrentProcess().Kill();
        }

        private void menuSystem_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmShow.Text = this.Visible ? "最小化" : "显示";
        }
    }
}
