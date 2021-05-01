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
using System.Diagnostics;

namespace Sly_WAL_Editor
{
    public partial class Sly_1 : Form
    {
        WAC wac = new WAC();
        WACEntry wacEntry = new WACEntry();
        WACFileEntry wacFileEntry = new WACFileEntry();
        FileDirs Dirs = new FileDirs();

        string selectedItem = "";

        public Sly_1()
        {
            InitializeComponent();
        }

        string appPath = Path.GetDirectoryName(Application.ExecutablePath);

        private void openFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "WAL Archive | *.WAL";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream WAL = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        var WACDir = new FileInfo(Path.Combine(Path.GetDirectoryName(ofd.FileName), "SLY.WAC"));

                        if (File.Exists(WACDir.ToString()))
                        {
                            using (FileStream WAC = new FileStream(WACDir.ToString(), FileMode.Open, FileAccess.Read))
                            {
                                using (BinaryReader WACReader = new BinaryReader(WAC))
                                {
                                    using (BinaryReader WALReader = new BinaryReader(WAL))
                                    {
                                        
                                        Dirs.wacDir = WACDir.ToString();
                                        Dirs.walDir = ofd.FileName;
                                        Dirs.folderDir = System.IO.Path.GetDirectoryName(ofd.FileName);
                                        loadFiles();
                                    }
                                }
                            }
                        }

