using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace ZCS_FormUI.Controls
{
    /// <summary>
    /// 扩展列
    /// </summary>
    public class DataGridViewCheckBoxColumnEx : DataGridViewCheckBoxColumn
    {
        /// <summary>
        /// 扩展表头属性
        /// </summary>
        public DataGridViewCheckBoxColumnHeaderCellEx HeaderCellEx
        {
            get { return this.HeaderCell as DataGridViewCheckBoxColumnHeaderCellEx; }
        }

        /// <summary>
        /// 本列是否全选，方便在Cell中获取
        /// </summary>
        [Browsable(true), Description("是否全选,默认不选中")]
        public Boolean IsCheckedAll
        {
            get { return HeaderCellEx.CheckedAllState == CheckState.Checked; }
        }

        public DataGridViewCheckBoxColumnEx()
            : base()
        {
            //建立表头单元格
            this.HeaderCell = new DataGridViewCheckBoxColumnHeaderCellEx() as DataGridViewColumnHeaderCell;
            //建立单元格模板
            this.CellTemplate = new DataGridViewCheckBoxCellEx();
        }
    }

    /// <summary>
    /// 扩展表头单元格
    /// </summary>
    public class DataGridViewCheckBoxColumnHeaderCellEx : DataGridViewColumnHeaderCell
    {
        #region "type:properties"

        /// <summary>
        /// 宿主DataGridView扩展
        /// </summary>
        protected DataGridViewEx DataGridViewEx
        {
            get { return DataGridView as DataGridViewEx; }
        }

        private CheckState m_checkedAllState = CheckState.Unchecked;
        /// <summary>
        /// 全选按钮状态
        /// </summary>
        public CheckState CheckedAllState
        {
            get { return m_checkedAllState; }
            set { m_checkedAllState = value; }
        }

        /// <summary>
        /// 用于标识当前的checkbox状态
        /// </summary>
        protected System.Windows.Forms.VisualStyles.CheckBoxState m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

        /// <summary>
        /// 是否是鼠标经过的这种hot状态，不是即为normal
        /// </summary>
        protected Boolean m_isHot = false;

        /// <summary>
        /// checkbox按钮区域
        /// </summary>
        protected Rectangle m_chkboxRegion;

        /// <summary>
        /// 相对于本cell的位置
        /// </summary>
        protected Rectangle m_absChkboxRegion;

        #endregion

        #region "type:paint"

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, @"", @"", errorText, cellStyle, advancedBorderStyle, paintParts);
            this.m_chkboxRegion = RectangleCommon.GetSmallRectOfRectangle(cellBounds, CheckBoxRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal), out m_absChkboxRegion);
            this.RenderCheckBox(graphics);
        }

        protected void RenderCheckBox(Graphics graphics)
        {
            if (m_isHot)
                RenderCheckBoxHover(graphics);
            else
                RenderCheckBoxNormal(graphics);

            CheckBoxRenderer.DrawCheckBox(graphics, m_chkboxRegion.Location, m_checkboxState);
        }

        protected void RenderCheckBoxNormal(Graphics graphics)
        {
            switch (m_checkedAllState)
            {
                case CheckState.Unchecked:
                    this.m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                    break;
                case CheckState.Indeterminate:
                    this.m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal;
                    break;
                case CheckState.Checked:
                    this.m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                    break;
            }
        }

        protected void RenderCheckBoxHover(Graphics graphics)
        {
            switch (m_checkedAllState)
            {
                case CheckState.Unchecked:
                    this.m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedHot;
                    break;
                case CheckState.Indeterminate:
                    this.m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.MixedHot;
                    break;
                case CheckState.Checked:
                    this.m_checkboxState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedHot;
                    break;
            }
        }

        #endregion

        #region "type:events"

        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsInCheckRegion(e.Location))
                m_isHot = true;
            this.DataGridView.InvalidateCell(this);
        }

        protected override void OnMouseLeave(int rowIndex)
        {
            base.OnMouseLeave(rowIndex);
            m_isHot = false;
            this.DataGridView.InvalidateCell(this);
        }

        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_isHot = IsInCheckRegion(e.Location);
            this.DataGridView.InvalidateCell(this);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Boolean value = false;
            if (IsInCheckRegion(e.Location))
            {
                switch (m_checkedAllState)
                {
                    case CheckState.Unchecked:
                        m_checkedAllState = CheckState.Checked;
                        value = true;
                        break;
                    case CheckState.Indeterminate:
                        m_checkedAllState = CheckState.Checked;
                        value = true;
                        break;
                    case CheckState.Checked:
                        m_checkedAllState = CheckState.Unchecked;
                        value = false;
                        break;
                }
                this.Value = value;
                this.DataGridViewEx.OnCheckAllCheckedChange(e.ColumnIndex, value);
            }
            base.OnMouseClick(e);
        }

        /// <summary>
        /// 是否在checkbox按钮区域
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected bool IsInCheckRegion(Point p)
        {
            return this.m_absChkboxRegion.Contains(p);
        }

        #endregion
    }

    /// <summary>
    /// 扩展单元格
    /// </summary>
    public class DataGridViewCheckBoxCellEx : DataGridViewCheckBoxCell
    {
        /// <summary>
        /// 本单元格是否被选中
        /// </summary>
        public Boolean Checked
        {
            get
            {
                return Convert.ToBoolean(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        /// <summary>
        /// 宿主列扩展
        /// </summary>
        internal DataGridViewCheckBoxColumnEx OwningColumnEx
        {
            get { return this.OwningColumn as DataGridViewCheckBoxColumnEx; }
        }

        /// <summary>
        /// 宿主datagridview扩展
        /// </summary>
        internal DataGridViewEx DataGridViewEx
        {
            get { return this.DataGridView as DataGridViewEx; }
        }

        /// <summary>
        /// 处理选中与取消选中
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Checked = !this.Checked;
            this.DataGridViewEx.OnCheckBoxCellCheckedChange(e.ColumnIndex, e.RowIndex, this.Checked);
            base.OnMouseClick(e);
        }
    }
}
