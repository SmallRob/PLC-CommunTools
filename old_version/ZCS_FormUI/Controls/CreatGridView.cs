using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace ZCS_FormUI.Controls
{
    public class CreatGridView
    {
        private static CreatGridView instance = null;
        private static readonly object locker = new Object();

        Form frm = null;

        private static DataGridViewEx dgv = new DataGridViewEx();

        public DataSet ds = null;

        private CreatGridView()
        {
        }
        public static CreatGridView Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            dgv.Name = "dgv";
                            dgv.AllowUserToAddRows = false;
                            dgv.AllowUserToDeleteRows = false;
                            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
                            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                            dgv.BackgroundColor = System.Drawing.SystemColors.Window;
                            dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                            dgv.RowHeadersVisible = false;
                            dgv.Size = new System.Drawing.Size(500, 200);
                            dgv.Location = new Point(500, 400);
                            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                            dgv.BringToFront();
                            ((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();

                            instance = new CreatGridView();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 创建DataGridView并添加到窗体中
        /// </summary>
        /// <param name="frm"></param>
        public void CreatDataGridView(Form frm)
        {
            this.frm = frm;
            this.frm.KeyPreview = true;
            this.frm.MouseClick += new MouseEventHandler(frm_MouseClick);
            this.frm.KeyDown += new KeyEventHandler(frm_KeyDown);
            this.frm.KeyPress += new KeyPressEventHandler(frm_KeyPress);
            dgv.KeyDown += new KeyEventHandler(dgv_KeyDown);
            dgv.CellDoubleClick += new DataGridViewCellEventHandler(dgv_CellDoubleClick);
            this.frm.Controls.Add(dgv);
            this.frm.Controls.SetChildIndex(dgv, 0);
            dgv.Hide();
        }

        /// <summary>
        /// DataGridView双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.SelectedCells.Count != 0)
            {
                int CIndex = dgv.CurrentRow.Index;

                foreach (Control ctl in this.frm.Controls)
                {
                    ControlGetValue(ctl);
                }
                dgv.Hide();
            }
        }

        /// <summary>
        /// 显示DataGridView并指定显示效果
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="ds"></param>
        public void ShowDataGridView(Controls.TextBoxEx txt, DataSet ds, int wValue)
        {
            dgv.TabIndex = txt.TabIndex;

            dgv.Size = new System.Drawing.Size(wValue, 200);

            dgv.Location = new Point(txt.Location.X, txt.Location.Y + txt.Height);

            dgv.Parent = txt.Parent;

            dgv.BringToFront();

            dgv.DataSource = null;

            dgv.DataSource = ds.Tables["table"].DefaultView;

            int widths = 0;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells); // 自动调整列宽 
                widths += dgv.Columns[i].Width; // 计算调整列后单元列的宽度和 
            }

            if (widths >= dgv.Size.Width) // 如果调整列的宽度大于设定列宽 
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; // 调整列的模式 自动
            else
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 如果小于 则填充 

            Cursor.Current = Cursors.Default;

            dgv.Show();
        }

        /// <summary>
        /// 给对应的控件负值
        /// </summary>
        /// <param name="ctls"></param>
        private void ControlGetValue(Control ctls)
        {
            int CIndex = dgv.CurrentRow.Index;

            if (ctls.HasChildren)
            {
                foreach (Control ctl in ctls.Controls)
                {
                    ControlGetValue(ctl);
                }
            }
            else
            {
                if (ctls is TextBoxEx || ctls is LabelEx)
                {
                    if (ctls.Tag != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["paramName"].ToString().Trim() == ctls.Tag.ToString())
                            {
                                ctls.Text = dgv[ctls.Tag.ToString(), CIndex].Value.ToString().Trim();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// DataGridView回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            int CIndex = dgv.CurrentRow.Index;

            if (e.KeyCode == Keys.Enter && dgv.Focus())
            {
                foreach (Control ctl in this.frm.Controls)
                {
                    ControlGetValue(ctl);
                }

                dgv.Hide();
            }
        }

        /// <summary>
        /// 离开DataGridView以外按鼠标左右键DataGridView隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                if (dgv != null)
                    dgv.Hide();
            }
        }

        /// <summary>
        /// 按下键选择DataGridView中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgv == null)
                return;

            if (dgv.Visible == true && e.KeyCode == Keys.Down)
            {
                if (dgv.Rows.Count > 0)
                {
                    this.frm.Focus();
                    dgv.Focus();
                    dgv.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Form窗体Enter键切换文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.frm.SelectNextControl(this.frm.ActiveControl, true, true, true, true);
            }
        }
    }
}