                        else
                        {
                            MessageBox.Show("SLY.WAC could not be found");
                        }

                    }
                }
            }
        }

        private void loadFiles()
        {
            listView1.Items.Clear();

            using (FileStream WAL = new FileStream(Dirs.walDir, FileMode.Open, FileAccess.Read))
            {
                using (FileStream WAC = new FileStream(Dirs.wacDir, FileMode.Open,FileAccess.Read))
                {
                    using (BinaryReader WACReader = new BinaryReader(WAC))
                    {
                        using (BinaryReader WALReader = new BinaryReader(WAL))
                        {
                            wac.entryCount = WACReader.ReadUInt32();
                            wac.entries = new List<WACEntry>();
                            
                            wacEntry.files = new List<WACFileEntry>();

                            for (int a = 0; a < wac.entryCount; a++)
                            {
                                byte letter;
                                int temp = 0;
                                wacEntry.fileNames = "";

                                while (temp != 23)
                                {
                                    letter = WACReader.ReadByte();
                                    if (letter != 0)
                                    {
                                        wacEntry.fileNames += Encoding.Default.GetString(new byte[] { letter });
                                        temp++;
                                    }

                                    else
                                    {
                                        temp++;
                                    }
                                }

                                wacFileEntry.type = WACReader.ReadChar();
                                wacFileEntry.offset = WACReader.ReadUInt32() * 2048;
                                wacFileEntry.size = WACReader.ReadUInt32();

                                long position = WAL.Position;
                                WAL.Seek(wacFileEntry.offset, SeekOrigin.Begin);
                                wacFileEntry.fileData = WALReader.ReadBytes(Convert.ToInt32(wacFileEntry.size));

                                WAL.Seek(position, SeekOrigin.Begin);

                                wacEntry.files.Add(wacFileEntry);

                                ListViewItem lvi = new ListViewItem(wacEntry.fileNames, 0);

                                lvi.SubItems.Add(wacFileEntry.size.ToString());
                                lvi.SubItems.Add(wacFileEntry.type.ToString());
                                lvi.SubItems.Add(wacFileEntry.offset.ToString("X"));

                                listView1.Items.Add(lvi);

                                wac.entries.Add(wacEntry);
                                
                            }

                            label1.Text = wac.entryCount + " Files";
                            listView1.ContextMenuStrip = contextMenuStrip1;
                            addAmbientFileToolStripMenuItem.Enabled = true;
                            addToolStripMenuItem.Enabled = true;
                            addToolStripMenuItem1.Enabled = true;
                            addToolStripMenuItem2.Enabled = true;
                            addLevelFileWToolStripMenuItem.Enabled = true;
                            rebuildArchiveToolStripMenuItem.Enabled = true;
                            extractAllFilesToolStripMenuItem.Enabled = true;
                            textBox1.Enabled = true;
                            button1.Enabled = true;
                            closeToolStripMenuItem.Enabled = true;
                        }
                    }
                }
            }
            
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                selectedItem = listView1.SelectedItems[0].Text;
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedItem = e.Item.Text;
        }

        private void exportFile(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                string fileName = selectedItem.Split('\\')[0];

                for (int i = 0; i < wac.entries.Count; i++)
                {
                    wacEntry = wac.entries[i];

                    if (wacEntry.fileNames.ToString().Equals(fileName))
                    {
                        wacFileEntry = wacEntry.files[i];
                    }
                }

                string filePath = Path.Combine(path, fileName);

                using (FileStream WAL = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    WAL.Write(wacFileEntry.fileData, 0, wacFileEntry.fileData.Length);
                }

                MessageBox.Show("Exported successfully.");
            }
            
        }

        private void exportAllFiles(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {

            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string path = fbd.SelectedPath;

                for (int i = 0; i < wac.entries.Count; i++)
                {
                    wacEntry = wac.entries[i];

                    wacFileEntry = wacEntry.files[i];

                    string filePath = Path.Combine(path, wacEntry.fileNames);
                    wacFileEntry.size = (uint)wacFileEntry.fileData.Length;

                    using (FileStream WAL = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        WAL.Write(wacFileEntry.fileData, 0, wacFileEntry.fileData.Length);
                    }
                }

                MessageBox.Show("Exported successfully.");
            }

        }

        private void replaceFile(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;

            string parentMenuText = (sender as ToolStripMenuItem).OwnerItem.Text;
            string subItemText = (sender as ToolStripMenuItem).Text;

            OpenFileDialog rfd = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };
            if (rfd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < wac.entries.Count; i++)
                {
                    wacEntry = wac.entries[i];

                    if (rfd.SafeFileName == listView1.SelectedItems[0].Text)
                    {
                        continue;
                    }

                    else
                    {
                        MessageBox.Show("File already exist in WAC");
                        return;
                    }
                }

                if (rfd.SafeFileName.Length > 23)
                {
                    MessageBox.Show("File Name cant be longer than 23 letters");
                }

                else
                {
                    var fileSize = new FileInfo(rfd.FileName).Length;

                    for (int i = 0; i < wac.entries.Count; i++)
                    {
                        wacEntry = wac.entries[i];

                        if (wacEntry.fileNames.ToString().Equals(selectedItem.Split('\\')[0]))
                        {
                            wacFileEntry = wacEntry.files[i];

                            switch (subItemText)
                            {
                                case "Replace Ambient (A)":
                                    wacFileEntry.type = 'A';
                                    break;

                                case "Replace Dialogue (D)":
                                    wacFileEntry.type = 'D';
                                    break;

                                case "Replace Effects (E)":
                                    wacFileEntry.type = 'E';
                                    break;

                                case "Replace Music (M)":
                                    wacFileEntry.type = 'M';
                                    break;

                                case "Replace World (W)":
                                    wacFileEntry.type = 'W';
                                    break;
                            }
                            wacEntry.fileNames = rfd.SafeFileName;
                            wacFileEntry.size = (uint)new FileInfo(rfd.FileName).Length;
                            wacFileEntry.fileData = File.ReadAllBytes(rfd.FileName);

                            wac.entries.RemoveAt(i);
                            wac.entries.Insert(i, wacEntry);

                            wacEntry.files.RemoveAt(i);
                            wacEntry.files.Insert(i, wacFileEntry);

                            listView1.SelectedItems[0].Text = wacEntry.fileNames;
                            listView1.SelectedItems[0].SubItems[1].Text = wacFileEntry.size.ToString();
                            listView1.SelectedItems[0].SubItems[2].Text = wacFileEntry.type.ToString();
                            listView1.SelectedItems[0].SubItems[3].Text = "";
                            listView1.SelectedItems[0].ForeColor = Color.Red;
                            listView1.SelectedItems[0].SubItems[1].ForeColor = Color.Red;
                            listView1.SelectedItems[0].SubItems[2].ForeColor = Color.Red;
                        }
                    }
                }
                
            }

        }

        private void addAFile(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;

            string parentMenuText = (sender as ToolStripMenuItem).OwnerItem.Text;
            string subItemText = (sender as ToolStripMenuItem).Text;
            

            OpenFileDialog afd = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            if (afd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < wac.entries.Count; i++)
                {
                    wacEntry = wac.entries[i];

                    if (afd.SafeFileName == wacEntry.fileNames)
                    {
                        MessageBox.Show("Theres already a file named that in the WAC file");
                        return;
                    }
                }

                if (afd.SafeFileName.Length > 23)
                {
                    MessageBox.Show("File Name cant be longer than 23 letters");
                }

                else
                {
                    switch (subItemText)
                    {
                        case "Add Ambient File (A)":
                            wacFileEntry.type = 'A';
                            break;

                        case "Add Dialogue (D)":
                            wacFileEntry.type = 'D';
                            break;

                        case "Add Sound Effects (E)":
                            wacFileEntry.type = 'E';
                            break;

                        case "Add Music (M)":
                            wacFileEntry.type = 'M';
                            break;

                        case "Add Level File (W)":
                            wacFileEntry.type = 'W';
                            break;
                    }

                    wacEntry.fileNames = afd.SafeFileName;
                    wacFileEntry.size = (uint)new FileInfo(afd.FileName).Length;
                    wacFileEntry.fileData = File.ReadAllBytes(afd.FileName);
                    wacEntry.files.Add(wacFileEntry);

                    ListViewItem lvi = new ListViewItem(wacEntry.fileNames, 0);

                    lvi.SubItems.Add(wacFileEntry.size.ToString());
                    lvi.SubItems.Add(wacFileEntry.type.ToString());

                    listView1.Items.Add(lvi);
                    listView1.Focus();
                    listView1.Select();
                    

                    wac.entries.Add(wacEntry);
                    wac.entryCount++;
                    label1.Text = wac.entryCount + " Files";
                }
                
            }
        }



        private void deleteFile(object sender, EventArgs e)
        {
            for (int i = 0; i < wac.entries.Count; i++)
            {
                wacEntry = wac.entries[i];

                if (wacEntry.fileNames.ToString().Equals(selectedItem))
                {
                    wac.entries.RemoveAt(i);
                    wacEntry.files.RemoveAt(i);

                    for (int lvi = listView1.Items.Count - 1; lvi >= 0; lvi--)
                    {
                        if (listView1.Items[lvi].Selected)
                        {
                            listView1.Items[lvi].Remove();
                        }
                    }

                }
            }

            wac.entryCount--;
            label1.Text = wac.entryCount + " Files";
        }
        

        private void rebuildWAL(object sender, EventArgs e)
        {
            using (FileStream WAC = new FileStream(Dirs.wacDir, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter wacWriter = new BinaryWriter(WAC))
                {
                    using (FileStream WAL = new FileStream(Dirs.walDir, FileMode.Create, FileAccess.Write))
                    {
                        using (BinaryWriter walWriter = new BinaryWriter(WAL))
                        {
                            wacWriter.Write(wac.entries.Count);
                            uint walNextSector = 0;
                            uint walSize = 0;

                            for (int i = 0; i < wac.entries.Count; i++)
                            {
                                wacEntry = wac.entries[i];
                                wacFileEntry = wacEntry.files[i];

                                WAC.Write(Encoding.ASCII.GetBytes(wacEntry.fileNames), 0, wacEntry.fileNames.Length);

                                int padding0 = 23 - wacEntry.fileNames.Length;

                                byte[] buffer0;
                                buffer0 = new byte[padding0];

                                WAC.Write(buffer0, 0, buffer0.Length);
                                WAC.WriteByte((byte)wacFileEntry.type);

                                wacFileEntry.offset = walNextSector;

                                walNextSector += (wacFileEntry.size + 2048 - 1) / 2048;

                                WAL.Write(wacFileEntry.fileData, 0, wacFileEntry.fileData.Length);

                                walSize += wacFileEntry.size;
                                
                                WAC.Write(BitConverter.GetBytes(wacFileEntry.offset), 0, sizeof(uint));
                                WAC.Write(BitConverter.GetBytes(wacFileEntry.size), 0, sizeof(uint));
                                
                                var padding1 = (walNextSector * 2048) - walSize;

                                byte[] buffer1;
                                buffer1 = new byte[padding1];

                                WAL.Write(buffer1, 0, buffer1.Length);

                                walSize += padding1;
                            }
                            WAC.Close();
                            WAL.Close();
                        }
                    }
                }
            }
            MessageBox.Show("File Successfully Rebuilt");
        }

        private void FindFile(object sender, EventArgs e)
        {
            listView1.SelectedItems.Clear();

            for(int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if(listView1.Items[i].ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    listView1.Items[i].Selected = true;
                    listView1.Select();
                    listView1.Items[i].Focused = true;
                    listView1.Items[i].EnsureVisible();
                }
            }
        }

        private void closeFile(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            wac.entries.Clear();
            wacEntry.files.Clear();
            label1.Text = "No Files Loaded";
            listView1.ContextMenuStrip = null;
            addAmbientFileToolStripMenuItem.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            addToolStripMenuItem1.Enabled = false;
            addToolStripMenuItem2.Enabled = false;
            addLevelFileWToolStripMenuItem.Enabled = false;
            rebuildArchiveToolStripMenuItem.Enabled = false;
            extractAllFilesToolStripMenuItem.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
            closeToolStripMenuItem.Enabled = false;
        }

        private void restartProgram(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tool Made By: Theclub654 & paledev and velocity\n\nFile names cant be longer than 23 letters\n\nA: Ambient Files\nD: Dialogue Files\nE: Sound Effect Files\nM: Music Files\nW: Level Data Files - Compressed");
        }
    }
}
