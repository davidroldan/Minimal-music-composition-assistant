using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization;

using MusicaMinimalista.Objects.Music;
using MusicaMinimalista.Controls;

namespace MusicaMinimalista.Objects.Utils
{
    public class ProjectUtil
    {
        internal void save(string filename, Tune tune, MotifTreeView motifTreeView)
        {
            List<TreeNode> nodes = motifTreeView.Nodes.Cast<TreeNode>().ToList();

            //Pack all data in one class
            KeyValuePair<Tune, List<TreeNode>> data = new KeyValuePair<Tune, List<TreeNode>>(tune, nodes);

            FileStream stream = File.Open(filename, FileMode.Create);
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

            //TreeNode needs to be a KnownType.
            List<Type> knowntypes = new List<Type>();
            knowntypes.Add(typeof(TreeNode));

            using (var file = XmlWriter.Create(stream, settings))
            {
                var ser = new DataContractSerializer(typeof(KeyValuePair<Tune, List<TreeNode>>), knowntypes);
                ser.WriteObject(file, data);
            }
            stream.Flush();
            stream.Close();
        }

        internal void load(string filename, out Tune tune, MotifTreeView motifTreeView)
        {
            FileStream stream = File.Open(filename, FileMode.Open);

            //TreeNode needs to be a KnownType.
            List<Type> knowntypes = new List<Type>();
            knowntypes.Add(typeof(TreeNode));

            KeyValuePair<Tune, List<TreeNode>> data;
            using (var file = XmlReader.Create(stream))
            {
                var ser = new DataContractSerializer(typeof(KeyValuePair<Tune, List<TreeNode>>), knowntypes);
                data = (KeyValuePair<Tune, List<TreeNode>>)ser.ReadObject(file);
            }
            stream.Flush();
            stream.Close();

            //MotifTreeView
            motifTreeView.Nodes.Clear();
            TreeNode[] nodeList = data.Value.ToArray();
            motifTreeView.Nodes.AddRange(nodeList);

            //Tune
            tune = data.Key;
        }
    }
}
