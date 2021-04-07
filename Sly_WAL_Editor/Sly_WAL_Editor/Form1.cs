﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Sly_WAL_Editor
{
    public partial class Form1 : Form
    {
        WAL wal = new WAL();
        TOCEntry tocEntry = new TOCEntry();
        TOCFileEntry tocFileEntry = new TOCFileEntry();
        string selectedNode = "";

        public Form1()
        {
            InitializeComponent();
        }

        int numFiles = 0;
        uint maxTOCSize = 0;

        string appPath = Path.GetDirectoryName(Application.ExecutablePath);
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
                            
                            wal.unk = binaryReader.ReadUInt32();

                            switch(wal.unk)
                            {
                                case 64248363:
                                    MessageBox.Show("File must be decrypted you can download my decryptor from the above link");
                                    if (MessageBox.Show(
                                    "Website", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                    {
                                        System.Diagnostics.Process.Start("https://github.com/theclub654/Sly-HD-collection-Toolset");
                                    }
                                    break;

                                case 64248469:
                                    MessageBox.Show("File must be decrypted you can download my decryptor from the above link\n\nhttps://github.com/theclub654/Sly-HD-collection-Toolset");
                                    if (MessageBox.Show(
                                    "Website", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                    {
                                        System.Diagnostics.Process.Start("https://github.com/theclub654/Sly-HD-collection-Toolset");
                                    }
                                    break;

                                case 694:
                                    wal.walDir = ofd.FileName;
                                    numFiles = 0;
                                    maxTOCSize = 0x40000;
                                    loadFiles();
                                break;

                                case 520:
                                    wal.walDir = ofd.FileName;
                                    numFiles = 0;
                                    maxTOCSize = 0x80000;
                                    loadFiles();
                                break;

                                default:
                                    MessageBox.Show("Invalide File");
                                break;
                            }

                        }
                    }
                }
            }
        }
        
        private void loadFiles()
        {
            treeView1.Nodes.Clear();

            using (FileStream fileStream = new FileStream(wal.walDir, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    binaryReader.BaseStream.Position = 0x4;
                    
                    wal.entryCount = binaryReader.ReadUInt32();
                    wal.entries = new List<TOCEntry>();

                    for (int i = 0; i < wal.entryCount; i++)
                    {
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
                    }

                    treeView1.ContextMenuStrip = contextMenuStrip1;

                    foreach (TreeNode rootNode in treeView1.Nodes)
                    {
                        foreach (TreeNode childNode in rootNode.Nodes)
                        {
                            childNode.ContextMenuStrip = contextMenuStrip2;
                        }
                    }

                    label1.Text = wal.entryCount + " Folders ," + numFiles.ToString() + " Files";
                    rebuildFileToolStripMenuItem.Enabled = true;
                    textBox1.Enabled = true;
                    button1.Enabled = true;
                    closeToolStripMenuItem.Enabled = true;
                    rebuildFileToolStripMenuItem.Enabled = true;
                    rebuildToolStripMenuItem.Enabled = true;
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

        private void Alignrebuild(FileStream WAL)
        {
            if (WAL.Position % 4 != 0)
            {
                WAL.Position += (uint)(4 - (WAL.Position % 4));
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
            for (int i = 0; i < wal.entries.Count; i++)
            {
                tocEntry = wal.entries[i];

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
            tocFileEntry = FindFileEntry(folderName, fileName);
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
                tocEntry = wal.entries[i];

                if (tocEntry.folderName.Equals(folderName))
                {
                    for (int j = 0; j < tocEntry.fileNames.Length; j++)
                    {
                        tocFileEntry = tocEntry.files[j];

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


        private void AddFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            

            DialogResult dialogResult = fbd.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                for (int i = 0; i < wal.entries.Count; i++)
                {
                    tocEntry = wal.entries[i];

                    if (tocEntry.folderName == Path.GetFileName(fbd.SelectedPath))
                    {
                        MessageBox.Show("Folder already exist in the WAL file");
                        return;
                    }
                }

                int invalidFiles = 0;
                tocEntry.folderName = Path.GetFileName(fbd.SelectedPath);
                tocEntry.files = new List<TOCFileEntry>();
                List<TreeNode> nodes = new List<TreeNode>();
                tocEntry.fileNames = "";

                foreach (var files in Directory.GetFiles(fbd.SelectedPath))
                {
                    FileInfo info = new FileInfo(files);
                    if (Path.GetFileName(info.FullName).Length > 1)
                    {
                        invalidFiles++;
                        continue;
                    }

                    else
                    {
                        tocEntry.fileNames += Path.GetFileName(info.FullName);
                        tocFileEntry.size = (uint)info.Length;
                        tocFileEntry.fileData = File.ReadAllBytes(info.FullName);
                        tocEntry.files.Add(tocFileEntry);

                        TreeNode node = new TreeNode(info.Name.ToString());
                        nodes.Add(node);
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                        numFiles++;
                    }
                }

                TreeNode treeNode = new TreeNode(tocEntry.folderName, nodes.ToArray());
                treeView1.Nodes.Add(treeNode);
                treeView1.SelectedNode = treeNode;
                treeNode.EnsureVisible();

                treeNode.ImageIndex = 0;
                treeNode.SelectedImageIndex = 0;
                wal.entries.Add(tocEntry);

                treeView1.ContextMenuStrip = contextMenuStrip1;

                foreach (TreeNode rootNode in treeView1.Nodes)
                {
                    foreach (TreeNode childNode in rootNode.Nodes)
                    {
                        childNode.ContextMenuStrip = contextMenuStrip2;
                    }
                }

                if (invalidFiles > 0)
                {
                    MessageBox.Show("There were " + invalidFiles.ToString() + " files that couldn't be added with the folder because there names where longer than 1 letter");
                }
            }
            wal.entryCount++;
            label1.Text = wal.entryCount + " Folders ," + numFiles.ToString() + " Files";
        }


        private void replaceFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();


            DialogResult dialogResult = fbd.ShowDialog();
            
            if(dialogResult == DialogResult.OK)
            {
                for (int i = 0; i < wal.entries.Count; i++)
                {
                    tocEntry = wal.entries[i];

                    if (treeView1.SelectedNode.Text == Path.GetFileName(fbd.SelectedPath))
                    {
                        continue;
                    }

                    else if(tocEntry.folderName == Path.GetFileName(fbd.SelectedPath))
                    {
                        MessageBox.Show("Folder already exist in the WAL file");
                        return;
                    }
                }

                for (int i = 0; i < wal.entries.Count; i++)
                {
                    tocEntry = wal.entries[i];

                    if (tocEntry.folderName.Equals(selectedNode))
                    {
                        int invalidFiles = 0;
                        tocEntry.folderName = Path.GetFileName(fbd.SelectedPath);
                        tocEntry.fileNames = "";

                        foreach (var files in Directory.GetFiles(fbd.SelectedPath))
                        {
                            FileInfo info = new FileInfo(files);
                            if(Path.GetFileName(info.FullName).Length > 1)
                            {
                                invalidFiles++;
                                continue;
                            }
                            else
                            {
                                tocEntry.fileNames += Path.GetFileName(info.FullName);
                            }
                        }
                            

                        for (int j = 0; j < tocEntry.fileNames.Length; j++)
                        {
                            tocEntry.files.Remove(tocFileEntry);
                            treeView1.SelectedNode.Nodes.Clear();
                        }

                        tocEntry.files = new List<TOCFileEntry>();
                        foreach (var files in Directory.GetFiles(fbd.SelectedPath))
                        {
                            FileInfo info = new FileInfo(files);
                            tocFileEntry.size = (uint)info.Length;
                            tocFileEntry.fileData = File.ReadAllBytes(info.FullName);
                            tocEntry.files.Add(tocFileEntry);
                            numFiles++;
                            label1.Text = wal.entryCount + " Folders ," + numFiles.ToString() + " Files";
                        }

                        wal.entries.RemoveAt(i);
                        wal.entries.Insert(i, tocEntry);
                        treeView1.SelectedNode.Text = tocEntry.folderName;

                        for (int j = 0; j < tocEntry.fileNames.Length; j++)
                        {
                            TreeNode tn = treeView1.SelectedNode.Nodes.Add(tocEntry.fileNames[j].ToString());
                            tn.ImageIndex = 1;
                            tn.SelectedImageIndex = 1;
                        }

                        foreach (TreeNode rootNode in treeView1.Nodes)
                        {
                            foreach (TreeNode childNode in rootNode.Nodes)
                            {
                                childNode.ContextMenuStrip = contextMenuStrip2;
                            }
                        }

                        if (invalidFiles > 0)
                        {
                            MessageBox.Show("There were " + invalidFiles.ToString() + " files that couldn't be added with the folder because there names where longer than 1 letter");
                        }
                    }
                }
            }
        }

        private void deleteFolder(object sender, EventArgs e)
        {
            for (int i = 0; i < wal.entries.Count; i++)
            {
                tocEntry = wal.entries[i];

                if (tocEntry.folderName.Equals(selectedNode))
                {
                    wal.entries.RemoveAt(i);
                    
                    for (int j = 0; j < tocEntry.fileNames.Length; j++)
                    {
                        tocEntry.files.Remove(tocFileEntry);
                        numFiles--;
                    }
                }
            }
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Parent == null)
                {
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                }
                else
                {
                    treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
                }
            }
            wal.entryCount--;
            label1.Text = wal.entryCount + " Folders ," + numFiles.ToString() + " Files";
        }

        private void addFiletoFolder(object sender, EventArgs e)
        {
            OpenFileDialog afd = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            DialogResult dialogResult = afd.ShowDialog();
            int index = 0;
            if (dialogResult == DialogResult.OK)
            {
                if (afd.SafeFileName.Length > 1)
                {
                    MessageBox.Show("File name must be 1 character long");
                    return;
                }


                else
                {
                    for (int i = 0; i < wal.entries.Count; i++)
                    {
                        tocEntry = wal.entries[i];

                        if (tocEntry.folderName.Equals(selectedNode))
                        {
                            tocEntry = wal.entries[i];
                            tocFileEntry = tocEntry.files[i];
                            index = i;

                            for (int j = 0; j < tocEntry.fileNames.Length; j++)
                            {
                                if (afd.SafeFileName[0] == tocEntry.fileNames[j])
                                {
                                    MessageBox.Show("File Directory already has a file named that");
                                    return;
                                }
                            }

                            break;
                        }
                    }

                    tocEntry.fileNames += afd.SafeFileName;
                    
                    tocFileEntry.size = (uint)new FileInfo(afd.FileName).Length;
                    tocFileEntry.fileData = File.ReadAllBytes(afd.FileName);

                    tocEntry.files.Add(tocFileEntry);
                    wal.entries.RemoveAt(index);
                    wal.entries.Insert(index, tocEntry);
                    
                    TreeNode tn = treeView1.SelectedNode.Nodes.Add(afd.SafeFileName);
                    tn.ImageIndex = 1;
                    tn.SelectedImageIndex = 1;
                }
            }

            foreach (TreeNode rootNode in treeView1.Nodes)
            {
                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    childNode.ContextMenuStrip = contextMenuStrip2;
                }
            }
            numFiles++;
            label1.Text = wal.entryCount + " Folders ," + numFiles.ToString() + " Files";
        }



        private void replaceFile(object sender, EventArgs e)
        {
            OpenFileDialog rfd = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            DialogResult dialogResult = rfd.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                if (rfd.SafeFileName.Length > 1)
                {
                    MessageBox.Show("File name must be 1 character long");
                }

                else
                {
                    
                    for (int i = 0; i < wal.entries.Count; i++)
                    {
                        tocEntry = wal.entries[i];

                        if (tocEntry.folderName.Equals(selectedNode.Split('\\')[0]))
                        {
                            for (int j = 0; j < tocEntry.fileNames.Length; j++)
                            {
                                for(int n = 0; n < tocEntry.fileNames.Length; n++)
                                {
                                    if (rfd.SafeFileName == treeView1.SelectedNode.Text)
                                    {
                                        continue;
                                    }

                                    else if (rfd.SafeFileName[0] == tocEntry.fileNames[j])
                                    {
                                        MessageBox.Show("File already exist in directory");
                                        return;
                                    }
                                }

                                if (tocEntry.fileNames[j].ToString().Equals(selectedNode.Split('\\')[1]))
                                {
                                    
                                    StringBuilder sb = new StringBuilder(tocEntry.fileNames);

                                    for(int k = 0; k < tocEntry.fileNames.Length; k++)
                                    {
                                        if (sb[k] == tocEntry.fileNames[j])
                                        {
                                            sb[k] = rfd.SafeFileName.ToCharArray()[0];
                                            tocEntry.fileNames = sb.ToString();
                                            tocFileEntry.size = (uint)new FileInfo(rfd.FileName).Length;
                                            tocFileEntry.fileData = File.ReadAllBytes(rfd.FileName);
                                            tocEntry.files.RemoveAt(j);
                                            tocEntry.files.Insert(j, tocFileEntry);
                                            wal.entries.RemoveAt(i);
                                            wal.entries.Insert(i, tocEntry);
                                            treeView1.SelectedNode.Text = rfd.SafeFileName;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void deleteFile(object sender, EventArgs e)
        {
            for (int i = 0; i < wal.entries.Count; i++)
            {
                tocEntry = wal.entries[i];

                if (tocEntry.folderName.Equals(selectedNode.Split('\\')[0]))
                {
                    for (int j = 0; j < tocEntry.fileNames.Length; j++)
                    {
                        if (tocEntry.fileNames[j].ToString().Equals(selectedNode.Split('\\')[1]))
                        {
                            tocEntry.files.RemoveAt(j);
                            StringBuilder sb = new StringBuilder(tocEntry.fileNames);
                            for (int k = 0; k < tocEntry.fileNames.Length; k++)
                            {
                                if (sb[k] == tocEntry.fileNames[j])
                                {
                                    sb.Remove(k, 1);
                                    tocEntry.fileNames = sb.ToString();
                                    wal.entries.RemoveAt(i);
                                    wal.entries.Insert(i, tocEntry);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Parent == null)
                {
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                }
                else
                {
                    treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
                }
            }

            numFiles--;
            label1.Text = wal.entryCount + " Folders ," + numFiles.ToString() + " Files";
        }

        private void FindFolder(object sender, EventArgs e)
        {
            for (int i = treeView1.Nodes.Count - 1; i >= 0; i--)
            {
                if (treeView1.Nodes[i].ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    treeView1.SelectedNode = treeView1.Nodes[i];
                    treeView1.Focus();
                    treeView1.Nodes[i].EnsureVisible();
                }
            }
        }

        private void rebuildWAL(object sender, EventArgs e)
        {
            using (FileStream WAL = new FileStream(wal.walDir, FileMode.Create, FileAccess.Write))
            {
                List<long> newOffsets = new List<long>();
                WAL.Write(BitConverter.GetBytes(wal.unk), 0, sizeof(uint));
                WAL.Write(BitConverter.GetBytes(wal.entries.Count), 0, sizeof(uint));

                uint sectorSize = 0x800;
                uint walNextSector = maxTOCSize / sectorSize;
                long offset;
                var count = 0;

                for (int i = 0; i < wal.entries.Count; i++)
                {
                    tocEntry = wal.entries[i];

                    WAL.Write(Encoding.ASCII.GetBytes(tocEntry.folderName), 0, tocEntry.folderName.Length);
                    WAL.WriteByte(0);
                    WAL.Write(Encoding.ASCII.GetBytes(tocEntry.fileNames), 0, tocEntry.fileNames.Length);
                    WAL.WriteByte(0);

                    Alignrebuild(WAL);

                    for (int b = 0; b < tocEntry.fileNames.Length; b++)
                    {
                        tocFileEntry = tocEntry.files[b];
                        tocFileEntry.offset = walNextSector;
                        
                        walNextSector += (tocFileEntry.size + sectorSize - 0x1) / sectorSize;
                        newOffsets.Add(tocFileEntry.offset * sectorSize);

                        WAL.Write(BitConverter.GetBytes(tocFileEntry.offset), 0, sizeof(int));
                        WAL.Write(BitConverter.GetBytes(tocFileEntry.size), 0, sizeof(int));
                    }
                }

                long padding = maxTOCSize - WAL.Position;
                byte[] buffer = new byte[padding];

                WAL.Write(buffer, 0, buffer.Length);

                for (int i = 0; i < wal.entries.Count; i++)
                {
                    tocEntry = wal.entries[i];

                    for(int j = 0; j < tocEntry.fileNames.Length; j++)
                    {
                        tocFileEntry = tocEntry.files[j];
                        
                        offset = newOffsets[count];

                        while (WAL.Position != offset)
                        {
                            WAL.WriteByte(0);
                        }

                        count++;
                        WAL.Write(tocFileEntry.fileData, 0, tocFileEntry.fileData.Length);
                    }
                }
                newOffsets.Clear();
                WAL.Close();
            }
            MessageBox.Show("File Successfully Rebuilt");
        }

        private void closeFile(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            wal.entries.Clear();
            tocEntry.files.Clear();
            label1.Text = "No Files Loaded";
            rebuildFileToolStripMenuItem.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
            closeToolStripMenuItem.Enabled = false;
            treeView1.ContextMenuStrip = null;
            rebuildFileToolStripMenuItem.Enabled = false;
            rebuildToolStripMenuItem.Enabled = false;
            maxTOCSize = 0;
        }

        private void restartProgram(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tool Made By: Theclub654 & paledev and velocity\n\nYou can only add files that have 1 letter file names\n\nB: Soundbank Files\nD: Detail Files - Compressed\nE: Sound Effect Files\nG: Graphic Files - Compressed\nM: Music Files\nZ: Level Files - Compressed\ni/h/n/a/e/d/g/f/p/s/w: Dialogue Language Files in various languages");
        }
    }
}
