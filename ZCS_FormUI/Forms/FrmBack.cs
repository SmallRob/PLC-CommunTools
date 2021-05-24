using System;
using System.ComponentModel;

namespace ZCS_FormUI.Forms
{
    /// <summary>
    /// Class FrmBack.
    /// Implements the <see cref="ZCS_FormUI.Forms.FrmBase" />
    /// </summary>
    /// <seealso cref="ZCS_FormUI.Forms.FrmBase" />
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmBack : FrmBase
    {
        /// <summary>
        /// The FRM title
        /// </summary>
        private string _frmTitle = "自定义窗体";
        /// <summary>
        /// 窗体标题
        /// </summary>
        /// <value>The FRM title.</value>
        [Description("窗体标题"), Category("自定义")]
        public string FrmTitle
        {
            get { return _frmTitle; }
            set
            {
                _frmTitle = value;
                btnBack.BtnText = "   " + value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmBack" /> class.
        /// </summary>
        public FrmBack()
        {
            InitializeComponent();
            InitFormMove(this.panTop);
        }

        /// <summary>
        /// Handles the btnClick event of the btnBack1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnBack1_btnClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
