using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sly_WAL_Editor
{
    public partial class Form1 : Form
    {
        WAL wal = new WAL();
        string selectedNode = "";

        public Form1()
        {
            InitializeComponent();
            
        }

        

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "WAL Archive | *.WAL";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    treeView1.Nodes.Clear();
                    using (FileStream fileStream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader binaryReader = new BinaryReader(fileStream))
                        {
                            int numDirs = 0;
                            int numFiles = 0;
                            wal.unk = binaryReader.ReadUInt32();

                            if(wal.unk == 64248363)
                            {
                                MessageBox.Show("File must be decrypted before loading up");
                            }

                            else if(wal.unk != 694)
                            {
                                MessageBox.Show("This is a Invalid File");
                            }

                            else
                            {
                                wal.entryCount = binaryReader.ReadUInt32();
                                wal.entries = new List<TOCEntry>();

                                for (int i = 0; i < wal.entryCount; i++)
                                {
                                    TOCEntry tocEntry = new TOCEntry();

                                    byte letter;
                                    tocEntry.folderName = "";

                                    while ((letter = binaryReader.ReadByte()) != 0)
                                    {
                                        tocEntry.folderName += Encoding.Default.GetString(new byte[] { letter });
                                    }

                                    tocEntry.fileNames = "";

                                    while ((letter = binaryReader.ReadByte()) != 0)
                                    {
                                        tocEntry.fileNames += Encoding.Default.GetString(new byte[] { letter });
                                    }

                                    tocEntry.files = new List<TOCFileEntry>();
                                    List<TreeNode> nodes = new List<TreeNode>();

                                    Align(binaryReader, 4);

                                    for (int j = 0; j < tocEntry.fileNames.Length; j++)
                                    {
                                        TOCFileEntry tocFileEntry = new TOCFileEntry();

                                        tocFileEntry.offset = binaryReader.ReadUInt32() * 2048;
                                        tocFileEntry.size = binaryReader.ReadUInt32();

                                        long position = fileStream.Position;
                                        fileStream.Seek(tocFileEntry.offset, SeekOrigin.Begin);
                                        tocFileEntry.fileData = binaryReader.ReadBytes(Convert.ToInt32(tocFileEntry.size));

                                        fileStream.Seek(position, SeekOrigin.Begin);

                                        tocEntry.files.Add(tocFileEntry);

                                        TreeNode node = new TreeNode(tocEntry.fileNames[j].ToString());
                                        nodes.Add(node);
                                        node.ImageIndex = 1;
                                        node.SelectedImageIndex = 1;
                                        numFiles++;
                                    }

                                    TreeNode treeNode = new TreeNode(tocEntry.folderName, nodes.ToArray());
                                    treeView1.Nodes.Add(treeNode);
                                    treeNode.ImageIndex = 0;
                                    treeNode.SelectedImageIndex = 0;
                                    wal.entries.Add(tocEntry);
                                    numDirs++;
                                }

                                treeView1.ContextMenuStrip = contextMenuStrip1;

                                foreach (TreeNode rootNode in treeView1.Nodes)
                                {
                                    foreach (TreeNode childNode in rootNode.Nodes)
                                    {
                                        childNode.ContextMenuStrip = contextMenuStrip2;
                                    }
                                }

                                label1.Text = numDirs.ToString() + " Folders ," + numFiles.ToString() + " Files";
                            }
                            
                            
                        }
                    }
                }
            
                

                
            }
        }

        private void Align(BinaryReader binaryReader, int alignment, byte padding = 0x00)
        {
            long position = binaryReader.BaseStream.Position;

            if (position % alignment == 0)
            {
                return;
            }

            long number = alignment - position % alignment;

            for (long i = 0; i < number; i++)
            {
                binaryReader.ReadByte();
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(e.Node.FullPath);
            selectedNode = e.Node.FullPath;
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //MessageBox.Show(e.Node.FullPath);
            selectedNode = e.Node.FullPath;
        }

        private void TsmiImportFolder_Click(object sender, EventArgs e)
        {

        }

        private void TsmiExportFolder_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(openFileDialog.FileName);
                path = Path.Combine(path, selectedNode);
                Directory.CreateDirectory(path);

                ExportAllFilesFromFolder(selectedNode, path);
            }
        }

        private void TsmiImportFile_Click(object sender, EventArgs e)
        {

        }

        private void TsmiExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(openFileDialog.FileName);
                string folderName = selectedNode.Split('\\')[0];
                string fileName = selectedNode.Split('\\')[1];

                ExportFile(folderName, fileName, path);
            }
        }

        private TOCFileEntry FindFileEntry(string folderName, string fileName)
        {
            TOCFileEntry tocFileEntry = new TOCFileEntry();

            for (int i = 0; i < wal.entries.Count; i++)
            {
                TOCEntry tocEntry = wal.entries[i];

                if (tocEntry.folderName.Equals(folderName))
                {
                    for (int j = 0; j < tocEntry.fileNames.Length; j++)
                    {
                        if (tocEntry.fileNames[j].ToString().Equals(fileName))
                        {
                            tocFileEntry = tocEntry.files[j];
                        }
                    }
                }
            }

            return tocFileEntry;
        }

        private void ExportFile(string folderName, string fileName, string path)
        {
            TOCFileEntry tocFileEntry = FindFileEntry(folderName, fileName);
            string filePath = Path.Combine(path, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(tocFileEntry.fileData, 0, tocFileEntry.fileData.Length);
            }

            MessageBox.Show("Exported successfully.");
        }

        private void ExportAllFilesFromFolder(string folderName, string filePath)
        {
            for (int i = 0; i < wal.entries.Count; i++)
            {
                TOCEntry tocEntry = wal.entries[i];

                if (tocEntry.folderName.Equals(folderName))
                {
                    for (int j = 0; j < tocEntry.fileNames.Length; j++)
                    {
                        TOCFileEntry tocFileEntry = tocEntry.files[j];
                        string path = Path.Combine(filePath, tocEntry.fileNames[j].ToString());

                        using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            fileStream.Write(tocFileEntry.fileData, 0, tocFileEntry.fileData.Length);
                        }
                    }
                }
            }

            MessageBox.Show("Exported successfully.");
        }
    }
}
