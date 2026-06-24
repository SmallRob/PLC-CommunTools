using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public partial class TreeViewCheckEx : TreeView
    {
        public TreeViewCheckEx()
        {
            InitializeComponent();
        }

        public TreeViewCheckEx(IContainer container)
        {
            container.Add(this);

            this.BorderStyle = BorderStyle.None;
            InitializeComponent();
        }

        #region WIN32
        public const UInt32 TV_FIRST = 4352;
        public const UInt32 TVSIL_NORMAL = 0;
        public const UInt32 TVSIL_STATE = 2;
        public const UInt32 TVM_SETIMAGELIST = TV_FIRST + 9;
        public const UInt32 TVM_GETNEXTITEM = TV_FIRST + 10;
        public const UInt32 TVIF_HANDLE = 16;
        public const UInt32 TVIF_STATE = 8;
        public const UInt32 TVIS_STATEIMAGEMASK = 61440;

        public const UInt32 TVM_GETITEM = TV_FIRST + 12;
        public const UInt32 TVM_SETITEM = TV_FIRST + 13;
        private const UInt32 TVM_HITTEST = TV_FIRST + 17;

        public const UInt32 TVGN_ROOT = 0;

        //TVHITTESTINFO.flags flags
        public const int TVHT_ONITEMSTATEICON = 64;


        // Use a sequential structure layout to define TVITEM for the TreeView.
        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public uint mask;
            public IntPtr hItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVHITTESTINFO
        {
            public POINTAPI pt;
            public int flags;
            public IntPtr hItem;
        }

        // Declare two overloaded SendMessage functions. The
        // difference is in the last parameter: one is ByVal and the
        // other is ByRef.
        [DllImport("user32.dll")]
        private static extern UInt32 SendMessage(IntPtr hWnd, UInt32 Msg,
            UInt32 wParam, UInt32 lParam);

        [DllImport("User32", CharSet = CharSet.Auto)]
        private static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg,
            UInt32 wParam, ref TVITEM lParam);

        [DllImport("User32", CharSet = CharSet.Auto)]
        private static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg,
            UInt32 wParam, ref TVHITTESTINFO lParam);
        #endregion

        #region 私有 fields
        private Dictionary<TreeNode, CkBoxState> _NodeStateDict = new Dictionary<TreeNode, CkBoxState>();
        #endregion 

        #region Protected fields
        protected ImageList _CtrlStateImageList;
        protected bool _ImageListSent = false;
        #endregion

        #region Public 属性
        /// <summary>
        /// The imagelist for node state
        /// </summary>
        [
        Browsable(true),
        CategoryAttribute("Behavior"),
        Description("从该结点状态图像的ImageList控制")
        ]
        public ImageList CheckBoxStateImageList
        {
            set
            {
                _ImageListSent = false;
                _CtrlStateImageList = value;
            }

            get
            {
                if (_CtrlStateImageList == null)
                {
                    _CtrlStateImageList = ctlStateImageList;
                }
                return _CtrlStateImageList;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 设置树节点和他的子checkbox的状态递归
        /// </summary>
        /// <param name="treeNode">tree node</param>
        /// <param name="checkboxState">checkbox state</param>
        private void SetTreeNodeAndChildrenStateRecursively(TreeNode treeNode, CkBoxState checkboxState)
        {
            if ((treeNode != null))
            {
                SetTreeNodeState(treeNode, checkboxState);

                foreach (TreeNode objChildTreeNode in treeNode.Nodes)
                {
                    SetTreeNodeAndChildrenStateRecursively(objChildTreeNode, checkboxState);
                }
            }
        }

        /// <summary>
        /// 递归设置父TreeNode的复选框的状态
        /// </summary>
        /// <param name="parentTreeNode"></param>
        private void SetParentTreeNodeStateRecursively(TreeNode parentTreeNode)
        {
            CkBoxState checkboxState;
            bool bAllChildrenChecked = true;
            bool bAllChildrenUnchecked = true;

            if ((parentTreeNode != null))
            {
                if (GetTreeNodeCheckBoxState(parentTreeNode) != CkBoxState.None)
                {
                    foreach (TreeNode treeNode in parentTreeNode.Nodes)
                    {
                        checkboxState = GetTreeNodeCheckBoxState(treeNode);

                        switch (checkboxState)
                        {
                            case CkBoxState.Checked:
                                bAllChildrenUnchecked = false;
                                break;
                            case CkBoxState.Indeterminate:
                                bAllChildrenUnchecked = false;
                                bAllChildrenChecked = false;
                                break;
                            case CkBoxState.Unchecked:
                                bAllChildrenChecked = false;
                                break;
                        }

                        if (bAllChildrenChecked == false & bAllChildrenUnchecked == false)
                        {
                            // This is an optimization
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                    if (bAllChildrenChecked)
                    {
                        SetTreeNodeState(parentTreeNode, CkBoxState.Checked);
                    }
                    else if (bAllChildrenUnchecked)
                    {
                        SetTreeNodeState(parentTreeNode, CkBoxState.Unchecked);
                    }
                    else
                    {
                        SetTreeNodeState(parentTreeNode, CkBoxState.Indeterminate);
                    }

                    // Enter in recursion
                    if ((parentTreeNode.Parent != null))
                    {
                        SetParentTreeNodeStateRecursively(parentTreeNode.Parent);
                    }
                }
            }
        }

        /// <summary>
        /// TreeNode 屏幕上的位置
        /// </summary>
        /// <param name="x">x position in screen</param>
        /// <param name="y">y position in screen</param>
        /// <returns></returns>
        private TreeNode GetTreeNodeHitAtCheckBoxByScreenPosition(int x, int y)
        {
            Point clientPoint;
            TreeNode treeNode;

            clientPoint = this.PointToClient(new Point(x, y));

            treeNode = GetTreeNodeHitAtCheckBoxByClientPosition(clientPoint.X, clientPoint.Y);

            return treeNode;
        }

        /// <summary>
        /// TreeNode的客户端位置
        /// </summary>
        /// <param name="x">x position in client</param>
        /// <param name="y">y position in client</param>
        /// <returns></returns>
        private TreeNode GetTreeNodeHitAtCheckBoxByClientPosition(int x, int y)
        {
            TreeNode treeNode = null;
            UInt32 iTreeNodeHandle;
            TVHITTESTINFO tTVHITTESTINFO = new TVHITTESTINFO();

            // Get the hit info
            tTVHITTESTINFO.pt.x = x;
            tTVHITTESTINFO.pt.y = y;
            iTreeNodeHandle = SendMessage(this.Handle, TVM_HITTEST, 0, ref tTVHITTESTINFO);

            // Check if it has clicked on an item
            if (iTreeNodeHandle != 0)
            {
                // Check if it has clicked on the state image of the item
                if ((tTVHITTESTINFO.flags & TVHT_ONITEMSTATEICON) != 0)
                {
                    treeNode = TreeNode.FromHandle(this, new IntPtr(iTreeNodeHandle));
                }
            }

            return treeNode;
        }

        /// <summary>
        /// 设置树节点复选框状态
        /// </summary>
        /// <param name="treeNode">tree node</param>
        /// <param name="checkboxState">checkbox state</param>
        private void SetTreeNodeState(TreeNode treeNode, CkBoxState checkboxState)
        {
            IntPtr hWnd;
            TVITEM tvi;
            hWnd = this.Handle;

            // Send a TVM_SETIMAGELIST with TVSIL_STATE.
            if (!_ImageListSent)
            {
                SendMessage(hWnd, (UInt32)TVM_SETIMAGELIST, (UInt32)TVSIL_STATE, (UInt32)CheckBoxStateImageList.Handle);
                _ImageListSent = true;
            }

            // The following uses the TVM_SETITEM message to set the State 
            // of a given item. It uses the TVITEM structure.

            //  tvi.mask: include TVIF_HANDLE and TVIF_STATE
            tvi.mask = TVIF_HANDLE | TVIF_STATE;

            // To use the State image, tvi.State cannot be 0.  
            //Setting it to 1 means to use the second image in the image list.
            tvi.state = (uint)checkboxState;
            // Left shift 12 to put info in bits 12 to 15
            tvi.state = tvi.state << 12;
            // Set StateMask. -This is required to isolate State above.
            tvi.stateMask = TVIS_STATEIMAGEMASK;

            // Define the item we want to set the State in.
            tvi.hItem = treeNode.Handle;  //For example, try the root.

            //  Initialize the rest to zero.
            tvi.pszText = (IntPtr)0;
            tvi.cchTextMax = 0;
            tvi.iImage = 0;
            tvi.iSelectedImage = 0;
            tvi.cChildren = 0;
            tvi.lParam = (IntPtr)0;

            // Send the TVM_SETITEM message.
            //  TVM_SETITEM = 4365
            SendMessage(hWnd, (UInt32)TVM_SETITEM, (UInt32)0, ref tvi);

            //Set Node State
            SetNodeState(treeNode, checkboxState);
        }

        #endregion

        #region Protected Methods

        protected void OnCheckBoxStateChanged(CheckBoxStateChangedEventArgs args)
        {
            EventHandler<CheckBoxStateChangedEventArgs> handler = CheckBoxStateChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            TreeNode treeNode;

            base.OnMouseUp(e);

            treeNode = GetTreeNodeHitAtCheckBoxByClientPosition(e.X, e.Y);
            if ((treeNode != null))
            {
                ToggleTreeNodeState(treeNode);
            }
        }

        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Space)
            {
                if ((this.SelectedNode != null))
                {
                    ToggleTreeNodeState(this.SelectedNode);
                }
            }
        }

        protected override void OnBeforeExpand(System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            // PATCH：正在扩大，如果节点通过双击状态图像，取消
            if ((GetTreeNodeHitAtCheckBoxByScreenPosition(Control.MousePosition.X, Control.MousePosition.Y) != null))
            {
                e.Cancel = true;
            }

        }

        protected override void OnBeforeCollapse(System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            // PATCH：如果节点正被坍塌的状态图像通过双击，取消
            if ((GetTreeNodeHitAtCheckBoxByScreenPosition(Control.MousePosition.X, Control.MousePosition.Y) != null))
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// 设置TreeNode的复选框状态
        /// </summary>
        /// <param name="treeNode">treenode</param>
        /// <param name="state">checkbox state</param>
        protected void SetNodeState(TreeNode treeNode, CkBoxState state)
        {
            if (!_NodeStateDict.ContainsKey(treeNode))
            {
                _NodeStateDict.Add(treeNode, state);
            }
            else
            {
                _NodeStateDict[treeNode] = state;
            }

            OnCheckBoxStateChanged(new CheckBoxStateChangedEventArgs(state, treeNode));
        }

        /// <summary>
        /// TreeNode的复选框状态 
        /// </summary>
        /// <param name="treeNode">treenode</param>
        /// <returns>checkbox state</returns>
        protected CkBoxState GetNodeState(TreeNode treeNode)
        {
            return _NodeStateDict[treeNode];
        }

        /// <summary>
        /// 状态切换的TreeNode
        /// </summary>
        /// <param name="treeNode">tree node</param>
        protected void ToggleTreeNodeState(TreeNode treeNode)
        {
            CkBoxState checkboxState = GetTreeNodeCheckBoxState(treeNode);

            this.BeginUpdate();

            switch (checkboxState)
            {
                case CkBoxState.Unchecked:
                    SetTreeNodeAndChildrenStateRecursively(treeNode, CkBoxState.Checked);
                    SetParentTreeNodeStateRecursively(treeNode.Parent);
                    break;
                case CkBoxState.Checked:
                case CkBoxState.Indeterminate:
                    SetTreeNodeAndChildrenStateRecursively(treeNode, CkBoxState.Unchecked);
                    SetParentTreeNodeStateRecursively(treeNode.Parent);
                    break;
            }

            this.EndUpdate();
        }

        /// <summary>
        /// Add TreeNode
        /// </summary>
        /// <param name="nodes">node collection</param>
        /// <param name="text">text</param>
        /// <param name="iImageIndex">image index</param>
        /// <param name="checkboxState">check box state</param>
        /// <returns></returns>
        protected TreeNode AddTreeNode(TreeNodeCollection nodes, string text, CkBoxState checkboxState)
        {
            TreeNode treeNode = nodes.Add(text);

            return AddTreeNode(nodes, treeNode, checkboxState);
        }

        /// <summary>
        /// Add TreeNode
        /// </summary>
        /// <param name="nodes">node collection</param>
        /// <param name="treeNode">TreeNode</param>
        /// <param name="iImageIndex">image index</param>
        /// <param name="checkboxState">check box state</param>
        /// <returns></returns>
        protected TreeNode AddTreeNode(TreeNodeCollection nodes, TreeNode treeNode, CkBoxState checkboxState)
        {
            if (!nodes.Contains(treeNode))
            {
                nodes.Add(treeNode);
            }

            this.SetTreeNodeState(treeNode, checkboxState);

            SetTreeNodeAndChildrenStateRecursively(treeNode, checkboxState);
            SetParentTreeNodeStateRecursively(treeNode.Parent);

            return treeNode;
        }

        protected TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, CkBoxState checkboxState)
        {
            TreeNode treeNode = nodes.Add(key, text, imageIndex);

            this.SetTreeNodeState(treeNode, checkboxState);
            SetTreeNodeAndChildrenStateRecursively(treeNode, checkboxState);
            SetParentTreeNodeStateRecursively(treeNode.Parent);

            return treeNode;
        }

        protected TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, int selectedImageIndex, CkBoxState checkboxState)
        {
            TreeNode treeNode = nodes.Add(key, text, imageIndex, selectedImageIndex);

            this.SetTreeNodeState(treeNode, checkboxState);
            SetTreeNodeAndChildrenStateRecursively(treeNode, checkboxState);
            SetParentTreeNodeStateRecursively(treeNode.Parent);

            return treeNode;
        }

        protected TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, CkBoxState checkboxState)
        {
            TreeNode treeNode = nodes.Add(key, text, imageKey);

            this.SetTreeNodeState(treeNode, checkboxState);
            SetTreeNodeAndChildrenStateRecursively(treeNode, checkboxState);
            SetParentTreeNodeStateRecursively(treeNode.Parent);

            return treeNode;
        }

        protected TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, string selectedImageKey, CkBoxState checkboxState)
        {
            TreeNode treeNode = nodes.Add(key, text, imageKey, selectedImageKey);

            this.SetTreeNodeState(treeNode, checkboxState);
            SetTreeNodeAndChildrenStateRecursively(treeNode, checkboxState);
            SetParentTreeNodeStateRecursively(treeNode.Parent);

            return treeNode;
        }

        #endregion

        #region Events
        [
        Browsable(true),
        CategoryAttribute("Property Changed"),
        Description("Occurs when the checkbox state of node changed")
        ]
        public event EventHandler<CheckBoxStateChangedEventArgs> CheckBoxStateChanged;

        #endregion

        #region Public Methods

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string text, bool checkboxChecked)
        {
            CkBoxState checkboxState = checkboxChecked ? CkBoxState.Checked : CkBoxState.Unchecked;
            return AddTreeNode(nodes, text, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, TreeNode node, bool checkboxChecked)
        {
            CkBoxState checkboxState = checkboxChecked ? CkBoxState.Checked : CkBoxState.Unchecked;
            return AddTreeNode(nodes, node, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, bool checkboxChecked)
        {
            CkBoxState checkboxState = checkboxChecked ? CkBoxState.Checked : CkBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageIndex, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, int selectedImageIndex, bool checkboxChecked)
        {
            CkBoxState checkboxState = checkboxChecked ? CkBoxState.Checked : CkBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageIndex, selectedImageIndex, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, bool checkboxChecked)
        {
            CkBoxState checkboxState = checkboxChecked ? CkBoxState.Checked : CkBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageKey, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, string selectedImageKey, bool checkboxChecked)
        {
            CkBoxState checkboxState = checkboxChecked ? CkBoxState.Checked : CkBoxState.Unchecked;
            return AddTreeNode(nodes, key, text, imageKey, selectedImageKey, checkboxState);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string text)
        {
            return AddTreeNode(nodes, text, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, TreeNode node)
        {
            return AddTreeNode(nodes, node, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex)
        {
            return AddTreeNode(nodes, key, text, imageIndex, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, int imageIndex, int selectedImageIndex)
        {
            return AddTreeNode(nodes, key, text, imageIndex, selectedImageIndex, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey)
        {
            return AddTreeNode(nodes, key, text, imageKey, false);
        }

        public TreeNode AddTreeNode(TreeNodeCollection nodes, string key, string text, string imageKey, string selectedImageKey)
        {
            return AddTreeNode(nodes, key, text, imageKey, selectedImageKey, false);
        }

        /// <summary>
        /// Get Tree Node CheckBox State
        /// </summary>
        /// <param name="treeNode">node</param>
        /// <returns>CheckBox State</returns>
        public CkBoxState GetTreeNodeCheckBoxState(TreeNode treeNode)
        {
            return GetNodeState(treeNode);
        }

        /// <summary>
        /// Set Tree Node CheckBox State
        /// </summary>
        /// <param name="treeNode">node</param>
        /// <param name="checkboxChecked">CheckBox State</param>
        /// <returns>New CheckBox State</returns>
        public CkBoxState SetTreeNodeCheckBoxChecked(TreeNode treeNode, bool checkboxChecked)
        {
            CkBoxState checkboxState = GetTreeNodeCheckBoxState(treeNode);
            bool done = false;

            switch (checkboxState)
            {
                case CkBoxState.Unchecked:
                    if (checkboxChecked)
                    {
                        done = true;
                    }
                    break;
                case CkBoxState.Checked:
                case CkBoxState.Indeterminate:
                    if (!checkboxChecked)
                    {
                        done = true;
                    }
                    break;
            }

            if (done)
            {
                ToggleTreeNodeState(treeNode);
            }

            return GetTreeNodeCheckBoxState(treeNode);
        }

        #endregion
    }


    /// <summary>
    /// CheckBox State
    /// </summary>
    public enum CkBoxState
    {
        None = 0,
        Unchecked = 1,
        Checked = 2,
        Indeterminate = 3
    }

    public class CheckBoxStateChangedEventArgs : EventArgs
    {
        private CkBoxState _State;
        private TreeNode _Node;

        public CkBoxState State
        {
            get
            {
                return _State;
            }
        }

        public TreeNode Node
        {
            get
            {
                return _Node;
            }
        }

        public CheckBoxStateChangedEventArgs(CkBoxState state, TreeNode node)
        {
            _State = state;
            _Node = node;
        }
    }
}
