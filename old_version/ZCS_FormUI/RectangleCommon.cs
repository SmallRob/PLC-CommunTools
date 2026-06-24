using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ZCS_FormUI
{
    public class RectangleCommon
    {
        /// <summary>
        /// 获取在大的Rectangle中的小矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="smallSize"></param>
        /// <returns></returns>
        public static Rectangle GetSmallRectOfRectangle(Rectangle rectangle, Size smallSize, out Rectangle absRectangle)
        {
            Rectangle rect = new Rectangle();
            absRectangle = new Rectangle();
            absRectangle.Size = smallSize;
            absRectangle.X = (rectangle.Width - smallSize.Width) / 2;
            absRectangle.Y = (rectangle.Height - smallSize.Height) / 2;
            rect.Size = smallSize;
            rect.X = absRectangle.X + rectangle.X;
            rect.Y = absRectangle.Y + rectangle.Y;
            return rect;
        }
    }
}
