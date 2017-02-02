using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicaMinimalista.Controls
{
    public partial class MotifTreeView : System.Windows.Forms.TreeView
    {
        public MotifTreeView()
        {
            InitializeComponent();
        }

        internal void addMotif(string motifName)
        {
            TreeNode t = new TreeNode(motifName, 0, 0);
            t.Name = motifName;
            this.Nodes.Add(t);
        }

        internal void addMotif(string motifName, string parentName){
            TreeNode t = new TreeNode(motifName, 0, 0);
            t.Name = motifName;
            TreeNode parent = this.Nodes.Find(parentName, true)[0];
            parent.Nodes.Add(t);
        }

        internal void deleteMotif(string motifName)
        {
            TreeNode node = this.Nodes.Find(motifName, true)[0];
            TreeNode parent = node.Parent;
            TreeNode[] RootNodeArray = new TreeNode[node.Nodes.Count];
            node.Nodes.CopyTo(RootNodeArray, 0);
            node.Nodes.Clear();
            if (parent == null)
            {
                this.Nodes.AddRange(RootNodeArray);
                this.Nodes.Remove(node);
            }
            else
            {
                parent.Nodes.AddRange(RootNodeArray);
                parent.Nodes.Remove(node);
            }
        }

        internal void restoreChilds(string motifName, List<string> childNames)
        {
            TreeNode node = this.Nodes.Find(motifName, true)[0];
            foreach (string childName in childNames)
            {
                TreeNode child = this.Nodes.Find(childName, true)[0];
                this.Nodes.Remove(child);
                node.Nodes.Add(child);
            }
        }

        internal void renameMotif(string oldName, string newName)
        {
            TreeNode node = this.Nodes.Find(oldName, true)[0];
            node.Name = newName;
            node.Text = newName;
        }

        internal void selectMotif(string motifName)
        {
            TreeNode node = this.Nodes.Find(motifName, true)[0];
            this.SelectedNode = node;
        }
    }
}
