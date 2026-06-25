using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZCS_FormUI.Controls
{
    class DataGridViewCellEx : DataGridViewTextBoxCell
    {
        //每一位数字的宽度
        private const int P_WIDTH = 10;

        public override Type ValueType
        {
            get { return typeof(decimal); }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                                      DataGridViewElementStates cellState, object value, object formattedValue,
                                      string errorText, DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            //背景色
            Color clr_background = (cellState & DataGridViewElementStates.Selected) !=
                                   DataGridViewElementStates.Selected
                                       ? cellStyle.BackColor
                                       : cellStyle.SelectionBackColor;
            using (Brush bru = new SolidBrush(clr_background))
            {
                graphics.FillRectangle(bru, cellBounds);
            }
            //边框
            if ((paintParts & DataGridViewPaintParts.Border) != 0)
            {
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }

            //画出10个整数位，2个小数位
            for (int i = 1; i < 10; i++)
            {
                graphics.DrawLine(Pens.DarkCyan, cellBounds.Left + i*P_WIDTH, cellBounds.Top,
                                  cellBounds.Left + i*P_WIDTH, cellBounds.Bottom - 1);
            }
            graphics.DrawLine(Pens.Red, cellBounds.Left + 10*P_WIDTH, cellBounds.Top, cellBounds.Left + 10*P_WIDTH,
                              cellBounds.Bottom - 1);
            graphics.DrawLine(Pens.DarkCyan, cellBounds.Left + 11*P_WIDTH, cellBounds.Top, cellBounds.Left + 11*P_WIDTH,
                              cellBounds.Bottom - 1);

            //文字
            if (value == null)
                return;
            var sf = new StringFormat
                         {
                             Alignment = StringAlignment.Center,
                             LineAlignment = StringAlignment.Center
                         };

            decimal v = Convert.ToDecimal(value);
            string s_int = ((int) v).ToString();
            //两位小数
            string s_dec = (v*100%100).ToString("00");
            string s_value = "￥" + s_int + s_dec;
            for (int i = 0; i < s_value.Length; i++)
            {
                string ch = s_value[s_value.Length - i - 1].ToString();
                int x = cellBounds.Left + (12 - i - 1)*P_WIDTH;
                int y = cellBounds.Top;
                var rect = new RectangleF(x, y, P_WIDTH, cellBounds.Height);
                graphics.DrawString(ch, cellStyle.Font, Brushes.Black, rect, sf);
            }

            sf.Dispose();
        }
    }
}
