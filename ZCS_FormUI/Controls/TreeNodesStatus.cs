using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public class TreeNodesStatus
    {
        /// <summary>
        ///  获取树节点状态
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="nodes"></param>
        /// <param name="NodesStatus"></param>
        /// <param name="SelectNodeFullPath"></param>
        public static void GetTreeNodesStatus(TreeView treeView, TreeNodeCollection nodes, Hashtable NodesStatus, String SelectNodeFullPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.IsExpanded)
                {
                    NodesStatus[node.FullPath] = true;
                }
                else
                {
                    NodesStatus.Remove(node.FullPath);
                }

                if (node.IsSelected)
                {
                    SelectNodeFullPath = node.FullPath;
                }
                GetTreeNodesStatus(treeView, node.Nodes, NodesStatus, SelectNodeFullPath);

            }

        }
        /// <summary>
        /// 设置树节点状态
        /// </summary>
        /// <param name="nodes"></param>
        public static void SetTreeNodesStatus(TreeView treeView, TreeNodeCollection nodes, Hashtable NodesStatus, String SelectNodeFullPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (NodesStatus[node.FullPath] != null)
                {
                    node.Expand();
                }

                if (node.FullPath == SelectNodeFullPath)
                {
                    treeView.SelectedNode = node;
                }

                SetTreeNodesStatus(treeView, node.Nodes, NodesStatus, SelectNodeFullPath);

            }

        }
    }
}
