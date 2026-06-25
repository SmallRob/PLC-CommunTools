using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace ZCS_FormUI.Function
{
    public class ImagePropertyEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //指定为模式窗体属性编辑器类型
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //打开属性编辑器修改数据
            FrmSelectImage frm = new FrmSelectImage();
            if (value == null || value is Image)
            {
                if (value != null)
                {
                    frm.SelectImage = (Image)value;
                }

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return frm.SelectImage;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                throw new Exception("这不是一个FontIcons类型的属性");
            }
        }
    }
}
